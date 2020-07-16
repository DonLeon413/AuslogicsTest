using AuslogicsTest.Interfaces;
using System;

namespace AuslogicsTest
{
    /// <summary>
    /// 
    /// </summary>
    public class RawAutorunLNK:
                RawAutorunItemBase
    {
        /// <summary>
        /// 
        /// </summary>
        public String FileName
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        public RawAutorunLNK( String fileName )
        {
            this.FileName = fileName;
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
