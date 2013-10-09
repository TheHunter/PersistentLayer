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
    [Obsolete("In order to use the same exception, you might download the open source project by nuget or visit here for source code: https://github.com/TheHunter/WcfExtensions")]
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
