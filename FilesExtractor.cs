using AuslogicsTest.Enums;
using AuslogicsTest.Extensions;
using AuslogicsTest.Interfaces;
using Microsoft.SqlServer.Server;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace AuslogicsTest
{
    /// <summary>
    /// 
    /// </summary>
    internal class FilesExtractor
    {

        private CancellationTokenSource _CancelTokenSource = null;
        private Task _Task = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iCollectionConsumer"></param>
        /// <param name="iInfo"></param>
        public void RefreshAutoRunFiles( IMultiThreadObservableCollectionConsumer<AutoRunItem> iCollectionConsumer,
                                         IInfo iInfo )
        {
            if( null != this._CancelTokenSource )
            {
                Debug.WriteLine( "Token cancel" );
                _CancelTokenSource.Cancel();                             
            }

            this._CancelTokenSource = new CancellationTokenSource();
            CancellationToken token = _CancelTokenSource.Token;
                        
            
            if( null == this._Task )
            {
                this._Task = new Task( ()=> FilesExtractTask( iCollectionConsumer, iInfo, token ) );
                this._Task.Start();
            }
            else
            {
                this._Task = this._Task.ContinueWith( obj => {  FilesExtractTask( iCollectionConsumer, 
                                                                                  iInfo, token ); } );
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="iCollectionConsumer"></param>
        /// <param name="iInfo"></param>
        /// <param name="token"></param>
        public void FilesExtractTask( IMultiThreadObservableCollectionConsumer<AutoRunItem> iCollectionConsumer,                                             
                                      IInfo iInfo,
                                      CancellationToken token )
        {
            iCollectionConsumer.Clear();

            IEnumerable<RawAutorunItemBase> all_items;
            
            all_items = MenuAutoStartFiles();

            all_items = all_items.Concat( RegistryAutoStartFiles() );

            AutoRunItemCreator creator = new AutoRunItemCreator();

            Int32 count_added = 0;

            foreach( var raw_item in all_items )
            {
#if DEBUG
                Thread.Sleep( 400 ); // 
#endif             
                if( token.IsCancellationRequested )
                {
                    break;
                }

                raw_item.Accept( creator );

                if( null != creator.Result )
                {
                    count_added++;
                    iInfo.Message( String.Format( "Add file: {0} ({1})", creator.Result.FileName, count_added ) );
                    iCollectionConsumer.Add( creator.Result );
                }
            }

            iInfo.Message( String.Format( "Found files: {0}", count_added ) );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IEnumerable<RawAutorunItemBase> RegistryAutoStartFiles()
        {
            // 4 files
            var CU_parte = RawAutoRunSource.CreateFromRegPath( Microsoft.Win32.Registry.CurrentUser, 
                                                          @"Software\Microsoft\Windows\CurrentVersion\Run" );
            // 3 files
            var LM_parte = RawAutoRunSource.CreateFromRegPath( Microsoft.Win32.Registry.LocalMachine,
                                                        @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run" );
            // 1 file
            var LM_WOW6432Node_parte = RawAutoRunSource.CreateFromRegPath( Microsoft.Win32.Registry.LocalMachine,
                                            @"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Run" );

            return CU_parte.Concat( LM_parte ).Concat( LM_WOW6432Node_parte );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IEnumerable<RawAutorunItemBase> MenuAutoStartFiles( )
        {
            IEnumerable<RawAutorunItemBase> result = Enumerable.Empty<RawAutorunItemBase>();

            
            // c:\ProgramData\Microsoft\Windows\Start Menu\Programs\StartUp\
            var path = Environment.GetFolderPath( Environment.SpecialFolder.CommonApplicationData );
            path += @"\Microsoft\Windows\Start Menu\Programs\StartUp";

            result = RawAutoRunSource.CreateFromFS( path );
                                   
            // c:\Users\wce20\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\Startup\
            path = Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData );
            path += @"\Microsoft\Windows\Start Menu\Programs\StartUp";

            return result.Concat( RawAutoRunSource.CreateFromFS( path ) );
        }
    }
}
