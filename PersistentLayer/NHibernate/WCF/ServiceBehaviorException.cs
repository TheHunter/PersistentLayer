using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersistentLayer.NHibernate.WCF
{
    /// <summary>
    /// 
    /// </summary>
    [Obsolete("In order to use the same exception, you might download the open source project by nuget or visit here for source code: https://github.com/TheHunter/WcfExtensions")]
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
