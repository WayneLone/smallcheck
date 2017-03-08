using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallCheck.Identity.Repostories
{
    /// <summary>
    /// 数据库操作
    /// </summary>
    public sealed class DbWorker
    {
        #region 实例 -static readonly DbWorker worker
        /// <summary>
        /// 实例
        /// </summary>
        private static readonly DbWorker worker = new DbWorker();
        #endregion

        #region 私有构造函数 -private DbWorker()
        /// <summary>
        /// 私有构造函数
        /// </summary>
        private DbWorker() { }
        #endregion

        #region 数据库操作对象实例 +static DbWorker Worker
        /// <summary>
        /// 数据库操作对象实例
        /// </summary>
        public static DbWorker Worker
        {
            get
            {
                return worker;
            }
        }
        #endregion

        #region 获得数据库连接 +IDbConnection OpenConnection(string connName)
        /// <summary>
        /// 获得数据库连接
        /// </summary>
        /// <param name="connName">数据连接字符串名称</param>
        /// <returns>数据库连接对象</returns>
        public IDbConnection OpenConnection(string connName)
        {
            // 获取web.config文件中connectionStrings配置节
            ConnectionStringSettingsCollection settings = ConfigurationManager.ConnectionStrings;
            foreach (ConnectionStringSettings setting in settings)
            {
                // 读取该配置节下名称 和 connectionString相同的配置节
                if (setting.Name == connName)
                {
                    // 调用DbProviderFactories根据providerName属性中的值创建特定的数据库工厂
                    DbProviderFactory dbProviderFactory = DbProviderFactories.GetFactory(setting.ProviderName);
                    DbConnection dbConnection = dbProviderFactory.CreateConnection();
                    dbConnection.ConnectionString = setting.ConnectionString;   // 赋值连接字符串
                    dbConnection.Open();
                    return dbConnection;
                }
            }
            return null;
        } 
        #endregion
    }
}
