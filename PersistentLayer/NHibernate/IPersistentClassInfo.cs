using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using PersistentLayer.Exceptions;

namespace PersistentLayer.NHibernate
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPersistentClassInfo
    {
        /// <summary>
        /// 
        /// </summary>
        IPropertyInfo Identifier { get; }

        /// <summary>
        /// 
        /// </summary>
        bool IsInherited { get; }

        /// <summary>
        /// 
        /// </summary>
        bool HasProxy { get; }

        /// <summary>
        /// 
        /// </summary>
        bool HasIdentifierProperty { get; }

        /// <summary>
        /// 
        /// </summary>
        bool HasNaturalIdentifier { get; }

        /// <summary>
        /// 
        /// </summary>
        bool HasSubclasses { get; }

        /// <summary>
        /// 
        /// </summary>
        IEnumerable<IPropertyInfo> Properties { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        bool ExistsProperty(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <param name="mode"></param>
        /// <exception cref="BusinessObjectException"></exception>
        /// <exception cref="MissingPropertyException"></exception>
        void SetPropertyValue(object instance, string propertyName, object value, EntityMode mode);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="values"></param>
        /// <param name="mode"></param>
        /// <exception cref="BusinessObjectException"></exception>
        /// <exception cref="MissingPropertyException"></exception>
        void SetPropertyValues(object instance, IEnumerable<KeyValuePair<string, object>> values, EntityMode mode);

    }
}
