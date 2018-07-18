using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using STM.Core.EntityLayer;
using STMAPI.Helper;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace SkuaTestManagerAPI.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;

        public ValuesController(IHttpContextAccessor httpContextAccessor, IUserService userService)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [Authorize]
        [HttpGet("{id}")]
        public int Get(int id)
        {
            User user = _userService.GetUser();
            return user.RoleId;
        }

        [HttpPost]
        [Route("MagicKey")]
        public string MagicKey([FromBody]string cs)
        {
            string encryptedValue = AESEngine.Encrypt(cs);
            return encryptedValue;
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpGet]
        [Route("Admin1")]
        public int Admin1()
        {
            User user = _userService.GetUser();
            return user.RoleId;
        }

        [Authorize(Policy = "UserOnly")]
        [HttpGet]
        [Route("User1")]
        public int User1()
        {
            User user = _userService.GetUser();
            return user.RoleId;
        }

        [Authorize(Policy = "AllUsers")]
        [HttpGet]
        [Route("Both")]
        public int Both()
        {
            User user = _userService.GetUser();
            return user.RoleId;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
