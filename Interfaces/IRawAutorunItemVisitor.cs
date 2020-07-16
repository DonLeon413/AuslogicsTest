using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuslogicsTest.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRawAutorunItemVisitor
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stringRegObject"></param>
        void Visit( RawAutorunStringReg stringRegObject );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expantStringRegObject"></param>
        void Visit( RawAutorunExpandStringReg expantStringRegObject );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rawAutorunLNKObject"></param>
        void Visit( RawAutorunLNK rawAutorunLNKObject );
    }
}
