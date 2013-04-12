using System;

namespace PersistentLayer.Exceptions
{
    /// <summary>
    /// An exception which is thrown when a generic query is executed through DAO methods because of an failed persistent operation.
    /// </summary>
    public class BusinessPersistentException
        : ExecutionQueryException
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="methodInvoker"></param>
        public BusinessPersistentException(string message, string methodInvoker)
            :base(message, methodInvoker)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public BusinessPersistentException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="methodInvoker"></param>
        /// <param name="innerException"></param>
        public BusinessPersistentException(string message, string methodInvoker, Exception innerException)
            :base(message, methodInvoker, innerException)
        {
        }
    }
}
