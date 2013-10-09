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
    public class ServiceArgumentException
        : WcfServiceException
    {
        private string parameter;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public ServiceArgumentException(string message)
            :base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="parameter"></param>
        public ServiceArgumentException(string message, string parameter)
            : base(message)
        {
            this.parameter = parameter;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public ServiceArgumentException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="parameter"></param>
        /// <param name="innerException"></param>
        public ServiceArgumentException(string message, string parameter, Exception innerException)
            : base(message, innerException)
        {
            this.parameter = parameter;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Parameter
        {
            get { return parameter; }
        }
    }
}
