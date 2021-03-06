﻿using System;

namespace PersistentLayer.Exceptions
{
    /// <summary>
    /// Rappresents an generic error when business DAOs execute any kind of query on data store.
    /// </summary>
    public class BusinessObjectException
        : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public BusinessObjectException(string message)
            :base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public BusinessObjectException(string message, Exception innerException)
            :base(message, innerException)
        {
        }
    }
}
