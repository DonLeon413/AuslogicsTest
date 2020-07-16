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
    public class RawAutorunStringReg:
                 RawAutorunItemBase
    {
        /// <summary>
        /// 
        /// </summary>
        public String RawString
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rawString"></param>
        public RawAutorunStringReg( String rawString )
        {
            this.RawString = rawString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iVisitor"></param>
        public override void Accept( IRawAutorunItemVisitor iVisitor )
        {
            iVisitor.Visit( this );
        }
    }
}
