﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using NHibernate;
using NHibernate.Context;
using NUnit.Framework;
using PersistentLayer.NHibernate;
using PersistentLayer.NHibernate.Impl;

namespace PersistentLayer.Test
{
    [TestFixture]
    public class CurrentTester
    {
        static ISessionFactory sessionFactory;
        NhConfigurationBuilder builder;
        INhPagedDAO currentPagedDAO;
        ISessionProvider sessionProvider;
        string rootPathProject;
        ISession currentSession;

        /// <summary>
        /// 
        /// </summary>
        public INhPagedDAO CurrentPagedDAO
        {
            get { return currentPagedDAO; }
        }

        /// <summary>
        /// 
        /// </summary>
        public ISessionProvider SessionProvider
        {
            get { return this.sessionProvider; }
        }

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
            currentPagedDAO = new EnterprisePagedDAO(sessionProvider);
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

        /// <summary>
        /// Discard the current internal session, opening new one.
        /// </summary>
        public void DiscardCurrentSession()
        {
            lock (this)
            {
                this.UnBindSession();
                if (this.currentSession != null)
                {
                    this.currentSession.Close();
                    this.currentSession = sessionFactory.OpenSession();
                }
                this.BindSession();
            }
        }
    }
}