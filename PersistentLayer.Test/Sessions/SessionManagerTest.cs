using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using NHibernate;
using NUnit.Framework;
using PersistentLayer.Domain;
using PersistentLayer.Exceptions;
using PersistentLayer.NHibernate;
using PersistentLayer.NHibernate.Impl;

namespace PersistentLayer.Test.Sessions
{
    /// <summary>
    /// 
    /// </summary>
    public class SessionManagerTest
        : CurrentTester
    {

        [Test]
        public void TestRightExecution1()
        {
            // this test calls other methods, in order for verify inner transactions..
            try
            {
                this.SessionProvider.BeginTransaction(IsolationLevel.ReadCommitted);

                this.Module1(1);
                this.Module2(1);
                this.Module3(1);

                this.SessionProvider.CommitTransaction();
            }
            catch (BusinessObjectException)
            {

            }
            catch (BusinessLayerException)
            {

            }
            catch (Exception)
            {
                
            }
            Assert.IsTrue(true);
        }

        [Test]
        public void TestWrongExecution1()
        {
            try
            {
                this.SessionProvider.BeginTransaction(IsolationLevel.ReadCommitted);

                this.Module1(1);
                this.Module2(-1);
                this.Module3(1);

                this.SessionProvider.CommitTransaction();
            }
            catch (BusinessObjectException)
            {
                Assert.IsTrue(true);
            }
            catch (BusinessLayerException)
            {
                Assert.IsTrue(false);
            }
            catch (Exception)
            {
                Assert.IsTrue(false);
            }
        }


        [Test]
        public void TestWrongExecution2()
        {
            try
            {
                this.SessionProvider.BeginTransaction(IsolationLevel.ReadCommitted);

                this.Module1(1);
                this.Module2(1);
                this.Module3WithRollBack(-1);

                this.SessionProvider.CommitTransaction();
            }
            catch (BusinessObjectException)
            {
                Assert.IsTrue(false);
            }
            catch (BusinessLayerException)
            {
                Assert.IsTrue(true);
            }
            catch (Exception)
            {
                Assert.IsTrue(false);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        internal void Module1(long? id)
        {
            try
            {
                this.SessionProvider.BeginTransaction(IsolationLevel.ReadCommitted);

                Salesman current = this.CurrentPagedDAO.FindBy<Salesman, long?>(id);

                this.SessionProvider.CommitTransaction();
            }
            catch (BusinessObjectException ex)
            {
                throw new BusinessObjectException("Error in Module1", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Unknown error on Module1", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        internal void Module2(long? id)
        {
            try
            {
                this.SessionProvider.BeginTransaction(IsolationLevel.ReadCommitted);

                TradeContract current = this.CurrentPagedDAO.FindBy<TradeContract, long?>(id);

                this.SessionProvider.CommitTransaction();
            }
            catch (BusinessObjectException ex)
            {
                throw new BusinessObjectException("Error in Module2", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Unknown error on Module2", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        internal void Module3(long? id)
        {
            try
            {
                this.SessionProvider.BeginTransaction(IsolationLevel.ReadCommitted);

                Agency current = this.CurrentPagedDAO.FindBy<Agency, long?>(id);

                this.SessionProvider.CommitTransaction();
            }
            catch (BusinessObjectException ex)
            {
                throw new BusinessObjectException("Error in Module3", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Unknown error on Module3", ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        internal void Module3WithRollBack(long? id)
        {
            try
            {
                this.SessionProvider.BeginTransaction(IsolationLevel.ReadCommitted);

                Agency current = this.CurrentPagedDAO.FindBy<Agency, long?>(id);

                this.SessionProvider.CommitTransaction();
            }
            catch (BusinessObjectException)
            {
                //throw new BusinessObjectException("Error in Module3", ex);
                this.SessionProvider.RollbackTransaction();
            }
            catch (Exception ex)
            {
                throw new Exception("Unknown error on Module3", ex);
            }
        }
    }
}
