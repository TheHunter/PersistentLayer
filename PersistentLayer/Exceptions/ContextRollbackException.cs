using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PersistentLayer.NHibernate;

namespace PersistentLayer.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class ContextRollbackException
        : BusinessLayerException
    {
        private readonly ITransactionInfo transactionInfo;
        private readonly Exception cause;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="transactionInfo"></param>
        public ContextRollbackException(string message, ITransactionInfo transactionInfo)
            : base(message, "RollbackTransaction")
        {
            this.transactionInfo = transactionInfo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="cause"></param>
        /// <param name="transactionInfo"></param>
        public ContextRollbackException(string message, Exception cause, ITransactionInfo transactionInfo)
            : base(message, "RollbackTransaction")
        {
            this.transactionInfo = transactionInfo;
            this.cause = cause;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="transactionInfo"></param>
        /// <param name="innerException"></param>
        public ContextRollbackException(string message, ITransactionInfo transactionInfo, Exception innerException)
            : base(message, "RollbackTransaction", innerException)
        {
            this.transactionInfo = transactionInfo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="cause"></param>
        /// <param name="transactionInfo"></param>
        /// <param name="innerException"></param>
        public ContextRollbackException(string message, Exception cause, ITransactionInfo transactionInfo, Exception innerException)
            : base(message, "RollbackTransaction", innerException)
        {
            this.transactionInfo = transactionInfo;
            this.cause = cause;
        }

        /// <summary>
        /// 
        /// </summary>
        public ITransactionInfo TransactionInfo { get { return this.transactionInfo; } }

        /// <summary>
        /// 
        /// </summary>
        public Exception Cause { get { return this.cause; } }
            
    }
}
