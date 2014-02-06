using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersistentLayer.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class InvalidIdentifierException
        : BusinessObjectException
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public InvalidIdentifierException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public InvalidIdentifierException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
