using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NHibernate;
using NHibernate.Linq;
using NHibernate.DAL.NhPersistentLayer;
using NHibernate.DAL.NhPersistentLayer.Imp;
using NHibernate.DAL.NhPersistentLayer.Imp.Util;
using NHibernate.DAL.NhPersistentLayer.Exceptions;
using NHibernate.Criterion;
using ScrignoV2.Business.Entities;
using NHibernate.Context;

namespace WFA_NhPersitentLayer_test
{
    public partial class Form1 : Form
    {
        static ISessionFactory sessionFactory = null;
        NhConfigurationBuilder builder = null;
        IPagedDAO ownPagedDAO = null;
        ISessionProvider sessionProvider = null;

        public Form1()
        {
            InitializeComponent();

            string dir = @"C:\Users\Diego\Documents\visual studio 2010\Projects\NhPersistentLayer\WFA_NhPersitentLayer_test\MappingsXml";
            string cfg = @"C:\Users\Diego\Documents\visual studio 2010\Projects\NhPersistentLayer\WFA_NhPersitentLayer_test\Cfg\Configuration.xml";

            builder = new NhConfigurationBuilder(cfg, dir);
            builder.SetProperty("connection.connection_string", @"Integrated Security=SSPI;Initial Catalog=Scrigno;Data Source=MYHOME\SQLEXPRESS");
            builder.BuildSessionFactory();
            sessionFactory = builder.SessionFactory;

            sessionProvider = new SessionManager(sessionFactory);

            ownPagedDAO = new EnterprisePagedDAO(sessionProvider);
        }

        #region PAGING
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int pageSize = Convert.ToInt32(txtPageSize.Text);
                int pageIndex = Convert.ToInt32(txtIndexPage.Text) - 1;

                BindSession();

                QueryOver<Consultant> query = QueryOver.Of<Consultant>().Where(n => n.ID > 10);
                IPagedResult<Consultant> paged = ownPagedDAO.GetPagedResult<Consultant>(pageSize * pageIndex, pageSize, query);

                txtCounter.Text = paged.Counter.ToString();
                dgv_Paging.DataSource = paged.GetResult();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().Name);
            }
            finally
            {
                UnBindSession();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int pageSize = Convert.ToInt32(txtPageSize.Text);
                int pageIndex = Convert.ToInt32(txtIndexPage.Text) - 1;

                BindSession();

                DetachedCriteria query = DetachedCriteria.For<Consultant>();
                query.Add(Expression.Gt("ID", (long)5));

                IPagedResult<Consultant> paged = ownPagedDAO.GetPagedResult<Consultant>(pageSize * pageIndex, pageSize, query);

                txtCounter.Text = paged.Counter.ToString();
                dgv_Paging.DataSource = paged.GetResult();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().Name);
            }
            finally
            {
                UnBindSession();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int pageSize = Convert.ToInt32(txtPageSize.Text);
                int pageIndex = Convert.ToInt32(txtIndexPage.Text) - 1;

                BindSession();

                IQueryable<Consultant> query = ownPagedDAO.ToIQueryable<Consultant>().Where(n => n.ID > 5);

                IPagedResult<Consultant> paged = ownPagedDAO.GetPagedResult<Consultant>(pageSize * pageIndex, pageSize, query);

                txtCounter.Text = paged.Counter.ToString();
                dgv_Paging.DataSource = paged.GetResult();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().Name);
            }
            finally
            {
                UnBindSession();
            }
        }

        #endregion

        #region
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                BindSession();
                IQuery query = ownPagedDAO.GetNamedQuery(txtNamedQuery.Text);

                dgvTransformerResult.DataSource = query.SetParameter(txtNP1.Text, txtPar1.Text).List<Consultant>();
                MessageBox.Show("Nessuna transformazione richiesta.");

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().Name);
            }
            finally
            {
                UnBindSession();
            }
        }
        
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                BindSession();
                string strQuery = richTxtBox.Text;
                IQuery query = ownPagedDAO.MakeHQLQuery(strQuery);

                AddParameters(query);

                dgvTransformerResult.DataSource = query.List<Consultant>();
                MessageBox.Show("Nessuna transformazione richiesta.");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().Name);
            }
            finally
            {
                UnBindSession();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                BindSession();
                string strQuery = richTxtBox.Text;
                ISQLQuery query = ownPagedDAO.MakeSQLQuery(strQuery);

                AddParameters(query);

                query.AddEntity(typeof(Consultant));
                dgvTransformerResult.DataSource = query.List<Consultant>();
                MessageBox.Show("Nessuna transformazione richiesta.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().Name);
            }
            finally
            {
                UnBindSession();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        private void AddParameters(IQuery query)
        {
            if (!string.IsNullOrEmpty(txtNP1.Text) && !string.IsNullOrEmpty(txtPar1.Text))
            {
                query.SetParameter(txtNP1.Text , txtPar1.Text);
            }

            if (!string.IsNullOrEmpty(txtNP2.Text) && !string.IsNullOrEmpty(txtPar2.Text))
            {
                query.SetParameter(txtNP2.Text, txtPar2.Text);
            }

            if (!string.IsNullOrEmpty(txtNP3.Text) && !string.IsNullOrEmpty(txtPar3.Text))
            {
                query.SetParameter(txtNP3.Text, txtPar3.Text);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void BindSession()
        {
            CurrentSessionContext.Bind(sessionFactory.OpenSession());
        }

        /// <summary>
        /// 
        /// </summary>
        private void UnBindSession()
        {
            ISession session = CurrentSessionContext.Unbind(sessionFactory);
            if (session != null)
            {
                session.Close();
            }
        }

        #region
        private void btnLoader_Click(object sender, EventArgs e)
        {
            try
            {
                BindSession();
                Consultant cons = ownPagedDAO.FindBy<Consultant, long?>(Convert.ToInt64(txtLoader.Text), LockMode.Read);

                MessageBox.Show(cons.ToString(), "All right");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().Name);
            }
            finally
            {
                UnBindSession();
            }
        }


        #endregion
        
    }
}
