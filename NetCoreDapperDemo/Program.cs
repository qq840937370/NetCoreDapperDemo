using Dapper;
using Microsoft.Data.SqlClient;
using NetCoreDapperDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NetCoreDapperDemo
{
    /// <summary>
    /// Test
    /// </summary>
    public class Program
    {
        //private static readonly string sqlCon = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
        private static readonly string SqlCon = "server=.;database=DapperDB;uid=sa;pwd=123456;";
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            //test_insert();
            //test_mult_insert();
            //test_update();
            //test_mult_update();
            test_select_one();
            test_select_list();
            test_select_content_with_comment();
        }

        #region 查询
        /// <summary>
        /// 查询单条指定的数据
        /// </summary>
        static void test_select_one()
        {
            using (var conn = new SqlConnection(SqlCon))
            {
                string sql_insert = @"select * from [dbo].[content] where id=@id";
                var result = conn.QueryFirstOrDefault<Content>(sql_insert, new { id = 5 });
                Console.WriteLine($"test_select_one：查到的数据为："+ result.content);
                Console.ReadKey();
            }
        }

        /// <summary>
        /// 查询多条指定的数据
        /// </summary>
        static void test_select_list()
        {
            using (var conn = new SqlConnection(SqlCon))
            {
                string sql_insert = @"select * from [dbo].[content] where id in @ids";
                var result = conn.Query<Content>(sql_insert, new { ids = new int[] { 6, 7 } });
                Console.WriteLine($"test_select_one：查到的数据为："+result.ToList()[0].content);
                Console.ReadKey();
            }
        }

        /// <summary>
        /// 关联查询
        /// </summary>
        static void test_select_content_with_comment()
        {
            using (var conn = new SqlConnection(SqlCon))
            {
                string sql_insert = @"select * from content where id=@id;
select * from comment where content_id=@id;";
                using (var result = conn.QueryMultiple(sql_insert, new { id = 5 }))
                {
                    var content = result.ReadFirstOrDefault<ContentWithComment>();
                    content.comments = result.Read<Comment>();
                    Console.WriteLine($"test_select_content_with_comment:内容5的评论数量{content.comments.Count()}");
                    Console.ReadKey();
                }
            }
        }
        #endregion

        #region 插入
        /// <summary>
        /// 测试插入单条数据
        /// </summary>
        static void test_insert()
        {
            var content = new Content
            {
                title = "标题1",
                content = "内容1",

            };
            using (var conn = new SqlConnection(SqlCon))
            {
                string sql_insert = @"INSERT INTO [Content]
                (title, [content], status, add_time, modify_time)
                VALUES(@title,@content,@status,@add_time,@modify_time)";
                var result = conn.Execute(sql_insert, content);
                Console.WriteLine($"test_insert：插入了{result}条数据！");
                Console.ReadKey();
            }
        }
        /// <summary>
        /// 测试一次批量插入两条数据
        /// </summary>
        static void test_mult_insert()
        {
            List<Content> contents = new List<Content>() {
               new Content
            {
                title = "批量插入标题1",
                content = "批量插入内容1",
            },
               new Content
            {
                title = "批量插入标题2",
                content = "批量插入内容2",
            },
            };

            using (var conn = new SqlConnection(SqlCon))
            {
                string sql_insert = @"INSERT INTO [Content]
                (title, [content], status, add_time, modify_time)
VALUES   (@title,@content,@status,@add_time,@modify_time)";
                var result = conn.Execute(sql_insert, contents);
                Console.WriteLine($"test_mult_insert：插入了{result}条数据！");
                Console.ReadKey();
            }
        }
        #endregion

        #region 修改
        /// <summary>
        /// 测试修改单条数据
        /// </summary>
        static void test_update()
        {
            var content = new Content
            {
                id = 5,
                title = "标题5",
                content = "内容5",

            };
            using (var conn = new SqlConnection(SqlCon))
            {
                string sql_insert = @"UPDATE  [Content]
SET         title = @title, [content] = @content, modify_time = GETDATE()
WHERE   (id = @id)";
                var result = conn.Execute(sql_insert, content);
                Console.WriteLine($"test_update：修改了{result}条数据！");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// 测试一次批量修改多条数据
        /// </summary>
        static void test_mult_update()
        {
            List<Content> contents = new List<Content>() {
               new Content
            {
                id=6,
                title = "批量修改标题6",
                content = "批量修改内容6",

            },
               new Content
            {
                id =7,
                title = "批量修改标题7",
                content = "批量修改内容7",

            },
        };

            using (var conn = new SqlConnection(SqlCon))
            {
                string sql_insert = @"UPDATE  [Content]
SET         title = @title, [content] = @content, modify_time = GETDATE()
WHERE   (id = @id)";
                var result = conn.Execute(sql_insert, contents);
                Console.WriteLine($"test_mult_update：修改了{result}条数据！");
                Console.ReadKey();
            }
        }
        #endregion

        #region 删除
        /// <summary>
        /// 测试删除单条数据
        /// </summary>
        static void test_del()
        {
            var content = new Content
            {
                id = 2,

            };
            using (var conn = new SqlConnection(SqlCon))
            {
                string sql_insert = @"DELETE FROM [Content]
WHERE   (id = @id)";
                var result = conn.Execute(sql_insert, content);
                Console.WriteLine($"test_del：删除了{result}条数据！");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// 测试一次批量删除两条数据
        /// </summary>
        static void test_mult_del()
        {
            List<Content> contents = new List<Content>() {
               new Content
            {
                id=3,

            },
               new Content
            {
                id=4,

            },
        };

            using (var conn = new SqlConnection(SqlCon))
            {
                string sql_insert = @"DELETE FROM [Content]
WHERE   (id = @id)";
                var result = conn.Execute(sql_insert, contents);
                Console.WriteLine($"test_mult_del：删除了{result}条数据！");
                Console.ReadKey();
            }
        }
        #endregion
    }
}
