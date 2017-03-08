using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallCheck.Identity.Entities
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class IdentityUser : IUser
    {
        #region 默认构造函数 +IdentityUser()
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public IdentityUser()
        {
            Id = Guid.NewGuid().ToString();
        }
        #endregion

        #region 构造函数赋值用户名 +IdentityUser(string userName) : this()
        /// <summary>
        /// 构造函数赋值用户名
        /// </summary>
        /// <param name="userName">用户名</param>
        public IdentityUser(string userName) : this()
        {
            UserName = userName;
        } 
        #endregion

        /// <summary>
        /// 用户id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 电子邮件
        /// </summary>
        public virtual string Email { get; set; }

        /// <summary>
        /// 电子邮件是否确认
        /// </summary>
        public virtual bool Email_Confirmed { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public virtual string Password_Hash { get; set; }

        /// <summary>
        /// 用户安全码
        /// </summary>
        public virtual string Security_Stamp { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public virtual string Phone_Number { get; set; }

        /// <summary>
        /// 手机号码是否验证
        /// </summary>
        public virtual bool Phone_Number_Confirmed { get; set; }

        /// <summary>
        /// 是否启用双重验证
        /// </summary>
        public virtual bool Two_Factory_Enable { get; set; }

        /// <summary>
        /// 上次锁定时间
        /// </summary>
        public virtual DateTime? Lockout_End_Date_Utc { get; set; }

        /// <summary>
        /// 是否被锁定
        /// </summary>
        public virtual bool LockoutEnabled { get; set; }

        /// <summary>
        /// 登录失败次数
        /// </summary>
        public virtual int Access_Failed_Count { get; set; }
    }
}
