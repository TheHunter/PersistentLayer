using System;

namespace PersistentLayer.Exceptions
{
    /// <summary>
    /// An exception which is thrown when a generic query is executed through DAO methods because of an not valid query syntax.
    /// </summary>
    public class QueryFormatException
        : ExecutionQueryException
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="methodInvoker"></param>
        public QueryFormatException(string message, string methodInvoker)
            :base(message, methodInvoker)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public QueryFormatException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="methodInvoker"></param>
        /// <param name="innerException"></param>
        public QueryFormatException(string message, string methodInvoker, Exception innerException)
            :base(message, methodInvoker, innerException)
        {
        }
    }
}
