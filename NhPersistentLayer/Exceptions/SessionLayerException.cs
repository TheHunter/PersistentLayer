using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernate.DAL.NhPersistentLayer.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class SessionLayerException
        : Exception
    {

        private string _InvokerName = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public SessionLayerException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public SessionLayerException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public string InvokerName
        {
            internal set { this._InvokerName = value; }
            get { return this._InvokerName; }
        }
    }
}
