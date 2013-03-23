using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernate.DAL.NhPersistentLayer.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class InnerRollBackException
        : SessionLayerException
    {
        private int _IndexTransaction = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public InnerRollBackException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public InnerRollBackException(string message, Exception exception)
            : base(message, exception)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public int IndexTransaction
        {
            internal set { this._IndexTransaction = value; }
            get { return this._IndexTransaction; }
        }
    }
}
