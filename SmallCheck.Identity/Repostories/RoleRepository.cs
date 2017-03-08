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
    /// 角色 Repository
    /// </summary>
    public class RoleRepository<T> : BaseRepository where T : IdentityRole, new()
    {
        #region 添加角色 +int Insert(T role)
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="role">角色信息</param>
        /// <returns>受影响的行数</returns>
        public int Insert(T role)
        {
            using (IDbConnection dbConn = DbWorker.Worker.OpenConnection(connectionName))
            {
                string sql = "insert into roles(id, name) values(@id, @name)";
                return dbConn.Execute(sql, role);
            }
        }
        #endregion

        #region 更新角色 +int Update(T role)
        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="role">角色信息</param>
        /// <returns>受影响的行数</returns>
        public int Update(T role)
        {
            using (IDbConnection dbConn = DbWorker.Worker.OpenConnection(connectionName))
            {
                string sql = "update roles set name = @name where id = @id";
                return dbConn.Execute(sql, role);
            }
        }
        #endregion

        #region 删除角色信息 +int Delete(string roleId)
        /// <summary>
        /// 删除角色信息
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <returns>受影响的行数</returns>
        public int Delete(string roleId)
        {
            using (IDbConnection dbConn = DbWorker.Worker.OpenConnection(connectionName))
            {
                string sql = "delete from roles where id = @roleId";
                return dbConn.Execute(sql, new { roleId = roleId });
            }
        }
        #endregion

        #region 获得角色名称 +string GetRoleName(string roleId)
        /// <summary>
        /// 获得角色名称
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <returns>角色名称</returns>
        public string GetRoleName(string roleId)
        {
            using (IDbConnection dbConn = DbWorker.Worker.OpenConnection(connectionName))
            {
                string sql = "select name from roles where id = @roleId";
                return dbConn.QueryFirstOrDefault<string>(sql, new { roleId = roleId });
            }
        }
        #endregion

        #region 获得角色id +string GetRoleId(string roleName)
        /// <summary>
        /// 获得角色id
        /// </summary>
        /// <param name="roleName">角色名称</param>
        /// <returns>角色ID</returns>
        public string GetRoleId(string roleName)
        {
            using (IDbConnection dbConn = DbWorker.Worker.OpenConnection(connectionName))
            {
                string sql = "select id from roles where name = @roleName";
                return dbConn.QueryFirstOrDefault<string>(sql, new { roleName = roleName });
            }
        }
        #endregion

        #region 获得角色 +T GetRoleById(string roleId)
        /// <summary>
        /// 获得角色
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <returns>角色信息</returns>
        public T GetRoleById(string roleId)
        {
            string roleName = GetRoleName(roleId);
            T role = null;
            if (roleName != null)
            {
                role = new T();
                role.Name = roleName;
                role.Id = roleId;
            }
            return role;
        }
        #endregion

        #region 获得角色 +T GetRoleByName(string roleName)
        /// <summary>
        /// 获得角色
        /// </summary>
        /// <param name="roleName">角色名称</param>
        /// <returns>角色信息</returns>
        public T GetRoleByName(string roleName)
        {
            string roleId = GetRoleId(roleName);
            T role = null;
            if (roleId != null)
            {
                role = new T();
                role.Id = roleId;
                role.Name = roleName;
            }
            return role;
        } 
        #endregion
    }
}
