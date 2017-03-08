using SmallCheck.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace SmallCheck.Identity.Repostories
{
    /// <summary>
    /// 用户Claims Repository
    /// </summary>
    public class UserClaimsRepository : BaseRepository
    {
        #region 获得用户身份标识 +ClaimsIdentity FindByUserId(string userId)
        /// <summary>
        /// 获得用户身份标识
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>用户身份标识</returns>
        public ClaimsIdentity FindByUserId(string userId)
        {
            ClaimsIdentity claims = new ClaimsIdentity();
            using (IDbConnection dbConn = DbWorker.Worker.OpenConnection(connectionName))
            {
                string sql = "select * from user_claims where userid = @userId";
                IEnumerable<UserClaims> userClaims = dbConn.Query<UserClaims>(sql, new { userId = userId });
                foreach (var item in userClaims)
                {
                    claims.AddClaim(new Claim(item.Claim_Type, item.Claim_Value));
                }
            }
            return claims;
        }
        #endregion

        #region 删除用户Claims +int Delete(string userId)
        /// <summary>
        /// 删除用户Claims
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>受影响的行数</returns>
        public int Delete(string userId)
        {
            using (IDbConnection dbConn = DbWorker.Worker.OpenConnection(connectionName))
            {
                string sql = "delete from user_claims where userId = @userId";
                return dbConn.Execute(sql, new { userId = userId });
            }
        }
        #endregion

        #region 删除用户Claims +int Delete(IdentityUser user, Claim claim)
        /// <summary>
        /// 删除用户Claims
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="claim">Claim</param>
        /// <returns></returns>
        public int Delete(IdentityUser user, Claim claim)
        {
            using (IDbConnection dbConn = DbWorker.Worker.OpenConnection(connectionName))
            {
                string sql = "delete from user_claims where userid = @userId and claim_value = @claimValue and claim_type = @claimType";
                return dbConn.Execute(sql, new { userId = user.Id, cliamValue = claim.Value, claimType = claim.Type });
            }
        } 
        #endregion

        #region 为用户添加Claim +int Insert(Claim userClaim, string userId)
        /// <summary>
        /// 为用户添加Claim
        /// </summary>
        /// <param name="userClaim">用户Claim</param>
        /// <param name="userId">用户id</param>
        /// <returns>受影响的行数</returns>
        public int Insert(Claim userClaim, string userId)
        {
            using (IDbConnection dbConn = DbWorker.Worker.OpenConnection(connectionName))
            {
                string sql = "insert into user_claims(id, userid, claim_type, claim_value) values(@id, @userid, @claimType, @claimValue)";
                return dbConn.Execute(sql);
            }
        } 
        #endregion
    }
}
