using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernate.DAL.NhPersistentLayer.Exceptions
{
    /// <summary>
    /// An exception which is thrown when a generic query is executed through DAO methods because of an not valid argument
    /// </summary>
    public class QueryArgumentException
        : ExecutionQueryException
    {
        private string argumentName = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="methodInvoker"></param>
        /// <param name="argumentName"></param>
        public QueryArgumentException(string message, string methodInvoker, string argumentName)
            :base(message, methodInvoker)
        {
            this.argumentName = argumentName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="methodInvoker"></param>
        /// <param name="argumentName"></param>
        /// <param name="innerException"></param>
        public QueryArgumentException(string message, string methodInvoker, string argumentName, Exception innerException)
            :base(message, methodInvoker, innerException)
        {
            this.argumentName = argumentName;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ArgumentName
        {
            get { return this.argumentName; }
        }
    }
}
