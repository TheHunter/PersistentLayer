using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernate.DAL.NhPersistentLayer
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITransactionContext
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ITransactionProvider GetTransactionProvider();
    }
}
