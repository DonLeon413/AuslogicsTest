using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace AuslogicsTest.Utils
{
    /// <summary>
    /// 
    /// </summary>
    internal static class IconUtils
    {
        /// <summary>
        /// Get BitmapImage associated
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static BitmapSource GetAssociatedImage( String fileName )
        {
            System.Drawing.Icon sys_icon = null;
            BitmapSource bmp_source = null;
            try
            {
                sys_icon = System.Drawing.Icon.ExtractAssociatedIcon( fileName );
                bmp_source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon( sys_icon.Handle,
                                                                System.Windows.Int32Rect.Empty,
                                                System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions() );
                if( bmp_source.CanFreeze )
                {
                    bmp_source.Freeze();
                }
            }
            catch( Exception )
            {
                throw;
            }
            finally
            {
                if( null != sys_icon )
                {
                    sys_icon.Dispose();
                }
            }

            return bmp_source;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static BitmapSource GetAssociatedImageSAFE( String fileName )
        {
            try
            {
                return IconUtils.GetAssociatedImage( fileName );
            }
            catch( Exception ex )
            {   // ???
                Debug.WriteLine( String.Format( "Error: {0}", ex.Message ) );
            }

            return null;
        }
    }
}
