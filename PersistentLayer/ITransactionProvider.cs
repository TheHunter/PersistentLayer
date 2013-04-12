using System.Data;

namespace PersistentLayer
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
