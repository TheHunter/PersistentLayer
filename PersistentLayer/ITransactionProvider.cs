using System.Data;

namespace PersistentLayer
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITransactionProvider
    {
        /// <summary>
        /// Indicates if a transaction is in progress.
        /// </summary>
        bool InProgress { get; }

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
