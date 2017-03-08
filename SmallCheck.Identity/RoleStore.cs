using System.Linq;
using SmallCheck.Identity.Entities;
using SmallCheck.Identity.Repostories;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System;

namespace SmallCheck.Identity
{
    /// <summary>
    /// Role Store
    /// </summary>
    public class RoleStore<TRole> : IQueryableRoleStore<TRole> where TRole : IdentityRole
    {
        #region 可延迟查询的角色集合 +IQueryable<TRole> Roles
        /// <summary>
        /// 可延迟查询的角色集合
        /// </summary>
        public IQueryable<TRole> Roles
        {
            get;
        } 
        #endregion

        #region 角色仓储 -RoleRepository<IdentityRole> roleRepository
        /// <summary>
        /// 角色仓储
        /// </summary>
        private RoleRepository<IdentityRole> roleRepository; 
        #endregion

        #region 默认构造函数 +RoleStore()
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public RoleStore()
        {
            roleRepository = new RoleRepository<IdentityRole>();
        }
        #endregion

        #region 创建角色 +Task CreateAsync(TRole role)
        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="role">角色信息</param>
        /// <returns></returns>
        public Task CreateAsync(TRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }
            return Task.FromResult(roleRepository.Insert(role));
        }
        #endregion

        #region 删除角色 +Task DeleteAsync(TRole role)
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="role">角色信息</param>
        /// <returns></returns>
        public Task DeleteAsync(TRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }
            return Task.FromResult(roleRepository.Delete(role.Id));
        }
        #endregion

        #region 更新角色信息 +Task UpdateAsync(TRole role)
        /// <summary>
        /// 更新角色信息
        /// </summary>
        /// <param name="role">角色信息</param>
        /// <returns></returns>
        public Task UpdateAsync(TRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("user");
            }
            return Task.FromResult(roleRepository.Update(role));
        } 
        #endregion

        #region 获取角色信息 +Task<TRole> FindByIdAsync(string roleId)
        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <returns></returns>
        public Task<TRole> FindByIdAsync(string roleId)
        {
            if (string.IsNullOrEmpty(roleId))
            {
                throw new ArgumentException("参数roleId不能为空或者null");
            }
            TRole role = roleRepository.GetRoleId(roleId) as TRole;
            return Task.FromResult(role);
        }
        #endregion

        #region 获取角色信息 +Task<TRole> FindByNameAsync(string roleName)
        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <param name="roleName">角色名称</param>
        /// <returns></returns>
        public Task<TRole> FindByNameAsync(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentNullException("参数roleName不能为空或者null");
            }
            TRole role = roleRepository.GetRoleName(roleName) as TRole;
            return Task.FromResult(role);
        }
        #endregion

        #region 释放资源 +void Dispose()
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        { } 
        #endregion
    }
}
