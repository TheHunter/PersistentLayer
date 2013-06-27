using System;

namespace PersistentLayer.Exceptions
{
    /// <summary>
    /// An exception which is thrown when a generic query is executed through DAO methods.
    /// </summary>
    public class ExecutionQueryException
        : BusinessObjectException
    {
        private string methodInvoker;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="methodInvoker"></param>
        public ExecutionQueryException(string message, string methodInvoker)
            :base(message)
        {
            this.methodInvoker = methodInvoker;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public ExecutionQueryException(string message, Exception innerException)
            :base(message, innerException)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="methodInvoker"></param>
        /// <param name="innerException"></param>
        public ExecutionQueryException(string message, string methodInvoker, Exception innerException)
            : base(message, innerException)
        {
            this.methodInvoker = methodInvoker;
        }

        /// <summary>
        /// 
        /// </summary>
        public string MethodInvoker
        {
            get { return this.methodInvoker; }
        }
    }
}
