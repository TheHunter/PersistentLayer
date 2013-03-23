using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernate.DAL.NhPersistentLayer.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class QueryFormatException
        : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public QueryFormatException(string message)
            :base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public QueryFormatException(string message, Exception innerException)
            :base(message, innerException)
        {
        }
    }
}
