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
    /// <typeparam name="T"></typeparam>
    public interface IMultiThreadObservableCollectionConsumer<T>                     
    {
        /// <summary>
        /// 
        /// </summary>
        void Clear( );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newItem"></param>
        void Add( T newItem );
    }
}
