using System;
using System.Data;
using PersistentLayer.Exceptions;

namespace PersistentLayer
{
    /// <summary>
    /// 
    /// </summary>
    public interface ITransactionProvider
    {
        /// <summary>
        /// Indicates if the root transaction is in progress.
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
        /// <exception cref="BusinessLayerException"></exception>
        void BeginTransaction(IsolationLevel? level);

        /// <summary>
        /// Begin a new transaction with the given name.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="level"></param>
        /// <exception cref="BusinessLayerException"></exception>
        void BeginTransaction(string name, IsolationLevel? level);

        /// <summary>
        /// Commit the transaction.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="CommitFailedException">
        /// Throws an exception when current transaction tries to commit.
        /// </exception>
        void CommitTransaction();

        /// <summary>
        /// Makes a rollback the transaction.
        /// </summary>
        /// <exception cref="RollbackFailedException">
        /// Throws an exception when current transaction makes a rollback.
        /// </exception>
        /// <exception cref="InnerRollBackException">
        /// Throws an exception when an inner transaction makes a rollback.
        /// </exception>
        void RollbackTransaction();

        /// <summary>
        /// Makes a rollback, indicating the exception associated to the last transaction.
        /// </summary>
        /// <param name="cause"></param>
        void RollbackTransaction(Exception cause);
    }
}