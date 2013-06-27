using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersistentLayer.NHibernate.WCF
{
    /// <summary>
    /// 
    /// </summary>
    public class ServiceBehaviorException
        : WcfServiceException
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public ServiceBehaviorException(string message)
            :base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public ServiceBehaviorException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
