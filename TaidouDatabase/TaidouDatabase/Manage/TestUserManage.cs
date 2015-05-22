using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaidouDatabase.Model;

namespace TaidouDatabase.Manage
{

    class TestUserManage
    {
        
        #region

        /// <summary>
        /// 获取用户信息表的所以信息
        /// </summary>
        /// <returns></returns>
        public IList<testUser> GetAllUser()
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var userList = session.QueryOver<testUser>();
                    return userList.List();
                }
            }
        }

        /// <summary>
        /// 通过姓名获取用户信息
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public IList<testUser> GetuserByUserName(string username)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var userList = session.QueryOver<testUser>().Where(x=>x.Username == username);
                    return userList.List();
                }
            }
        }

        /// <summary>
        /// 检测用户是否存在数据库
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static bool HasUserOrNot(string username)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var userList = session.QueryOver<testUser>().Where(x => x.Username == username);
                    if (userList.RowCount() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        /// <summary>
        /// 注册用户信息到数据库
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool SaveUser(testUser user)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                   session.Save(user);
                   try
                   {
                       transaction.Commit();
                       return true;
                   }
                   catch(Exception ex)
                   {
                       return false;
                       throw ex; 
                   }
                }
            }
        }

        /// <summary>
        /// 用户密码匹配
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool matchUserNameAndPwd(string username, string password)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var userList = session.QueryOver<testUser>().Where(x => x.Username == username && x.Password == password);
                    if (userList.RowCount() > 0)
                    {
                        Console.WriteLine("该用户成功登陆!");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("用户名或密码错误，请重新输入!");
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool deleteUserById(int id)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    testUser tu = new testUser();
                    tu.Id = id;
                    session.Delete(tu);
                    try
                    {
                        transaction.Commit();
                        return true;
                    }
                    catch(Exception ex)
                    {
                        return false;
                        throw ex;
                    }
                }
            }
        }
        public static bool UpdateUserInfo(testUser tu)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Update(tu);
                    try
                    {
                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        return false;
                        throw ex;
                    }
                }
            }
        }
        #endregion
        #region
        public static void Main(string[] args)
        {
            Console.WriteLine("--------全表查询----------");
            TestUserManage testuserManage = new TestUserManage();
            IList<testUser> testUser = testuserManage.GetAllUser();
            foreach (var tu in testUser)
            {
                Console.WriteLine("UserName:"+tu.Username + "\nPasswd:"+tu.Password );
            }

            Console.WriteLine();
            Console.WriteLine("--------按姓名查询----------");
            IList<testUser> testUser2 = testuserManage.GetuserByUserName("siki");
            foreach (var tu in testUser2)
            {
                Console.WriteLine("UserName:" + tu.Username + "\nPasswd:" + tu.Password);
            }

            Console.WriteLine();
            Console.WriteLine("--------bool姓名查询----------");
            if(HasUserOrNot("wuzhang"))
            {
                Console.WriteLine("该用户存在数据库！");
            }

            Console.WriteLine();
            //Console.WriteLine("--------注册用户----------");
            //testUser userlog = new Model.testUser();
            //userlog.Username = "toki";
            //userlog.Password = "测试数据";
            //userlog.Age = 100;
            ////注册前应先检索用户是否存在
            //if (HasUserOrNot("toki"))
            //{
            //    Console.WriteLine("该用户名已注册,请重新输入！");
            //}
            //else
            //{
            //    if (SaveUser(userlog))
            //    {
            //        Console.WriteLine("注册成功！");
            //    }
            //}

            matchUserNameAndPwd("wuzhang","wuzhang");

            testUser tmp_user = new Model.testUser();
            tmp_user.Id = 6;
            if (deleteUserById(tmp_user.Id))
            {
                Console.WriteLine("删除数据成功!");
            }
            else
            {
                Console.WriteLine("删除数据失败!");
            }

            testUser tu1 = testUser[0];
            tu1.Password = "123456";
            if (UpdateUserInfo(tu1))
            {
                Console.WriteLine("更新数据成功!");
            }
            else
            {
                Console.WriteLine("更新数据失败!");
            }

            Console.ReadLine();
        }
        #endregion
    }
}
