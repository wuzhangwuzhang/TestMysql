using FluentNHibernate.Cfg;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Cfg.Db;

namespace TaidouDatabase
{
    class NHibernateHelper
    {
        private static ISessionFactory sessionFactory = null;//单例模式

        private static void InitializedFactory()
        {
            sessionFactory = Fluently.Configure().Database(MySQLConfiguration.Standard.ConnectionString(db => db.Server("localhost").Database("taidou").Username("root").Password("root")))
                .Mappings(x => x.FluentMappings.AddFromAssemblyOf<NHibernateHelper>()).BuildSessionFactory();
        }
        private static ISessionFactory SessionFactory{ 
            get{
                if(sessionFactory == null)
                    InitializedFactory();
                 return sessionFactory;
     
            }
        }
        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}
