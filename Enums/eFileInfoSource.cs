using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuslogicsTest.Enums
{
    /// <summary>
    /// 
    /// </summary>
    [Flags]
    public enum eFileInfoSource
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        Registry = 0x00000001,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        Menu = 0x00000002,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
