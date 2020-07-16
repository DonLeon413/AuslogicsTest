using AuslogicsTest.Interfaces;
using AuslogicsTest.Utils;
using IWshRuntimeLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace AuslogicsTest
{
    /// <summary>
    /// 
    /// </summary>
    public class AutoRunItemCreator:
                IRawAutorunItemVisitor
    {
        /// <summary>
        /// 
        /// </summary>
        public AutoRunItem Result
        {
            get;
            private set;
        }

        /// <summary>
        /// Ctor
        /// </summary>
        public AutoRunItemCreator( )
        {

        }

        #region IRawAutorunItemVisitor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stringRegObject"></param>
        public void Visit( RawAutorunStringReg stringRegObject )
        {
            this.Result = null;

            String str_command = stringRegObject.RawString.Trim();

            if( str_command[0] == '"' )
            {
                Int32 end_pos = str_command.IndexOf( '"', 1 );
                if( end_pos != -1 && end_pos >= 1 )
                {
                    String full_file_name = str_command.Substring( 1, end_pos - 1 );
                    String parameters = str_command.Substring( end_pos + 1 );

                    String file_name = Path.GetFileName( full_file_name );
                    String file_path = Path.GetDirectoryName( full_file_name );

                    if( false == String.IsNullOrWhiteSpace( file_name ) &&
                        false == String.IsNullOrWhiteSpace( file_path ) )
                    {
                        this.Result = new AutoRunItem( file_name, file_path, parameters,
                                                       Enums.eFileInfoSource.Registry,
                                                       IconUtils.GetAssociatedImage( full_file_name ) );
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expantStringRegObject"></param>
        public void Visit( RawAutorunExpandStringReg expantStringRegObject )
        {
            this.Result = null;

            String str_command = expantStringRegObject.RawString.Trim();

            Int32 separator_pos = str_command.IndexOf( ' ', 0 );

            String parameters = "";
            String full_file_name = "";

            if( separator_pos != -1 )
            { // with parameters
                parameters = str_command.Substring( separator_pos + 1 );
                full_file_name = str_command.Substring( 0, separator_pos - 1 );
            }
            else
            {
                full_file_name = str_command;
            }

            String file_name = Path.GetFileName( full_file_name );
            String file_path = Path.GetDirectoryName( full_file_name );

            if( false == String.IsNullOrWhiteSpace( file_name ) &&
                false == String.IsNullOrWhiteSpace( file_path ) )
            {
                this.Result = new AutoRunItem( file_name, file_path, parameters,
                                               Enums.eFileInfoSource.Registry,
                                               IconUtils.GetAssociatedImage( full_file_name ) );
            }
        }    

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rawAutorunLNKObject"></param>
        public void Visit( RawAutorunLNK rawAutorunLNKObject )
        {
            this.Result = null;

            // Default
            string full_file_name = rawAutorunLNKObject.FileName; 
            string parameters = "";

            try
            {
                IWshShortcut i_wsh_shorcut;
                WshShell shell = new WshShell();

                i_wsh_shorcut = (IWshShortcut)shell.CreateShortcut( rawAutorunLNKObject.FileName );

                parameters = i_wsh_shorcut.Arguments;
                full_file_name = i_wsh_shorcut.TargetPath;

            }catch( Exception ex )
            {
                // ???
                Debug.WriteLine( String.Format( "Error: {0}", ex.Message ) ); 
            }

            String file_name = Path.GetFileName( full_file_name );
            String path = Path.GetDirectoryName( full_file_name );

            this.Result = new AutoRunItem( file_name, path, parameters, 
                                           Enums.eFileInfoSource.Menu,
                                           IconUtils.GetAssociatedImage( full_file_name ) );
        }
        #endregion
    }
}
