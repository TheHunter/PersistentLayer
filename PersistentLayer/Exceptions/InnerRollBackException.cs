using System;
using PersistentLayer.NHibernate;

namespace PersistentLayer.Exceptions
{
    /// <summary>
    /// Indicates an error has occurred when an inner transaction has invoked a rollback method.
    /// </summary>
    public class InnerRollBackException
        : ContextRollbackException
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="transactionInfo"></param>
        public InnerRollBackException(string message, ITransactionInfo transactionInfo)
            : base(message, transactionInfo)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="cause"></param>
        /// <param name="transactionInfo"></param>
        public InnerRollBackException(string message, Exception cause, ITransactionInfo transactionInfo)
            : base(message, cause, transactionInfo)
        {
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="transactionInfo"></param>
        /// <param name="innerException"></param>
        public InnerRollBackException(string message, ITransactionInfo transactionInfo, Exception innerException)
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
        public InnerRollBackException(string message, Exception cause, ITransactionInfo transactionInfo, Exception innerException)
            : base(message, cause, transactionInfo, innerException)
        {
        }

    }
}
