using System;
using System.Data;

namespace PersistentLayer
{
    /// <summary>
    /// Manages a common transaction.
    /// </summary>
    /// <seealso cref="PersistentLayer.ITransactionInfo" />
    public interface ITransactionWorker
        : ITransactionInfo, IDisposable
    {
        /// <summary>
        /// Gets the status of this instance.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        TransactionStatus Status { get; }

        /// <summary>
        /// Begins this instance.
        /// </summary>
        void Begin();

        /// <summary>
        /// Begins the specified isolation level.
        /// </summary>
        /// <param name="isolationLevel">The isolation level.</param>
        void Begin(IsolationLevel? isolationLevel);

        /// <summary>
        /// Commits this instance.
        /// </summary>
        void Commit();

        /// <summary>
        /// Rollbacks this instance.
        /// </summary>
        void Rollback();
    }
}
