using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Metadata;
using NHibernate.Type;
using PersistentLayer.Exceptions;

namespace PersistentLayer.NHibernate
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class PersistentClassInfo
        : IPersistentClassInfo
    {
        private readonly IClassMetadata metadata;
        private readonly IPropertyInfo identifier;
        private readonly ICollection<IPropertyInfo> properties;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="metadata"></param>
        internal PersistentClassInfo(IClassMetadata metadata)
        {
            if (metadata == null)
                throw new BusinessLayerException("The metadata info of persistent class cannot be null.");

            //metadata.SetIdentifier();
            //metadata.SetPropertyValue();
            
            this.metadata = metadata;
            this.properties = new HashSet<IPropertyInfo>();
            
            try
            {
                string idName = metadata.IdentifierPropertyName;
                IType type = metadata.GetPropertyType(idName);

                this.identifier = new PropertyMapInfo(type, idName);
            }
            catch (Exception)
            {
                //metadata.GetPropertyValuesToInsert()
            }

            metadata.PropertyNames.All
                (
                    property =>
                        {
                            try
                            {
                                IType type = metadata.GetPropertyType(property);
                                PropertyMapInfo current = new PropertyMapInfo(type, property);
                                properties.Add(current);
                            }
                            catch (Exception)
                            {
                                //
                            }
                            return true;
                        }
                );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public Type GetMappedClass(EntityMode mode)
        {
            return this.metadata.GetMappedClass(mode);
        }

        /// <summary>
        /// 
        /// </summary>
        public IPropertyInfo Identifier
        {
            get { return identifier; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsInherited
        {
            get { return metadata.IsInherited; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool HasProxy
        {
            get { return metadata.HasProxy; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool HasIdentifierProperty
        {
            get { return metadata.HasIdentifierProperty; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool HasNaturalIdentifier
        {
            get { return metadata.HasNaturalIdentifier; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool HasSubclasses
        {
            get { return metadata.HasSubclasses; }
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<IPropertyInfo> Properties
        {
            get { return properties; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool ExistsProperty(string name)
        {
            if (name == null || name.Trim() == string.Empty)
                return false;

            return this.Properties.Any(n => n.Name == name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="id"></param>
        /// <param name="mode"></param>
        public void SetIdentifier(object instance, object id, EntityMode mode)
        {
            metadata.SetIdentifier(instance, id, mode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public object GetIdentifier(object instance, EntityMode mode)
        {
            return metadata.GetIdentifier(instance, mode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="propertyName"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public object GetPropertyValue(object instance, string propertyName, EntityMode mode)
        {
            if (instance == null)
                throw new BusinessObjectException("The instance to get property value cannot be null.");

            return this.GetProperty(instance, propertyName, mode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="propertyNames"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public object[] GetPropertyValues(object instance, string[] propertyNames, EntityMode mode)
        {
            if (instance == null)
                throw new BusinessObjectException("The instance to get property value cannot be null.");

            if (propertyNames == null)
                throw new BusinessObjectException("PropertyNames cannot be null");

            return propertyNames.Select(propertyName => this.GetProperty(instance, propertyName, mode)).ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <param name="mode"></param>
        /// <exception cref="BusinessObjectException"></exception>
        /// <exception cref="MissingPropertyException"></exception>
        public void SetPropertyValue(object instance, string propertyName, object value, EntityMode mode)
        {
            if (instance == null)
                throw new BusinessObjectException("The instance to set property value cannot be null.");

            this.SetProperty(instance, propertyName, value, mode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="values"></param>
        /// <param name="mode"></param>
        /// <exception cref="BusinessObjectException"></exception>
        /// <exception cref="MissingPropertyException"></exception>
        public void SetPropertyValues(object instance, IEnumerable<KeyValuePair<string, object>> values, EntityMode mode)
        {
            if (instance == null)
                throw new BusinessObjectException("The instance to set property values cannot be null.");

            if (values == null)
                throw new BusinessObjectException("Collection with property values cannot be null.");

            foreach (var value in values)
            {
                this.SetProperty(instance, value.Key, value.Value, mode);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <param name="mode"></param>
        /// <exception cref="MissingPropertyException"></exception>
        private void SetProperty(object instance, string propertyName, object value, EntityMode mode)
        {
            if (!this.ExistsProperty(propertyName))
                throw new MissingPropertyException("The property to set wasn't founded", propertyName);
            
            this.metadata.SetPropertyValue(instance, propertyName, value, mode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="propertyName"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        private object GetProperty(object instance, string propertyName, EntityMode mode)
        {
            if (!this.ExistsProperty(propertyName))
                throw new MissingPropertyException("The property to get value wasn't found", propertyName);

            return this.metadata.GetPropertyValue(instance, propertyName, mode);
        }
    }
}
