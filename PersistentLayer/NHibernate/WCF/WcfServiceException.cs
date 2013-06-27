using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PersistentLayer.Exceptions;

namespace PersistentLayer.NHibernate.WCF
{
    /// <summary>
    /// 
    /// </summary>
    public class WcfServiceException
        : Exception

    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public WcfServiceException(string message)
            :base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public WcfServiceException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
