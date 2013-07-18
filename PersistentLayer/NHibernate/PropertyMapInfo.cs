using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Type;
using PersistentLayer.Exceptions;

namespace PersistentLayer.NHibernate
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class PropertyMapInfo
        : IPropertyInfo
    {
        private readonly IType type;
        private readonly string name;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        internal PropertyMapInfo(IType type, string name)
        {
            if (type == null)
                throw new BusinessLayerException("The object Type cannot be null.");

            if (name == null || name.Trim().Equals(string.Empty))
                throw new BusinessLayerException("The property name cannot be null or empty.");

            this.type = type;
            this.name = name;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get { return name; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Type TypeValue
        {
            get { return type.ReturnedClass; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsMutable
        {
            get { return type.IsMutable; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsAssociationType
        {
            get { return type.IsAssociationType; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsXMLElement
        {
            get { return type.IsXMLElement; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsCollectionType
        {
            get { return type.IsCollectionType; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsComponentType
        {
            get { return type.IsComponentType; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsEntityType
        {
            get { return type.IsEntityType; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsAnyType
        {
            get { return type.IsAnyType; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is PropertyMapInfo)
                return this.GetHashCode() == obj.GetHashCode();

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return this.name.GetHashCode() - this.type.GetHashCode();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Property Name: {0}, Type info: {1}", this.name, this.TypeValue.FullName);
        }
    }
}
