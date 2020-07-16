using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuslogicsTest
{
    /// <summary>
    /// 
    /// </summary>
    public class RawAutoRunSource
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="regKey"></param>
        /// <param name="keyPath"></param>
        /// <returns></returns>
        public static IEnumerable<RawAutorunItemBase> CreateFromRegPath( RegistryKey regKey, String keyPath )
        {
            using( var reg_key = regKey.OpenSubKey( keyPath ) )
            {
                if( null != reg_key )
                {
                    foreach( var name in reg_key.GetValueNames() )
                    {
                        RawAutorunItemBase new_raw_obj = null;
                        String value = null;
                        RegistryValueKind type_value;

                        try
                        {
                            var raw_value = reg_key.GetValue( name );
                            type_value = reg_key.GetValueKind( name );

                            if( null != raw_value && raw_value is String )
                            {
                                value = raw_value as String;
                            }
                        }
                        catch( Exception ex )
                        {
                            Debug.WriteLine( String.Format( "Error: {0}", ex.Message ) );
                            continue; // Продолжаем поиск дальше :(
                        }

                        if( false == String.IsNullOrWhiteSpace( value ) )
                        {
                            switch( type_value )
                            {
                                case RegistryValueKind.String:
                                {
                                    new_raw_obj = new RawAutorunStringReg( value );
                                    break;
                                }

                                case RegistryValueKind.ExpandString:
                                {
                                    new_raw_obj = new RawAutorunExpandStringReg( value );
                                    break;
                                }
                            }

                            if( null != new_raw_obj )
                            {
                                yield return new_raw_obj;
                            }
#if DEBUG
                            else
                            {
                                Debug.WriteLine( String.Format( "WARNING: unknown type: {0}", type_value.ToString() ) );
                            }
#endif
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="mask"></param>
        /// <returns></returns>
        public static IEnumerable<RawAutorunItemBase> CreateFromFS( String path, String mask = null )
        {
            path = path.Trim();
            if( false == String.IsNullOrWhiteSpace( path ) )
            {

                if( String.IsNullOrWhiteSpace( mask ) )
                {
                    mask = "*.lnk";
                }

                String[] files = new String[0];

                try
                {
                    files = Directory.GetFiles( path, mask );
                }
                catch( Exception ex )
                {
                    Debug.WriteLine( String.Format( "ERROR: {0}", ex.Message ) );
                }

                foreach( var file in files )
                {
                    yield return new RawAutorunLNK( file );
                }
            }
        }
    }
}
