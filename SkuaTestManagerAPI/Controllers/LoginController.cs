using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using STM.Core.EntityLayer;
using STM.Core.Helper;
using STM.Core.Repositories;
using STMAPI.Helper;
using STMAPI.JWT;
using STMAPI.Responses;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace STMAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Login")]
    public class LoginController : Controller
    {
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly JsonSerializerSettings _serializerSettings;
        private ILoginRepository _loginRepository;
        private readonly IUserService _userService;

        public LoginController(IOptions<JwtIssuerOptions> jwtOptions, ILoginRepository loginRepository, IUserService userService)
        {
            _loginRepository = loginRepository;
            _jwtOptions = jwtOptions.Value;
            _userService = userService;
            ThrowIfInvalidOptions(_jwtOptions);

            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("CheckUser")]
        public async Task<IActionResult> CheckUser([FromBody]User receivedUser)
        {
            IQueryable<User> lstRetrievedUser = (await _loginRepository.GetUserFromDB(receivedUser.UserName.Trim()));
            ListModelResponse<UserPreference> response = new ListModelResponse<UserPreference>();
            try
            {
                if (lstRetrievedUser != null && lstRetrievedUser.Any())
                {
                    User retrievedUser = lstRetrievedUser.First();
					bool isValid = true;
				//	bool isValid = CheckPassword(retrievedUser.Password, retrievedUser.Salt, receivedUser.Password);
					if (isValid)
                    {
                        int roleId = retrievedUser.RoleId;
                        Claim[] claims = new[]
                                {
                            new Claim("RoleId",roleId.ToString()),
                            new Claim(JwtRegisteredClaimNames.GivenName,receivedUser.UserName)
                         };

                        // Create the JWT security token and encode it.
                        JwtSecurityToken jwt = new JwtSecurityToken(
                            issuer: _jwtOptions.Issuer,
                            audience: _jwtOptions.Audience,
                            claims: claims,
                            notBefore: DateTime.Now,
                            expires: DateTime.Now.AddHours(10),
                            signingCredentials: _jwtOptions.SigningCredentials);

                        string encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                        UserPreference userPrefernce = new UserPreference();
                        userPrefernce.projectName = "France";
                        userPrefernce.auth_Token = encodedJwt;
                        userPrefernce.userName = retrievedUser.UserName;

                        List<UserPreference> userPreferences = new List<UserPreference>
                    {
                        userPrefernce
                    };
                        response.Model = userPreferences;
                    }
                    else
                        response.Message = "Invalid";
                }
                else
                    response.Message = "Invalid";
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.DidError = true;
                response.Message = "Invalid";
            }
            return response.ToHttpResponse();
        }

        [HttpPost]
        [Route("ChangePassword")]

        public async Task<string> ChangePassword([FromBody]User receivedUser)
        {
            User user = _userService.GetUser();
            //receivedUser.UserName = user.UserName;
            ListModelResponse<UserPreference> response = new ListModelResponse<UserPreference>();
            int updateCount = 0;
            IQueryable<User> lstRetrievedUser = (await _loginRepository.GetUserFromDB(receivedUser.UserName.Trim()));
            if (lstRetrievedUser != null && lstRetrievedUser.Any())
            {
                User retrievedUser = lstRetrievedUser.First();
                //bool isValid = CheckPassword(retrievedUser.Password, retrievedUser.Salt, receivedUser.Password);
                //if (isValid)
                if (user.UserName.Trim() == "admin")
                {
                    string saltAndPwd = String.Concat(retrievedUser.Salt, receivedUser.NewPassword);
                    HashAlgorithm hashAlgo = SHA256.Create();
                    byte[] saltwithpassword = Encoding.ASCII.GetBytes(saltAndPwd);
                    byte[] digestBytes = hashAlgo.ComputeHash(saltwithpassword.ToArray());
                    receivedUser.NewPassword = Convert.ToBase64String(digestBytes);
                    //receivedUser.NewPassword = AESEngine.Encrypt(saltAndPwd);
                    updateCount = _loginRepository.UpdateUserPassword(receivedUser.UserName, receivedUser.NewPassword);
                }
            }
            return updateCount == 0 ? "failed" : "success";
        }

        private static void ThrowIfInvalidOptions(JwtIssuerOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtIssuerOptions.ValidFor));
            }

            if (options.SigningCredentials == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.SigningCredentials));
            }

            if (options.JtiGenerator == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.JtiGenerator));
            }
        }

        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        private static long ToUnixEpochDate(DateTime date)
          => (long)Math.Round((date.ToUniversalTime() -
                               new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                              .TotalSeconds);

        private static bool CheckPassword(string retrievedUserPassword, string salt, string receivedPwd)
        {
            string hashPwd = CreatePasswordHash(receivedPwd, salt);
            if (hashPwd == retrievedUserPassword)
                return true;
            else
                return false;
        }

        private static string CreatePasswordHash(string pwd, string salt)
        {
            string saltAndPwd = String.Concat(salt, pwd);
            //This has gone obsolete, need to find alternative
            //string hashedPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(saltAndPwd, "sha1");
            HashAlgorithm hashAlgo = SHA256.Create();
            byte[] saltwithpassword = Encoding.ASCII.GetBytes(saltAndPwd);
            byte[] digestBytes = hashAlgo.ComputeHash(saltwithpassword.ToArray());
            string str = Convert.ToBase64String(digestBytes);
            return str;
        }
    }
}