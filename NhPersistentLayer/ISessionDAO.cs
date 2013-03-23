using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.DAL.NhPersistentLayer.Imp;

namespace NHibernate.DAL.NhPersistentLayer
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="VKey"></typeparam>
    public interface ISessionDAO<TEntity, VKey>
        : IEntityDAO<TEntity, VKey>, ISessionContext, ITransactionContext
        where TEntity : class
    {
    }

    /// <summary>
    /// 
    /// </summary>
    public interface ISessionDAO
        : IDomainDAO, ISessionContext, ITransactionContext
    {
    }
}
