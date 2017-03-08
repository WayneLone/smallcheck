using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallCheck.Identity.Entities
{
    /// <summary>
    /// 用户声明信息
    /// </summary>
    public class UserClaims
    {
        /// <summary>
        /// 主键id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 用户id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 声明类型
        /// </summary>
        public string Claim_Type { get; set; }

        /// <summary>
        /// 声明值
        /// </summary>
        public string Claim_Value { get; set; }
    }
}
