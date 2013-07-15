using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersistentLayer.NHibernate
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITransactionInfo
    {
        /// <summary>
        /// 
        /// </summary>
        int Index { get; }

        /// <summary>
        /// 
        /// </summary>
        string Name { get; }
    }
}
