using System;
using Blog.Model.Models;

namespace Blog.Model.Models
{
    public class FrameSeed
    {

        /// <summary>
        /// 生成Model层
        /// </summary>
        /// <param name="DbContext">上下文</param>
        /// <param name="tableNames">数据库表名数组，默认空，生成所有表</param>
        /// <returns></returns>
        public static bool CreateModels(DbContext DbContext, string[] tableNames = null)
        {

            try
            {
                DbContext.Create_Model_ClassFileByDBTalbe($@"C:\my-file\Blog.Core.Model", "Blog.Core.Model.Models", tableNames, "");
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        /// <summary>
        /// 生成IRepository层
        /// </summary>
        /// <param name="DbContext">上下文</param>
        /// <param name="tableNames">数据库表名数组，默认空，生成所有表</param>
        /// <returns></returns>
        public static bool CreateIRepositorys(DbContext DbContext, string[] tableNames = null)
        {

            try
            {
                DbContext.Create_IRepository_ClassFileByDBTalbe($@"C:\my-file\Blog.Core.IRepository", "Blog.Core.IRepository", tableNames, "");
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }



        /// <summary>
        /// 生成 IService 层
        /// </summary>
        /// <param name="DbContext">上下文</param>
        /// <param name="tableNames">数据库表名数组，默认空，生成所有表</param>
        /// <returns></returns>
        public static bool CreateIServices(DbContext DbContext, string[] tableNames = null)
        {

            try
            {
                DbContext.Create_IServices_ClassFileByDBTalbe($@"C:\my-file\Blog.Core.IServices", "Blog.Core.IServices", tableNames, "");
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }



        /// <summary>
        /// 生成 Repository 层
        /// </summary>
        /// <param name="DbContext">上下文</param>
        /// <param name="tableNames">数据库表名数组，默认空，生成所有表</param>
        /// <returns></returns>
        public static bool CreateRepository(DbContext DbContext, string[] tableNames = null)
        {

            try
            {
                DbContext.Create_Repository_ClassFileByDBTalbe($@"C:\my-file\Blog.Core.Repository", "Blog.Core.Repository", tableNames, "");
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }



        /// <summary>
        /// 生成 Service 层
        /// </summary>
        /// <param name="DbContext">上下文</param>
        /// <param name="tableNames">数据库表名数组，默认空，生成所有表</param>
        /// <returns></returns>
        public static bool CreateServices(DbContext DbContext, string[] tableNames = null)
        {

            try
            {
                DbContext.Create_Services_ClassFileByDBTalbe($@"C:\my-file\Blog.Core.Services", "Blog.Core.Services", tableNames, "");
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
