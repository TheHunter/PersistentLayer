using System.Data;

namespace PersistentLayer
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITransactionProvider
    {
        /// <summary>
        /// Indicates if there's at least a transaction in progress.
        /// </summary>
        bool InProgress { get; }

        /// <summary>
        /// Indicates if there's a transaction with the given name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool Exists(string name);
        
        /// <summary>
        /// Begin a new transaction
        /// </summary>
        /// <param name="level"></param>
        void BeginTransaction(IsolationLevel? level);

        /// <summary>
        /// Begin a new transaction with the given name.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="level"></param>
        void BeginTransaction(string name, IsolationLevel? level);

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
