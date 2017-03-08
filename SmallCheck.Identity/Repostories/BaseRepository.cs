using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallCheck.Identity.Repostories
{
    /// <summary>
    /// Repository Base Class
    /// </summary>
    public class BaseRepository
    {
        #region 数据连接名称 -readonly string connectionName
        /// <summary>
        /// 数据连接名称
        /// </summary>
        protected readonly string connectionName;
        #endregion

        #region 默认构造函数 +BaseRepository()
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public BaseRepository()
        {
            connectionName = "identityConn";
        }
        #endregion

        #region 构造函数 +BaseRepository(string connectionName)
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionName">连接字符串名称</param>
        public BaseRepository(string connectionName)
        {
            this.connectionName = connectionName;
        }
        #endregion
    }
}
