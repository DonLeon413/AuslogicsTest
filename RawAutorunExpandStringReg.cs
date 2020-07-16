using AuslogicsTest.Interfaces;
using System;

namespace AuslogicsTest
{
    /// <summary>
    /// 
    /// </summary>
    public class RawAutorunExpandStringReg:
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
        public RawAutorunExpandStringReg( String rawString )
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
