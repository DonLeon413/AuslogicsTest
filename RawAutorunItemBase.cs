using AuslogicsTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuslogicsTest
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class RawAutorunItemBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iVisitor"></param>
        public abstract void Accept( IRawAutorunItemVisitor iVisitor );
    }
}
