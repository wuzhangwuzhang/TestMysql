using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaidouDatabase.Model.Mapping
{
    /// <summary>
    /// 该类是与数据库的表建立映射
    /// </summary>
    class TestUserMap:ClassMap<testUser>
    {
        public TestUserMap()
        {
            Id(x => x.Id).Column("id");//设置Id为主键 x表示testUser表的对象
            Map(x => x.Username).Column("username");
            Map(x => x.Password).Column("password");
            Map(x => x.Age).Column("age");
        }
    }
}
