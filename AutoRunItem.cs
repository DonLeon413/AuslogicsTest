using AuslogicsTest.Enums;
using AuslogicsTest.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AuslogicsTest
{
    /// <summary>
    /// File Info
    /// </summary>
    public class AutoRunItem
    {
        /// <summary>
        /// File name
        /// </summary>
        public String FileName
        {
            get;
            private set;
        }

        /// <summary>
        /// File path
        /// </summary>
        public String Path
        {
            get;
            private set;
        }

        /// <summary>
        /// CMD parameters
        /// </summary>
        public String Parameters
        {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        public ImageSource ImageSrc
        {
            get;
            private set;
        }

        private eFileInfoSource _Source;
        
        /// <summary>
        /// 
        /// </summary>
        public String Source
        {
            get
            {
                return this._Source.GUIName();
            }
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="path"></param>
        /// <param name="parameters"></param>
        /// <param name="source"></param>
        /// <param name="imageSource"></param>
        public AutoRunItem( String fileName, String path, String parameters, eFileInfoSource source, ImageSource imageSource = null )
        {
            this.FileName = fileName;
            this.Path = path;
            this.Parameters = parameters;
            this._Source = source;
            this.ImageSrc = imageSource;
        }
    }
}
