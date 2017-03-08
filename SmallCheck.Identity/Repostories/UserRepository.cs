using SmallCheck.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace SmallCheck.Identity.Repostories
{
    /// <summary>
    /// 用户Repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class UserRepository<T> : BaseRepository where T : IdentityUser, new()
    {
        #region 获得用户名 +string GetUserName(string userId)
        /// <summary>
        /// 获得用户名
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns>用户名</returns>
        public string GetUserName(string userId)
        {
            using (IDbConnection dbConn = DbWorker.Worker.OpenConnection(connectionName))
            {
                string commandText = "select name from users where id = @id";
                return dbConn.QueryFirstOrDefault<string>(commandText, new { id = userId });
            }
        }
        #endregion

        #region 获得用户id +string GetUserId(string userName)
        /// <summary>
        /// 获得用户id
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>用户id</returns>
        public string GetUserId(string userName)
        {
            using (IDbConnection dbConn = DbWorker.Worker.OpenConnection(connectionName))
            {
                string cmdText = "select id from users where username = @username";
                return dbConn.QueryFirstOrDefault<string>(cmdText, new { username = userName });
            }
        }
        #endregion

        #region 获得用户信息 +T GetUserById(string userId)
        /// <summary>
        /// 获得用户信息
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns>用户信息</returns>
        public T GetUserById(string userId)
        {
            using (IDbConnection dbConn = DbWorker.Worker.OpenConnection(connectionName))
            {
                string cmdText = "select * from users where id = @userid";
                return dbConn.QueryFirstOrDefault(cmdText, new { userid = userId });
            }
        }
        #endregion

        #region 获得用户集合 +List<T> GetUserByName(string userName)
        /// <summary>
        /// 获得用户集合
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>用户集合</returns>
        public List<T> GetUserByName(string userName)
        {
            using (IDbConnection dbConn = DbWorker.Worker.OpenConnection(connectionName))
            {
                string sql = "select * from users where username = @username";
                return dbConn.Query<T>(sql, new { username = userName }).ToList() ?? new List<T>();
            }
        }
        #endregion

        #region 获得用户集合 +List<T> GetUserByEmail(string email)
        /// <summary>
        /// 获得用户集合
        /// </summary>
        /// <param name="email">用户email</param>
        /// <returns>用户集合</returns>
        public List<T> GetUserByEmail(string email)
        {
            using (IDbConnection dbConn = DbWorker.Worker.OpenConnection(connectionName))
            {
                string cmdText = "select * from users where email = @email";
                return dbConn.Query<T>(cmdText, new { email = email }).ToList() ?? new List<T>();
            }
        }
        #endregion

        #region 获得用户密码 +string GetPasswordHash(string userId)
        /// <summary>
        /// 获得用户密码
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns>用户密码</returns>
        public string GetPasswordHash(string userId)
        {
            using (IDbConnection dbConn = DbWorker.Worker.OpenConnection(connectionName))
            {
                string cmdText = "select password_hash from users where id = @userid";
                return dbConn.QueryFirstOrDefault(cmdText, new { userid = userId });
            }
        }
        #endregion

        #region 设置用户密码 +int SetPasswordHash(string userId, string passwordHash)
        /// <summary>
        /// 设置用户密码
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <param name="passwordHash">用户密码 密文</param>
        /// <returns>受影响的行数</returns>
        public int SetPasswordHash(string userId, string passwordHash)
        {
            using (IDbConnection dbConn = DbWorker.Worker.OpenConnection(connectionName))
            {
                string cmdText = "update users set password_hash = @pwdHash where id = @userId";
                return dbConn.Execute(cmdText, new { pwdHash = passwordHash, userId = userId });
            }
        }
        #endregion

        #region 获得用户安全码 +string GetSecurityStamp(string userId)
        /// <summary>
        /// 获得用户安全码
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns>安全码</returns>
        public string GetSecurityStamp(string userId)
        {
            using (IDbConnection dbConn = DbWorker.Worker.OpenConnection(connectionName))
            {
                string cmdText = "select security_stamp from users where id = @userId";
                return dbConn.QueryFirstOrDefault<string>(cmdText, new { userId = userId });
            }
        }
        #endregion

        #region 添加用户 +int Insert(T user)
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns>受影响的行数</returns>
        public int Insert(T user)
        {
            using (IDbConnection dbConn = DbWorker.Worker.OpenConnection(connectionName))
            {
                string sql = @"insert into users(id, username, email, email_confirmed, password_hash, security_stamp, phone_number, phone_number_confirmed, two_factor_enabled, lockout_end_date_utc, lockout_enabled, access_failed_count)
                    values(@id, @username, @email, @email_confirmed, @password_hash, @security_stamp, @phone_number, @phone_number_confirmed, @two_factor_enabled, @lockout_end_date_utc, @lockout_enabled, @access_failed_count)";
                return dbConn.Execute(sql, user);
            }
        }
        #endregion

        #region 删除用户 +int Delete(string userId)
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns>受影响的行数</returns>
        public int Delete(string userId)
        {
            using (IDbConnection dbConn = DbWorker.Worker.OpenConnection(connectionName))
            {
                string sql = "delete from users where id = @userId";
                return dbConn.Execute(sql, new { userId = userId });
            }
        }
        #endregion

        #region 删除用户 +int Delete(T user)
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        public int Delete(T user)
        {
            return Delete(user.Id);
        } 
        #endregion

        #region 更新用户 +int Update(T user)
        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns>受影响的行数</returns>
        public int Update(T user)
        {
            using (IDbConnection dbConn = DbWorker.Worker.OpenConnection(connectionName))
            {
                string sql = @"update users set username = @username, email = @email, email_confirmed = @email_confirmed, password_hash = @password_hash, 
                    security_stamp = @security_stamp, phone_number = @phone_number, phone_number_confirmed = @phone_number_confirmed, two_factor_enabled = @two_factor_enabled, 
                    lockout_end_date_utc = @lockout_end_date_utc, lockout_enabled = @lockout_enabled, access_failed_count = @access_failed_count where id = @id";
                return dbConn.Execute(sql, user);
            }
        }
        #endregion
    }
}
