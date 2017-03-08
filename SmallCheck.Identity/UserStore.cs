using Microsoft.AspNet.Identity;
using SmallCheck.Identity.Entities;
using SmallCheck.Identity.Repostories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SmallCheck.Identity
{
    /// <summary>
    /// user store
    /// </summary>
    /// <typeparam name="TUser">IdentityUser</typeparam>
    public class UserStore<TUser> : IUserLoginStore<TUser>,
        IUserClaimStore<TUser>,
        IUserRoleStore<TUser>,
        IUserPasswordStore<TUser>,
        IUserSecurityStampStore<TUser>,
        IQueryableUserStore<TUser>,
        IUserEmailStore<TUser>,
        IUserPhoneNumberStore<TUser>,
        IUserTwoFactorStore<TUser, string>,
        IUserLockoutStore<TUser, string>,
        IUserStore<TUser>
        where TUser : IdentityUser
    {
        #region 用户仓储 -UserRepository<IdentityUser> userRepository
        /// <summary>
        /// 用户仓储
        /// </summary>
        private UserRepository<IdentityUser> userRepository;
        #endregion

        #region 角色仓储 -RoleRepository<IdentityRole> roleRepository
        /// <summary>
        /// 角色仓储
        /// </summary>
        private RoleRepository<IdentityRole> roleRepository;
        #endregion

        #region 用户角色仓储 -UserRolesRepository userRoleRepository
        /// <summary>
        /// 用户角色仓储
        /// </summary>
        private UserRolesRepository userRoleRepository;
        #endregion

        #region 用户声明仓储 -UserClaimsRepository userClaimRepository
        /// <summary>
        /// 用户声明仓储
        /// </summary>
        private UserClaimsRepository userClaimRepository;
        #endregion

        #region 用户登录仓储 -UserLoginsRepository userLoginRepository
        /// <summary>
        /// 用户登录仓储
        /// </summary>
        private UserLoginsRepository userLoginRepository;
        #endregion

        #region 默认构造函数 +UserStore()
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public UserStore()
        {
            userRepository = new UserRepository<IdentityUser>();
            roleRepository = new RoleRepository<IdentityRole>();
            userRoleRepository = new UserRolesRepository();
            userClaimRepository = new UserClaimsRepository();
            userLoginRepository = new UserLoginsRepository();
        }
        #endregion

        #region 用户 +IQueryable<TUser> Users
        /// <summary>
        /// 用户
        /// </summary>
        public IQueryable<TUser> Users
        {
            get;
        }
        #endregion

        #region 创建用户 +Task CreateAsync(TUser user)
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        public Task CreateAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            userRepository.Insert(user);
            return Task.FromResult<object>(null);
        }
        #endregion

        #region 查找用户 +Task<TUser> FindByIdAsync(string userId)
        /// <summary>
        /// 查找用户
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns>用户信息</returns>
        public Task<TUser> FindByIdAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("参数userId不能为null或空");
            }
            TUser result = userRepository.GetUserById(userId) as TUser;
            if (result != null)
            {
                return Task.FromResult(result);
            }
            return Task.FromResult<TUser>(null);
        }
        #endregion

        #region 查找用户 +Task<TUser> FindByNameAsync(string userName)
        /// <summary>
        /// 查找用户
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>用户信息</returns>
        public Task<TUser> FindByNameAsync(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("参数userName不能为空或者为null");
            }
            List<TUser> result = userRepository.GetUserByName(userName) as List<TUser>;
            if (result != null && result.Count == 1)
            {
                return Task.FromResult(result[0]);
            }
            return Task.FromResult<TUser>(null);
        }
        #endregion

        #region 更新用户 +Task UpdateAsync(TUser user)
        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        public Task UpdateAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            userRepository.Insert(user);
            return Task.FromResult<object>(null);
        }
        #endregion

        #region +void Dispose()
        public void Dispose()
        { }
        #endregion

        #region 添加Claim +Task AddClaimAsync(TUser user, Claim claim)
        /// <summary>
        /// 添加Claim
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="claim">声明</param>
        /// <returns></returns>
        public Task AddClaimAsync(TUser user, Claim claim)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (claim == null)
            {
                throw new ArgumentNullException("claim");
            }
            userClaimRepository.Insert(claim, user.Id);
            return Task.FromResult<object>(null);
        }
        #endregion

        #region 获取用户Claims +Task<IList<Claim>> GetClaimsAsync(TUser user)
        /// <summary>
        /// 获取用户Claims
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        public Task<IList<Claim>> GetClaimsAsync(TUser user)
        {
            ClaimsIdentity identity = userClaimRepository.FindByUserId(user.Id);
            return Task.FromResult<IList<Claim>>(identity.Claims.ToList());
        }
        #endregion

        #region 移除用户Claim +Task RemoveClaimAsync(TUser user, Claim claim)
        /// <summary>
        /// 移除用户Claim
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="claim">声明</param>
        /// <returns></returns>
        public Task RemoveClaimAsync(TUser user, Claim claim)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (claim == null)
            {
                throw new ArgumentNullException("claim");
            }
            userClaimRepository.Delete(user, claim);
            return Task.FromResult<object>(null);
        }
        #endregion

        #region 添加登录信息 +Task AddLoginAsync(TUser user, UserLoginInfo login)
        /// <summary>
        /// 添加登录信息
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="login">登录信息</param>
        /// <returns></returns>
        public Task AddLoginAsync(TUser user, UserLoginInfo login)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (login == null)
            {
                throw new ArgumentNullException("login");
            }
            userLoginRepository.Insert(user, login);
            return Task.FromResult<object>(null);
        }
        #endregion

        #region 获取用户信息 +Task<TUser> FindAsync(UserLoginInfo login)
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="login">登录信息</param>
        /// <returns></returns>
        public Task<TUser> FindAsync(UserLoginInfo login)
        {
            if (login == null)
            {
                throw new ArgumentNullException("login");
            }
            var userId = userLoginRepository.FindUserIdByLogin(login);
            if (userId != null)
            {
                TUser user = userRepository.GetUserById(userId) as TUser;
                if (user != null)
                {
                    return Task.FromResult(user);
                }
            }
            return Task.FromResult<TUser>(null);
        }
        #endregion

        #region 获取用户登录信息 +Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user)
        /// <summary>
        /// 获取用户登录信息
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns>用户登录信息</returns>
        public Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user)
        {
            List<UserLoginInfo> userLogins = new List<UserLoginInfo>();
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            List<UserLoginInfo> logins = userLoginRepository.FindByUserId(user.Id);
            if (logins == null)
            {
                return Task.FromResult<IList<UserLoginInfo>>(logins);
            }
            return Task.FromResult<IList<UserLoginInfo>>(null);
        }
        #endregion

        #region 移除登录信息 +Task RemoveLoginAsync(TUser user, UserLoginInfo login)
        /// <summary>
        /// 移除登录信息
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="login">登录信息</param>
        /// <returns></returns>
        public Task RemoveLoginAsync(TUser user, UserLoginInfo login)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (login == null)
            {
                throw new ArgumentNullException("login");
            }
            userLoginRepository.Delete(user, login);
            return Task.FromResult<object>(null);
        }
        #endregion

        #region 为用户添加角色 +Task AddToRoleAsync(TUser user, string roleName)
        /// <summary>
        /// 为用户添加角色
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="roleName">角色名称</param>
        /// <returns></returns>
        public Task AddToRoleAsync(TUser user, string roleName)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentException("参数roleName不能为空或null");
            }
            string roleId = roleRepository.GetRoleId(roleName);
            if (!string.IsNullOrEmpty(roleId))
            {
                userRoleRepository.Insert(user, roleId);
            }
            return Task.FromResult<object>(null);
        }
        #endregion

        #region 获取用户角色信息 +Task<IList<string>> GetRolesAsync(TUser user)
        /// <summary>
        /// 获取用户角色信息
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns>角色信息</returns>
        public Task<IList<string>> GetRolesAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            List<string> roles = userRoleRepository.FindByUserId(user.Id);
            if (roles != null)
            {
                return Task.FromResult<IList<string>>(roles);
            }
            return Task.FromResult<IList<string>>(null);
        }
        #endregion

        #region 验证用户是否拥有角色 +Task<bool> IsInRoleAsync(TUser user, string roleName)
        /// <summary>
        /// 验证用户是否拥有角色
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="roleName">角色名称</param>
        /// <returns></returns>
        public Task<bool> IsInRoleAsync(TUser user, string roleName)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentException("参数roleName不能为空或者null");
            }
            List<string> roles = userRoleRepository.FindByUserId(user.Id);
            if (roles != null && roles.Contains(roleName))
            {
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
        #endregion

        #region 移除用户角色信息 +Task RemoveFromRoleAsync(TUser user, string roleName)
        /// <summary>
        /// 移除用户角色信息
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="roleName">角色名称</param>
        /// <returns></returns>
        public Task RemoveFromRoleAsync(TUser user, string roleName)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentException("参数roleName不能为空或者null");
            }
            var role = roleRepository.GetRoleByName(roleName);
            if (role != null && !string.IsNullOrEmpty(role.Id))
            {
                userRoleRepository.Delete(user, role.Id);
            }
            return Task.FromResult<object>(null);
        }
        #endregion

        #region 删除用户 +Task DeleteAsync(TUser user)
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        public Task DeleteAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            userRepository.Delete(user);
            return Task.FromResult<object>(null);
        }
        #endregion

        #region 获得用户密码 +Task<string> GetPasswordHashAsync(TUser user)
        /// <summary>
        /// 获得用户密码
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns>用户密码</returns>
        public Task<string> GetPasswordHashAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            string passwordHash = userRepository.GetPasswordHash(user.Id);
            return Task.FromResult(passwordHash);
        }
        #endregion

        #region 用户是否有密码 +Task<bool> HasPasswordAsync(TUser user)
        /// <summary>
        /// 用户是否有密码
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        public Task<bool> HasPasswordAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            bool hasPassword = !string.IsNullOrEmpty(userRepository.GetPasswordHash(user.Id));
            return Task.FromResult(hasPassword);
        }
        #endregion

        #region 设置用户密码 +Task SetPasswordHashAsync(TUser user, string passwordHash)
        /// <summary>
        /// 设置用户密码
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="passwordHash">密码</param>
        /// <returns></returns>
        public Task SetPasswordHashAsync(TUser user, string passwordHash)
        {
            user.Password_Hash = passwordHash;
            return Task.FromResult<object>(null);
        }
        #endregion

        #region 设置用户安全码 +Task SetSecurityStampAsync(TUser user, string stamp)
        /// <summary>
        /// 设置用户安全码
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="stamp">安全码</param>
        /// <returns></returns>
        public Task SetSecurityStampAsync(TUser user, string stamp)
        {
            user.Security_Stamp = stamp;
            return Task.FromResult(0);
        }
        #endregion

        #region 获取用户安全码 +Task<string> GetSecurityStampAsync(TUser user)
        /// <summary>
        /// 获取用户安全码
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns>用户安全码</returns>
        public Task<string> GetSecurityStampAsync(TUser user)
        {
            return Task.FromResult(user.Security_Stamp);
        }
        #endregion

        #region 设置用户邮件 +Task SetEmailAsync(TUser user, string email)
        /// <summary>
        /// 设置用户邮件
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="email">邮件</param>
        /// <returns></returns>
        public Task SetEmailAsync(TUser user, string email)
        {
            user.Email = email;
            int affectRow = userRepository.Update(user);
            return Task.FromResult(affectRow);
        }
        #endregion

        #region 获取用户邮件 +Task<string> GetEmailAsync(TUser user)
        /// <summary>
        /// 获取用户邮件
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns>电子邮件</returns>
        public Task<string> GetEmailAsync(TUser user)
        {
            return Task.FromResult(user.Email);
        }
        #endregion

        #region 用户邮件是否已经确认 +Task<bool> GetEmailConfirmedAsync(TUser user)
        /// <summary>
        /// 用户邮件是否已经确认
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns>是否确认</returns>
        public Task<bool> GetEmailConfirmedAsync(TUser user)
        {
            return Task.FromResult(user.Email_Confirmed);
        }
        #endregion

        #region 设置电子邮件是否确认 +Task SetEmailConfirmedAsync(TUser user, bool confirmed)
        /// <summary>
        /// 设置电子邮件是否确认
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="confirmed">是否确认</param>
        /// <returns></returns>
        public Task SetEmailConfirmedAsync(TUser user, bool confirmed)
        {
            user.Email_Confirmed = confirmed;
            return Task.FromResult(userRepository.Update(user));
        }
        #endregion

        #region 获取用户信息 +Task<TUser> FindByEmailAsync(string email)
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="email">电子邮件</param>
        /// <returns>用户信息</returns>
        public Task<TUser> FindByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException("参数email不能为空或者null");
            }
            TUser user = userRepository.GetUserByEmail(email) as TUser;
            if (user != null)
            {
                return Task.FromResult(user);
            }
            return Task.FromResult<TUser>(null);
        }
        #endregion

        #region 设置用户手机号 +Task SetPhoneNumberAsync(TUser user, string phoneNumber)
        /// <summary>
        /// 设置用户手机号
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="phoneNumber">手机号</param>
        /// <returns></returns>
        public Task SetPhoneNumberAsync(TUser user, string phoneNumber)
        {
            user.Phone_Number = phoneNumber;
            return Task.FromResult(userRepository.Update(user));
        }
        #endregion

        #region 获取用户手机号 +Task<string> GetPhoneNumberAsync(TUser user)
        /// <summary>
        /// 获取用户手机号
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        public Task<string> GetPhoneNumberAsync(TUser user)
        {
            return Task.FromResult(user.Phone_Number);
        }
        #endregion

        #region 手机号是否已确认 +ask<bool> GetPhoneNumberConfirmedAsync(TUser user)
        /// <summary>
        /// 手机号是否已确认
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        public Task<bool> GetPhoneNumberConfirmedAsync(TUser user)
        {
            return Task.FromResult(user.Phone_Number_Confirmed);
        }
        #endregion

        #region 设置手机号是否已验证 +Task SetPhoneNumberConfirmedAsync(TUser user, bool confirmed)
        /// <summary>
        /// 设置手机号是否已验证
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="confirmed">是否已验证</param>
        /// <returns></returns>
        public Task SetPhoneNumberConfirmedAsync(TUser user, bool confirmed)
        {
            user.Phone_Number_Confirmed = confirmed;
            return Task.FromResult(userRepository.Update(user));
        }
        #endregion

        #region 设置双重身份验证启用 +Task SetTwoFactorEnabledAsync(TUser user, bool enabled)
        /// <summary>
        /// 设置双重身份验证启用
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="enabled">是否启用</param>
        /// <returns></returns>
        public Task SetTwoFactorEnabledAsync(TUser user, bool enabled)
        {
            user.Two_Factory_Enable = enabled;
            return Task.FromResult(userRepository.Update(user));
        }
        #endregion

        #region 双重身份验证是否启用 +Task<bool> GetTwoFactorEnabledAsync(TUser user)
        /// <summary>
        /// 双重身份验证是否启用
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        public Task<bool> GetTwoFactorEnabledAsync(TUser user)
        {
            return Task.FromResult(user.Two_Factory_Enable);
        }
        #endregion

        #region 获取上次锁定时间 +Task<DateTimeOffset> GetLockoutEndDateAsync(TUser user)
        /// <summary>
        /// 获取上次锁定时间
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        public Task<DateTimeOffset> GetLockoutEndDateAsync(TUser user)
        {
            return Task.FromResult(user.Lockout_End_Date_Utc.HasValue ?
                new DateTimeOffset(DateTime.SpecifyKind(user.Lockout_End_Date_Utc.Value, DateTimeKind.Utc))
                : new DateTimeOffset());
        }
        #endregion

        #region 设置锁定时间 +Task SetLockoutEndDateAsync(TUser user, DateTimeOffset lockoutEnd)
        /// <summary>
        /// 设置锁定时间
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="lockoutEnd">锁定时间</param>
        /// <returns></returns>
        public Task SetLockoutEndDateAsync(TUser user, DateTimeOffset lockoutEnd)
        {
            user.Lockout_End_Date_Utc = lockoutEnd.UtcDateTime;
            return Task.FromResult(userRepository.Update(user));
        }
        #endregion

        #region 增加登录失败次数 +Task<int> IncrementAccessFailedCountAsync(TUser user)
        /// <summary>
        /// 增加登录失败次数
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        public Task<int> IncrementAccessFailedCountAsync(TUser user)
        {
            user.Access_Failed_Count++;
            userRepository.Update(user);
            return Task.FromResult(user.Access_Failed_Count);
        }
        #endregion

        #region 重置登录失败次数 +Task ResetAccessFailedCountAsync(TUser user)
        /// <summary>
        /// 重置登录失败次数
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        public Task ResetAccessFailedCountAsync(TUser user)
        {
            user.Access_Failed_Count = 0;
            userRepository.Update(user);
            return Task.FromResult(0);
        }
        #endregion

        #region 获取用户登录失败次数 +Task<int> GetAccessFailedCountAsync(TUser user)
        /// <summary>
        /// 获取用户登录失败次数
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns>登录失败次数</returns>
        public Task<int> GetAccessFailedCountAsync(TUser user)
        {
            return Task.FromResult(user.Access_Failed_Count);
        }
        #endregion

        #region 获取用户是否被锁定 +Task<bool> GetLockoutEnabledAsync(TUser user)
        /// <summary>
        /// 获取用户是否被锁定
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns></returns>
        public Task<bool> GetLockoutEnabledAsync(TUser user)
        {
            return Task.FromResult(user.LockoutEnabled);
        }
        #endregion

        #region 设置用户是否锁定 +Task SetLockoutEnabledAsync(TUser user, bool enabled)
        /// <summary>
        /// 设置用户是否锁定
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="enabled">是否锁定</param>
        /// <returns></returns>
        public Task SetLockoutEnabledAsync(TUser user, bool enabled)
        {
            user.LockoutEnabled = enabled;
            return Task.FromResult(userRepository.Update(user));
        } 
        #endregion
    }
}
