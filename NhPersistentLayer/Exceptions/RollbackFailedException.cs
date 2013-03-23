using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernate.DAL.NhPersistentLayer.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class RollbackFailedException
        : SessionLayerException
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public RollbackFailedException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public RollbackFailedException(string message, Exception exception)
            : base(message, exception)
        {
        }
    }
}
