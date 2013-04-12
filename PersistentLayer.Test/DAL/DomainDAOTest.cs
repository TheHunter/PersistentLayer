using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using System.IO;
using System.Xml;
using NUnit.Framework;
using System.Configuration;
using NHibernate.Context;
using NHibernate.Criterion;
using System.Data;
using PersistentLayer.Domain;
using PersistentLayer.Exceptions;
using PersistentLayer.NHibernate;
using PersistentLayer.NHibernate.Impl;
using PersistentLayer.Test.Wrappers;

namespace PersistentLayer.Test.DAL
{
    [TestFixture(Description = "Test for DAO's methods.")]
    public class DomainDAOTest
    {
        static ISessionFactory sessionFactory;
        NhConfigurationBuilder builder;
        INhPagedDAO ownPagedDAO;
        SessionManager sessionProvider;
        string rootPathProject;
        ISession currentSession;

        [TestFixtureSetUp]
        public void OnStartUp()
        {
            SetRootPathProject();

            XmlTextReader configReader = new XmlTextReader(new MemoryStream(Properties.Resources.Configuration));
            DirectoryInfo dir = new DirectoryInfo(this.rootPathProject + "MappingsXml");
            Console.WriteLine(dir);

            builder = new NhConfigurationBuilder(configReader, dir);

            builder.SetProperty("connection.connection_string", GetConnectionString());

            builder.BuildSessionFactory();
            sessionFactory = builder.SessionFactory;
            sessionProvider = new SessionManager(sessionFactory);
            ownPagedDAO = new EnterprisePagedDAO(sessionProvider);
            currentSession = sessionFactory.OpenSession();

            //CurrentSessionContext.Bind(currentSession);
        }

        [TestFixtureTearDown]
        public void OnFinishedTest()
        {
            if (currentSession != null && currentSession.IsOpen)
            {
                currentSession.Close();
                currentSession.Dispose();
                //CurrentSessionContext.Unbind(sessionFactory);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetRootPathProject()
        {
            var list = new List<string>(Directory.GetCurrentDirectory().Split('\\'));
            list.RemoveAt(list.Count - 1);
            list.RemoveAt(list.Count - 1);
            list.Add(string.Empty);
            this.rootPathProject = string.Join("\\", list);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetConnectionString()
        {
            string output = this.rootPathProject + "db\\";
            
            var str = ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString;
            return string.Format(str, output);
        }

        [SetUp]
        public void BindSession()
        {
            CurrentSessionContext.Bind(currentSession);
        }

        [TearDown]
        public void UnBindSession()
        {
            CurrentSessionContext.Unbind(sessionFactory);
        }

        [Test]
        public void LoadTest0()
        {
            Assert.IsNotNull(ownPagedDAO.FindBy<Salesman, long?>(1));
        }

        [Test]
        [ExpectedException(typeof(ExecutionQueryException))]
        public void FailedLoadTest0()
        {
            var cons = ownPagedDAO.FindBy<Salesman, long?>(-1);
            Assert.IsNotNull(cons);
        }

        [Test(Description = "Verify the right loading of object")]
        public void LoadTest1()
        {
            Assert.IsNotNull(ownPagedDAO.FindBy<Salesman, long?>(1, LockMode.Read));
            Assert.IsNotNull(ownPagedDAO.FindBy<Salesman, long?>(2, null));
        }

        [Test]
        [ExpectedException(typeof(ExecutionQueryException))]
        public void FailedLoad2Test()
        {
            Assert.IsNull(ownPagedDAO.FindBy<Salesman, long?>(-1, LockMode.Read));
        }

        [Test(Description="Verifies the loading of some instances.")]
        public void FindAllTest()
        {
            Assert.IsNotNull(ownPagedDAO.FindAll<Salesman>());
            // the follow test fails always because the static method indicated cannot be converted into Sql instruction
            // so it throws an exception when It's executed.
            //Assert.IsNotNull(ownPagedDAO.FindAll<Salesman>(n => string.IsNullOrEmpty(n.Email)));
            Assert.IsNotNull(ownPagedDAO.FindAll<Salesman>(n => n.Email == null || n.Email.Equals(string.Empty)));
            Assert.IsNotNull(ownPagedDAO.FindAll<Salesman>(true));
            Assert.IsNotNull(ownPagedDAO.FindAll<Salesman>(2));
            Assert.IsNotNull(ownPagedDAO.FindAll<Salesman>("ciccio"));
        }

        [Test(Description="Verifies if a valid detached criteria doesn't throw an exception")]
        public void FindAllDetachedCriteriaTest()
        {
            DetachedCriteria criteria = DetachedCriteria.For<Salesman>();
            criteria.Add(Restrictions.Like("Name", "Dav", MatchMode.Start));
            Assert.IsTrue(ownPagedDAO.FindAll<Salesman>(criteria).Count() > 0);
        }

        [Test]
        [ExpectedException(typeof(QueryArgumentException))]
        public void FailedFindAllDetachedCriteriaTest1()
        {
            ownPagedDAO.FindAll<Salesman>((DetachedCriteria)null);
        }

        [Test]
        [ExpectedException(typeof(ExecutionQueryException))]
        public void FailedFindAllDetachedCriteriaTest2()
        {
            DetachedCriteria criteria = DetachedCriteria.For<Salesman>();
            ownPagedDAO.FindAll<Agency>(criteria);
        }

        [Test]
        public void FindAllQueryOver()
        {
            QueryOver<Salesman> query = QueryOver.Of<Salesman>().Where(n => n.ID > 11);
            Assert.IsTrue(ownPagedDAO.FindAll(query).Count() > 0);
        }

        [Test]
        [ExpectedException(typeof(ExecutionQueryException))]
        public void FailedFindAllQueryOver()
        {
            // this query must throw an exception because the queryover instance has a projection result.
            QueryOver<Salesman> query = QueryOver.Of<Salesman>().Where(n => n.ID > 11).Select(n => n.ID, n => n.Name);
            Assert.IsTrue(ownPagedDAO.FindAll(query).Count() > 0);
        }

        [Test]
        public void FindAllFutureDetachedCriteriaTest()
        {
            DetachedCriteria criteria = DetachedCriteria.For<Salesman>().Add(Restrictions.Eq("ID", (long?)1));
            Assert.IsNotNull(ownPagedDAO.FindAllFuture<Salesman>(criteria).FirstOrDefault());
        }

        [Test]
        [ExpectedException(typeof(QueryArgumentException))]
        public void FailedFindAllFutureDetachedCriteriaTest()
        {
            ownPagedDAO.FindAllFuture<Salesman>((DetachedCriteria)null);
        }

        [Test]
        public void FindAllFutureQueryOverTest()
        {
            QueryOver<Salesman> query = QueryOver.Of<Salesman>().Where(n => n.ID > 11);
            Assert.IsNotNull(ownPagedDAO.FindAllFuture(query).FirstOrDefault());
        }

        [Test]
        public void GetFutureValueDetachedCriteriaTest()
        {
            DetachedCriteria criteria = DetachedCriteria.For<Salesman>().Add(Restrictions.IdEq(1));
            Assert.IsNotNull(ownPagedDAO.GetFutureValue<Salesman>(criteria).Value);
        }

        [Test(Description = "Test of GetFutureValue<TEntity, TResult> method")]
        public void GetFutureValueQueryOverTest()
        {
            QueryOver<Salesman> query = QueryOver.Of<Salesman>().Where(n => n.ID == 1);
            Assert.IsNotNull(ownPagedDAO.GetFutureValue<Salesman, Salesman>(query));
        }

        [Test]
        public void ExistsIDTest()
        {
            Assert.IsTrue(ownPagedDAO.Exists<Salesman, long?>(1));
            long?[] identifiers = new long?[]{ 1, 2};
            Assert.IsTrue(ownPagedDAO.Exists<Salesman, long?>(identifiers));
        }

        [Test]
        public void FailedExistsIDTest()
        {
            Assert.IsFalse(ownPagedDAO.Exists<Salesman, long?>(-1));
            long?[] identifiers = new long?[] { 1, -2 };
            Assert.IsFalse(ownPagedDAO.Exists<Salesman, long?>(identifiers));
        }

        [Test]
        [ExpectedException(typeof(QueryArgumentException))]
        public void FailedExistsIDsTest()
        {
            Assert.IsFalse(ownPagedDAO.Exists<Salesman, long?>((long?[])null));
        }

        [Test]
        public void ExistsDetachedCriteriaTest()
        {
            DetachedCriteria criteria = DetachedCriteria.For<Salesman>().Add(Restrictions.Eq("ID", (long)1));
            Assert.IsTrue(ownPagedDAO.Exists(criteria));
        }

        [Test]
        [ExpectedException(typeof(QueryArgumentException))]
        public void FailedExistsDetachedCriteriaTest()
        {
            // ReSharper disable RedundantCast
            Assert.IsTrue(ownPagedDAO.Exists((DetachedCriteria)null));
            // ReSharper restore RedundantCast
        }

        [Test]
        public void ExistsQueryOverTest()
        {
            QueryOver<Salesman> query = QueryOver.Of<Salesman>().Where(n => n.ID == 1);
            Assert.IsTrue(ownPagedDAO.Exists(query));
        }

        [Test]
        [ExpectedException(typeof(QueryArgumentException))]
        public void FailedExistsQueryOverTest()
        {
            Assert.IsFalse(ownPagedDAO.Exists((QueryOver<Salesman>)null));
        }

        [Test]
        public void ToIQueryableTest()
        {
            Assert.IsTrue(ownPagedDAO.ToIQueryable<Salesman>().Count() > 0);
            Assert.IsTrue(ownPagedDAO.ToIQueryable<Salesman>(CacheMode.Refresh).Count() > 0);
            Assert.IsTrue(ownPagedDAO.ToIQueryable<Salesman>("pages1").Count() > 0);
            Assert.IsTrue(ownPagedDAO.ToIQueryable<Salesman>(CacheMode.Refresh, "pages2").Count() > 0);
        }

        [Test]
        public void MakePersistentSaveTest()
        {
            sessionProvider.BeginTransaction(IsolationLevel.ReadCommitted);

            Salesman cons = new Salesman
                                  {
                                      Name = "Maria",
                                      Surname = "Bonita",
                                      IdentityCode = 450,
                                      Email = "chica_bonita@hotmail.com"
                                  };

            ownPagedDAO.MakePersistent(cons);
            Assert.IsTrue(ownPagedDAO.IsCached(cons));
            ownPagedDAO.Evict(cons);
            
            sessionProvider.RollbackTransaction();
        }

        [Test]
        [ExpectedException(typeof(BusinessPersistentException))]
        public void FailedMakePersistentSaveTest()
        {
            ReportSalesman rep = new ReportSalesman {ID = 10, Name = "Ciccio"};
            ownPagedDAO.MakePersistent(rep);
        }

        [Test]
        [ExpectedException(typeof(BusinessPersistentException))]
        public void FailedMakePersistentSave2Test()
        {
            /*
             * NOTA:
             * This test tries to save an transient instance which properties state is copied by a persistent instance.
             * So.. in a few words, the calling of MakePersistent method tries to update the given instance (if this one is persistent)
             * otherwise MakePersistent method tries to save it, but It can throw an exception if the given transient state instance 
             * is equals to an existing persistent state into data store.
             */

            sessionProvider.BeginTransaction(IsolationLevel.ReadCommitted);
            Salesman cons = ownPagedDAO.ToIQueryable<Salesman>().First();
            Salesman cons2 = new Salesman
                                   {
                                       ID = cons.ID,
                                       IdentityCode = cons.IdentityCode,
                                       Name = cons.Name,
                                       Surname = cons.Surname,
                                       Email = cons.Email
                                   };

            cons2.UpdateVersion(cons);
            try
            {
                ownPagedDAO.MakePersistent(cons2);
                sessionProvider.CommitTransaction();
            }
            catch (Exception)
            {
                sessionProvider.RollbackTransaction();
                throw;
            }
        }

        [Test]
        public void MakePersistentUpdateTest()
        {
            try
            {
                sessionProvider.BeginTransaction(IsolationLevel.ReadCommitted);
                Salesman cons = ownPagedDAO.ToIQueryable<Salesman>().Where(n => n.ID == 11).First();
                Assert.IsNotNull(cons);
                Assert.IsTrue(ownPagedDAO.IsCached(cons));
                string oldEmail = cons.Email;
                string newEmail = string.Format("{0}_.{1}_@gmail.com", cons.Name, cons.Surname);
                cons.Email = newEmail;
                sessionProvider.CommitTransaction();

                sessionProvider.BeginTransaction(IsolationLevel.ReadCommitted);
                Salesman cons1 = ownPagedDAO.FindBy<Salesman, long?>(cons.ID, LockMode.Upgrade);
                cons1.Email = oldEmail;
                sessionProvider.CommitTransaction();
            }
            catch (Exception)
            {
                sessionProvider.RollbackTransaction();
                throw;
            }
        }

        [Test]
        [ExpectedException(typeof(BusinessPersistentException))]
        public void FailedMakePersistentUpdateTest()
        {
            try
            {
                sessionProvider.BeginTransaction(IsolationLevel.ReadCommitted);
                Salesman cons = ownPagedDAO.ToIQueryable<Salesman>().Where(n => n.ID == 11).First();
                cons.Email = "test_email";

                /*
                 * the calling of this method fails because It tries to update an persistent instance reference (cons),
                 * associated with the given identifier(11).
                 * So, in order to use this method, the instance to update must be transient or detached, and naturally
                 * the given indentifier must exists in data store.
                 */
                ownPagedDAO.MakePersistent(cons, 11);

                sessionProvider.CommitTransaction();
            }
            catch (Exception)
            {
                sessionProvider.RollbackTransaction();
                throw;
            }
        }

        [Test]
        [ExpectedException(typeof(QueryArgumentException))]
        public void FailedMakePersistentCollectionTest()
        {
            ownPagedDAO.MakePersistent((IEnumerable<Salesman>)null);
        }

        [Test]
        [ExpectedException(typeof(QueryArgumentException))]
        public void FailedMakeTransientTest()
        {
            ownPagedDAO.MakeTransient((Salesman)null);
        }

        [Test]
        [ExpectedException(typeof(QueryArgumentException))]
        public void FailedMakeTransientCollectionTest()
        {
            ownPagedDAO.MakeTransient((IEnumerable<Salesman>)null);
        }

        [Test]
        [ExpectedException(typeof(QueryArgumentException))]
        public void FailedRefreshStateTest()
        {
            ownPagedDAO.RefreshState((Salesman)null);
        }

        [Test]
        [ExpectedException(typeof(QueryArgumentException))]
        public void FailedRefreshStateCollectionTest()
        {
            ownPagedDAO.RefreshState((IEnumerable<Salesman>)null);
        }

        [Test]
        public void GetPagedResultTest1()
        {
            DetachedCriteria criteria = DetachedCriteria.For<Salesman>().Add(Restrictions.Gt("ID", (long)1));
            IPagedResult<Salesman> result =  ownPagedDAO.GetPagedResult<Salesman>(1, 5, criteria);
            Assert.IsTrue(result.Counter > 0);
        }

        [Test]
        [ExpectedException(typeof(QueryArgumentException))]
        public void FailedGetPagedResult1()
        {
            ownPagedDAO.GetPagedResult<Salesman>(1, 5, (DetachedCriteria)null);
        }

        [Test]
        public void GetPagedResultTest2()
        {
            IPagedResult<Salesman> result = ownPagedDAO.GetPagedResult(1, 5, QueryOver.Of<Salesman>().Where(n => n.ID > 1));
            Assert.IsTrue(result.Counter > 0);
        }

        [Test]
        [ExpectedException(typeof(QueryArgumentException))]
        public void FailedGetPagedResult2()
        {
            ownPagedDAO.GetPagedResult(1, 5, (QueryOver<Salesman>)null);
        }

        [Test]
        public void IsCachedTest()
        {
            var cons = ownPagedDAO.FindBy<Salesman, long?>(1, null);
            Assert.IsTrue(ownPagedDAO.IsCached(cons));
            Assert.IsFalse(ownPagedDAO.IsCached((Salesman)null));
        }

        [Test]
        public void GetIdentifierTest()
        {
            long? id = 1;
            var cons = ownPagedDAO.FindBy<Salesman, long?>(id, null);
            Assert.IsNotNull(ownPagedDAO.GetIdentifier<Salesman, long?>(cons));
        }

        [Test]
        [ExpectedException(typeof(QueryArgumentException))]
        public void FailedGetIdentifierTest()
        {
            ownPagedDAO.GetIdentifier<Salesman, long?>(null);
        }

        [Test]
        public void SessionWithChangesTest()
        {
            long? id = 1;
            var cons = ownPagedDAO.FindBy<Salesman, long?>(id, null);

            string oldEmail = cons.Email;
            cons.Email = "ciao_email";
            Assert.IsTrue(ownPagedDAO.SessionWithChanges());

            cons.Email = oldEmail;
            Assert.IsFalse(ownPagedDAO.SessionWithChanges());
        }

        [Test]
        public void EvictTest()
        {
            long? id = 1;
            var cons = ownPagedDAO.FindBy<Salesman, long?>(id, null);

            Assert.IsTrue(ownPagedDAO.IsCached(cons));
            ownPagedDAO.Evict(cons);

            Assert.IsFalse(ownPagedDAO.IsCached(cons));
        }

        [Test]
        public void EvictCollectionTest()
        {
            var col = ownPagedDAO.FindAll<Salesman>(DetachedCriteria.For<Salesman>().Add(Restrictions.Gt("ID", (long?) 1)));
            
            Assert.IsTrue(ownPagedDAO.IsCached(col));
            ownPagedDAO.Evict(col);

            Assert.IsFalse(ownPagedDAO.IsCached(col));
        }

        [Test]
        public void EvictAllTest()
        {
            var col = ownPagedDAO.FindAll<Salesman>(DetachedCriteria.For<Salesman>());
            Assert.IsTrue(ownPagedDAO.IsCached(col));
            ownPagedDAO.Evict();

            Assert.IsFalse(ownPagedDAO.IsCached(col));
        }

        [Test]
        public void TransformResultQueryOverTest()
        {
            QueryOver<Salesman> query = QueryOver.Of<Salesman>()
                .Select(
                    Projections.ProjectionList()
                    .Add(Projections.Property("ID"), "ID")
                    .Add(Projections.Property("Name"), "Name")
                    .Add(Projections.Property("Surname"), "Surname")
                    .Add(Projections.Property("Email"), "Email")
                );
            var col = ownPagedDAO.TransformResult<Salesman, SalesmanPrj>(query);
            Assert.IsTrue(col.Any());

        }

        //

        [Test]
        public void ToProjectOverTest()
        {
            QueryOver<Salesman> query = QueryOver.Of<Salesman>()
                .Select(
                    Projections.ProjectionList()
                    .Add(Projections.Property("ID"), "ID")
                    .Add(Projections.Property("Name"), "Name")
                    .Add(Projections.Property("Surname"), "Surname")
                    .Add(Projections.Property("Email"), "Email")
                );
            var col = ownPagedDAO.ToProjectOver(query);
            Assert.IsTrue(col.Any());
        }

        [Test]
        public void TransformFutureResultTest()
        {
            QueryOver<Salesman> query = QueryOver.Of<Salesman>()
                .Select(
                    Projections.ProjectionList()
                    .Add(Projections.Property("ID"), "ID")
                    .Add(Projections.Property("Name"), "Name")
                    .Add(Projections.Property("Surname"), "Surname")
                    .Add(Projections.Property("Email"), "Email")
                );
            var col = ownPagedDAO.TransformFutureResult<Salesman, SalesmanPrj>(query);
            var count = ownPagedDAO.GetFutureValue<Salesman, int>(query.ToRowCountQuery());
            
            Assert.IsTrue(count.Value > 0 && col.Any());
        }

        [Test]
        public void ToProjectOverFutureTest()
        {
            QueryOver<Salesman> query = QueryOver.Of<Salesman>()
                .Select(
                    Projections.ProjectionList()
                    .Add(Projections.Property("ID"), "ID")
                    .Add(Projections.Property("Name"), "Name")
                    .Add(Projections.Property("Surname"), "Surname")
                    .Add(Projections.Property("Email"), "Email")
                );

            var col = ownPagedDAO.ToProjectOverFuture(query);
            var count = ownPagedDAO.GetFutureValue<Salesman, int>(query.ToRowCountQuery());

            Assert.IsTrue(count.Value > 0 && col.Any());
        }

        [Test]
        public void TransformResultDetachedCriteriaTest()
        {
            DetachedCriteria criteria = DetachedCriteria.For<Salesman>()
                .SetProjection
                (
                    Projections.ProjectionList()
                        .Add(Projections.Property("ID"), "ID")
                        .Add(Projections.Property("Name"), "Name")
                        .Add(Projections.Property("Surname"), "Surname")
                        .Add(Projections.Property("Email"), "Email")
                );

            var col = ownPagedDAO.TransformResult<SalesmanPrj>(criteria);
            Assert.IsTrue(col.Any());
        }

        [Test]
        public void ToProjectOverDetachedCriteriaTest()
        {
            DetachedCriteria criteria = DetachedCriteria.For<Salesman>()
                .SetProjection
                (
                    Projections.ProjectionList()
                        .Add(Projections.Property("ID"), "ID")
                        .Add(Projections.Property("Name"), "Name")
                        .Add(Projections.Property("Surname"), "Surname")
                        .Add(Projections.Property("Email"), "Email")
                );

            var col = ownPagedDAO.ToProjectOver(criteria);
            Assert.IsTrue(col.Any());
        }

        [Test]
        public void TransformFutureResultDetachedCriteriaTest()
        {
            DetachedCriteria criteria = DetachedCriteria.For<Salesman>()
                .SetProjection
                (
                    Projections.ProjectionList()
                        .Add(Projections.Property("ID"), "ID")
                        .Add(Projections.Property("Name"), "Name")
                        .Add(Projections.Property("Surname"), "Surname")
                        .Add(Projections.Property("Email"), "Email")
                );

            var col = ownPagedDAO.TransformFutureResult<SalesmanPrj>(criteria);
            var count = ownPagedDAO.GetFutureValue<int>(CriteriaTransformer.TransformToRowCount(criteria));
            Assert.IsTrue(count.Value > 0 && col.Any());
        }

        [Test]
        public void ToProjectOverFutureDetachedCriteriaTest()
        {
            DetachedCriteria criteria = DetachedCriteria.For<Salesman>()
                .SetProjection
                (
                    Projections.ProjectionList()
                        .Add(Projections.Property("ID"), "ID")
                        .Add(Projections.Property("Name"), "Name")
                        .Add(Projections.Property("Surname"), "Surname")
                        .Add(Projections.Property("Email"), "Email")
                );

            var col = ownPagedDAO.ToProjectOverFuture(criteria);
            var count = ownPagedDAO.GetFutureValue<int>(CriteriaTransformer.TransformToRowCount(criteria));
            Assert.IsTrue(count.Value > 0 && col.Any());
        }
    }
}
