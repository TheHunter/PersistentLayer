using System;
using PersistentLayer.NHibernate;

namespace PersistentLayer.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class RollbackFailedException
        : ContextRollbackException
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="transactionInfo"></param>
        public RollbackFailedException(string message, ITransactionInfo transactionInfo)
            : base(message, transactionInfo)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="cause"></param>
        /// <param name="transactionInfo"></param>
        public RollbackFailedException(string message, Exception cause, ITransactionInfo transactionInfo)
            : base(message, cause, transactionInfo)
        {
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="transactionInfo"></param>
        /// <param name="innerException"></param>
        public RollbackFailedException(string message, ITransactionInfo transactionInfo, Exception innerException)
            : base(message, transactionInfo, innerException)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="cause"></param>
        /// <param name="transactionInfo"></param>
        /// <param name="innerException"></param>
        public RollbackFailedException(string message, Exception cause, ITransactionInfo transactionInfo, Exception innerException)
            : base(message, cause, transactionInfo, innerException)
        {
        }
    }
}
