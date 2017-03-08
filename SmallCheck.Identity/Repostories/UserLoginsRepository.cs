using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using SmallCheck.Identity.Entities;
using Microsoft.AspNet.Identity;
using System.Data;

namespace SmallCheck.Identity.Repostories
{
    /// <summary>
    /// 用户登录
    /// </summary>
    public class UserLoginsRepository : BaseRepository
    {
        #region 删除用户登录信息 +int Delete(IdentityUser user, UserLoginInfo login)
        /// <summary>
        /// 删除用户登录信息
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="login">登录信息</param>
        /// <returns>受影响的行数</returns>
        public int Delete(IdentityUser user, UserLoginInfo login)
        {
            using (IDbConnection dbConn = DbWorker.Worker.OpenConnection(connectionName))
            {
                string sql = "delete from user_logins where userid = @userId and login_provider = @loginProvider and provider_key = @providerKey";
                return dbConn.Execute(sql, new { userId = user.Id, loginProvider = login.LoginProvider, providerKey = login.ProviderKey });
            }
        }
        #endregion

        #region 删除用户登录信息 +int Delete(string userId)
        /// <summary>
        /// 删除用户登录信息
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns>受影响的行数</returns>
        public int Delete(string userId)
        {
            using (IDbConnection dbConn = DbWorker.Worker.OpenConnection(connectionName))
            {
                string sql = "delete from user_logins where userid = @userId";
                return dbConn.Execute(sql, new { userId = userId });
            }
        }
        #endregion

        #region 添加用户登录信息 +int Insert(IdentityUser user, UserLoginInfo login)
        /// <summary>
        /// 添加用户登录信息
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="login">登录信息</param>
        /// <returns>受影响的行数</returns>
        public int Insert(IdentityUser user, UserLoginInfo login)
        {
            using (IDbConnection dbConn = DbWorker.Worker.OpenConnection(connectionName))
            {
                string sql = "insert into user_logins(login_provider, provider_key, userid) values(@loginProvider, @providerKey, @userId)";
                return dbConn.Execute(sql, new { loginProvider = login.LoginProvider, providerKey = login.ProviderKey, userId = user.Id });
            }
        }
        #endregion

        #region 查询用户id +string FindUserIdByLogin(UserLoginInfo userLogin)
        /// <summary>
        /// 查询用户id
        /// </summary>
        /// <param name="userLogin">用户登录信息</param>
        /// <returns></returns>
        public string FindUserIdByLogin(UserLoginInfo userLogin)
        {
            using (IDbConnection dbConn = DbWorker.Worker.OpenConnection(connectionName))
            {
                string sql = "select userid from user_logins where login_provider = @loginProvider and provider_key = @providerKey";
                return dbConn.QueryFirstOrDefault<string>(sql, userLogin);
            }
        }
        #endregion

        #region 获得用户登录信息 +List<UserLoginInfo> FindByUserId(string userId)
        /// <summary>
        /// 获得用户登录信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>用户登录信息</returns>
        public List<UserLoginInfo> FindByUserId(string userId)
        {
            List<UserLoginInfo> logins = new List<UserLoginInfo>();
            using (IDbConnection dbConn = DbWorker.Worker.OpenConnection(connectionName))
            {
                string sql = "select login_provider loginProvider, provider_key providerKey from user_logins where userId = @userId";
                logins = dbConn.Query<UserLoginInfo>(sql, new { userId = userId }).ToList();
            }
            return logins;
        } 
        #endregion
    }
}
