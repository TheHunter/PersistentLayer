using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace NHibernate.DAL.NhPersistentLayer
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITransactionProvider
    {
        /// <summary>
        /// Begin a new transaction
        /// </summary>
        /// <param name="level"></param>
        void BeginTransaction(IsolationLevel? level);

        /// <summary>
        /// Commit the transaction.
        /// </summary>
        /// <returns></returns>
        void CommitTransaction();

        /// <summary>
        /// Makes a rollback the transaction.
        /// </summary>
        void RollbackTransaction();
    }
}
