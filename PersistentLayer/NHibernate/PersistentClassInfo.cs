using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                //
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
    }
}
