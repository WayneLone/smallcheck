using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallCheck.Identity.Entities
{
    /// <summary>
    /// 角色信息
    /// implements the ASP.NET Identity IRole interface
    /// </summary>
    public class IdentityRole : IRole
    {
        #region 默认构造函数 +IdentityRole()
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public IdentityRole()
        {
            Id = Guid.NewGuid().ToString();
        } 
        #endregion

        #region 构造函数 +IdentityRole(string name) : this()
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">角色名称</param>
        public IdentityRole(string name) : this()
        {
            Name = name;
        }
        #endregion

        #region 构造函数 +IdentityRole(string name, string id)
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">角色名称</param>
        /// <param name="id">角色id</param>
        public IdentityRole(string name, string id)
        {
            Name = name;
            Id = id;
        }
        #endregion

        /// <summary>
        /// 角色 ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }
    }
}
