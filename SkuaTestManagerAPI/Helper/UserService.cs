using Microsoft.AspNetCore.Http;
using STM.Core.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace STMAPI.Helper
{
    public class UserService : IUserService
    {
        private IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public User GetUser()
        {
            User user = new User();
            try
            { 
                List<ClaimsIdentity> lstClaimsIdentities = _httpContextAccessor.HttpContext.User.Identities.ToList<ClaimsIdentity>();
                List<Claim> lstClaim = lstClaimsIdentities[0].Claims.ToList();
                user.RoleId = Convert.ToInt32(lstClaim[0].Value);
                user.UserName = lstClaim[1].Value;
            }
            catch (Exception ex)
            {
                return null;   
            }
            return user;
        }
    }
}
