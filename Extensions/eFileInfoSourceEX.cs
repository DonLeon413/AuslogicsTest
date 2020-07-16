using AuslogicsTest.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuslogicsTest.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class eFileInfoSourceEX
    {
        /// <summary>
        /// 
        /// </summary>
        private static Dictionary<eFileInfoSource, String> _GUINames = new Dictionary<eFileInfoSource, String>()
        {
            { eFileInfoSource.Menu, "Menu" },
            { eFileInfoSource.Registry, "Registry" }
        };

        /// <summary>
        /// 
        /// </summary>
        private const String _DefaultGUIName = "Unknowm";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_this"></param>
        /// <returns></returns>
        public static String GUIName( this eFileInfoSource  _this )
        {
            return ( _GUINames.ContainsKey( _this ) ? _GUINames[_this] : _DefaultGUIName );    
        }
    }
}
