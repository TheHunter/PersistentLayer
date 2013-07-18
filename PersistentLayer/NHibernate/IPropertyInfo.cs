using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PersistentLayer.NHibernate
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPropertyInfo
    {
        /// <summary>
        /// 
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        Type TypeValue { get; }

        /// <summary>
        /// 
        /// </summary>
        bool IsMutable { get; }

        /// <summary>
        /// 
        /// </summary>
        bool IsAssociationType { get; }

        /// <summary>
        /// 
        /// </summary>
        bool IsXMLElement { get; }
        
        /// <summary>
        /// 
        /// </summary>
        bool IsCollectionType { get; }
        
        /// <summary>
        /// 
        /// </summary>
        bool IsComponentType { get; }
        
        /// <summary>
        /// 
        /// </summary>
        bool IsEntityType { get; }
        
        /// <summary>
        /// 
        /// </summary>
        bool IsAnyType { get; }
    }
}
