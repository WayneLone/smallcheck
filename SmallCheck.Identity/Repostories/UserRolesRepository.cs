using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using SmallCheck.Identity.Entities;

namespace SmallCheck.Identity.Repostories
{
    /// <summary>
    /// 用户角色Repository
    /// </summary>
    public class UserRolesRepository : BaseRepository
    {

        #region 获得用户所有的角色 +List<string> FindByUserId(string userId)
        /// <summary>
        /// 获得用户所有的角色
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns>角色名称集合</returns>
        public List<string> FindByUserId(string userId)
        {
            List<string> roles = new List<string>();
            using (IDbConnection dbConn = DbWorker.Worker.OpenConnection(connectionName))
            {
                string sql = "select rs.name from user_roles ur, roles rs where rs.id = ur.roleid and ur.userid = @userId";
                roles = dbConn.Query<string>(sql, new { userId = userId }).ToList();
            }
            return roles;
        }
        #endregion

        #region 给用户添加角色 +int Insert(IdentityUser user, string roleId)
        /// <summary>
        /// 给用户添加角色
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="roleId">角色id</param>
        /// <returns>受影响的行数</returns>
        public int Insert(IdentityUser user, string roleId)
        {
            using (IDbConnection dbConn = DbWorker.Worker.OpenConnection(connectionName))
            {
                string sql = "insert into user_roles(userid, roleid) values(@userId, @roleId)";
                return dbConn.Execute(sql, new { userId = user.Id, roleId = roleId });
            }
        }
        #endregion

        #region 删除用户拥有的角色 +int Delete(string userId)
        /// <summary>
        /// 删除用户拥有的角色
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns>受影响的行数</returns>
        public int Delete(string userId)
        {
            using (IDbConnection dbConn = DbWorker.Worker.OpenConnection(connectionName))
            {
                string sql = "delete from user_roles where userid = @userId";
                return dbConn.Execute(sql, new { userId = userId });
            }
        }
        #endregion

        #region 删除用户角色信息 +int Delete(IdentityUser user, string roleId)
        /// <summary>
        /// 删除用户角色信息
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="roleId">角色id</param>
        /// <returns></returns>
        public int Delete(IdentityUser user, string roleId)
        {
            using (IDbConnection dbConn = DbWorker.Worker.OpenConnection(connectionName))
            {
                string sql = "delete from user_roles where userid = @userId and roleid = @roleId";
                return dbConn.Execute(sql, new { userId = user.Id, roleId = roleId });
            }
        } 
        #endregion
    }
}
