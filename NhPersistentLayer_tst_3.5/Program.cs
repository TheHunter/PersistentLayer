using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Linq;
using NHibernate.DAL.NhPersistentLayer;
using NHibernate.DAL.NhPersistentLayer.Imp;
using NHibernate.DAL.NhPersistentLayer.Imp.Util;
using NHibernate.DAL.NhPersistentLayer.Exceptions;
using NHibernate.Criterion;
using ScrignoV2.Business.Entities;
using NHibernate.Context;

namespace NhPersistentLayer_tst
{
    class Program
    {
        static ISessionFactory sessionFactory = null;
        static NhConfigurationBuilder Builder = null;
        static IPagedDAO OwnPagedDAO = null;
        static ISessionProvider sessionProvider = null;

        static void Main(string[] args)
        {
            try
            {
                MakeEnvironment();

                CurrentSessionContext.Bind(sessionFactory.OpenSession());

                Test_PagedResult_QueryOver();
                //Test_PagedResult_DC();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                ISession session = CurrentSessionContext.Unbind(sessionFactory);
                if (session != null)
                {
                    session.Close();
                }
            }
            Console.ReadLine();
        }

        static void Test_PagedResult_QueryOver()
        {
            QueryOver<Consultant> query = QueryOver.Of<Consultant>().Where(n => n.ID > 10);
            IPagedResult<Consultant> paged = OwnPagedDAO.GetPagedResult<Consultant>(5, 5, query);
            

            Console.WriteLine(string.Format("Instances query count: {0}", paged.Counter.ToString()));
            Console.WriteLine(string.Format("Instances result count: {0}", paged.GetResult().Count().ToString()));
            
        }

        

        static void MakeEnvironment()
        {
            string dir = @"C:\Users\Diego\Documents\visual studio 2010\Projects\NhPersistentLayer\NhPersistentLayer_tst_3.5\MappingsXml";
            string cfg = @"C:\Users\Diego\Documents\visual studio 2010\Projects\NhPersistentLayer\NhPersistentLayer_tst_3.5\Cfg\Configuration.xml";

            Builder = new NhConfigurationBuilder(cfg, dir);
            Builder.SetProperty("connection.connection_string", @"Integrated Security=SSPI;Initial Catalog=Scrigno;Data Source=MYHOME\SQLEXPRESS");
            Builder.BuildSessionFactory();
            sessionFactory = Builder.SessionFactory;

            sessionProvider = new SessionManager(sessionFactory);

            OwnPagedDAO = new EnterprisePagedDAO(sessionProvider);

            Console.WriteLine("config generated");
            Console.WriteLine();
        }
    }
}
