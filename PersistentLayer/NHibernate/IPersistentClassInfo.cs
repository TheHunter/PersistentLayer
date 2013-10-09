using System;
using System.Collections;
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
        /// <param name="mode"></param>
        /// <returns></returns>
        Type GetMappedClass(EntityMode mode);

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
        /// <param name="mode"></param>
        /// <returns></returns>
        object GetIdentifier(object instance, EntityMode mode);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="id"></param>
        /// <param name="mode"></param>
        void SetIdentifier(object instance, object id, EntityMode mode);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="propertyName"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        object GetPropertyValue(object instance, string propertyName, EntityMode mode);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="propertyNames"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        object[] GetPropertyValues(object instance, string[] propertyNames, EntityMode mode);

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
