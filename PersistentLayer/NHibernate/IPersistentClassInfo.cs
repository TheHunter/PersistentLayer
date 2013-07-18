using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

    }
}
