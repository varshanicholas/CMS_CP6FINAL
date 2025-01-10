using CMS_CP6FINAL.Model;
using CMS_CP6FINAL.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CMS_CP6FINAL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ILoginService _loginService;
        
        public LoginsController(IConfiguration config, ILoginService loginService)
        {
            _config = config;
            _loginService = loginService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(int staffId, string username, string password)
        {
            try
            {
                var user = await _loginService.RegisterUser(staffId, username, password);
                return Ok(new { message = "User successfully registered." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(int userId, string newUsername, string newPassword)
        {
            try
            {
                var success = await _loginService.ResetUserPassword(userId, newUsername, newPassword);
                if (success)
                    return Ok(new { message = "Password successfully reset." });

                return BadRequest(new { message = "User not found." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{username}/{password}")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _loginService.AuthenticateUser(username, password);
            if (user != null)
            {
                // Generate JWT Token
                var tokenString = GenerateJWTToken(user);

                return Ok(new
                {
                    uName = user.Username,
                    roleId = user.RoleId,
                    token = tokenString
                });
            }
            return Unauthorized(new { message = "Invalid credentials." });
        }

        [HttpGet("eligible-users")]
        public async Task<IActionResult> GetEligibleUsers()
        {
            var eligibleUsers = await _loginService.FetchEligibleUsers();
            return Ok(eligibleUsers);
        }

        [HttpPost("assign-credentials")]
        public async Task<IActionResult> AssignCredentials(int staffId, string username, string password)
        {
            try
            {
                var success = await _loginService.AssignUserCredentials(staffId, username, password);
                if (success)
                    return Ok(new { message = "Credentials successfully assigned." });

                return BadRequest(new { message = "Failed to assign credentials." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        #region Generate JWT Token
        private string GenerateJWTToken(UserRegistration user)
        {
            // Secret key
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            // Algorithm
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            // JWT
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"], null, expires: DateTime.Now.AddMinutes(20),
                signingCredentials: credentials);

            // Writing Token
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion
    }
}



























//using CMS_CP6FINAL.Model;
//using CMS_CP6FINAL.Model;
//using CMS_CP6FINAL.Repository;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Text;

//// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace CMS_CP6FINAL.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class LoginsController : ControllerBase
//    {
//        private readonly IConfiguration _config;
//        private readonly ILoginService _loginService;
        
//        public LoginsController(IConfiguration config, ILoginService loginService)
//        {
//            _config = config;
//            _loginService = loginService;
//        }

//        [HttpPost("register")]
//        public async Task<IActionResult> RegisterUser(int staffId, string username, string password)
//        {
//            try
//            {
//                var user = await _loginService.RegisterUser(staffId, username, password);
//                return Ok(new { message = "User successfully registered." });
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(new { message = ex.Message });
//            }
//        }

//        [HttpPost("reset-password")]
//        public async Task<IActionResult> ResetPassword(int userId, string newUsername, string newPassword)
//        {
//            try
//            {
//                var success = await _loginService.ResetUserPassword(userId, newUsername, newPassword);
//                if (success)
//                    return Ok(new { message = "Password successfully reset." });

//                return BadRequest(new { message = "User not found." });
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(new { message = ex.Message });
//            }
//        }

//        [HttpPost("login")]
//        [AllowAnonymous]
//        public async Task<IActionResult> Login(string username, string password)
//        {
//            var user = await _loginService.AuthenticateUser(username, password);
//            if (user != null)
//            {
//                // Generate JWT Token
//                var tokenString = GenerateJWTToken(user);

//                return Ok(new
//                {
//                    uName = user.Username,
//                    roleId = user.RoleId,
//                    token = tokenString
//                });
//            }
//            return Unauthorized(new { message = "Invalid credentials." });
//        }

//        [HttpGet("eligible-users")]
//        public async Task<IActionResult> GetEligibleUsers()
//        {
//            var eligibleUsers = await _loginService.FetchEligibleUsers();
//            return Ok(eligibleUsers);
//        }

//        [HttpPost("assign-credentials")]
//        public async Task<IActionResult> AssignCredentials(int staffId, string username, string password)
//        {
//            try
//            {
//                var success = await _loginService.AssignUserCredentials(staffId, username, password);
//                if (success)
//                    return Ok(new { message = "Credentials successfully assigned." });

//                return BadRequest(new { message = "Failed to assign credentials." });
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(new { message = ex.Message });
//            }
//        }

//        #region Generate JWT Token




//        private string GenerateJWTToken(UserRegistration user)
//        {
//            // Secret key
//            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

//            // Algorithm
//            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

//            // JWT
//            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
//                _config["Jwt:Issuer"], null, expires: DateTime.Now.AddMinutes(20),
//                signingCredentials: credentials);

//            // Writing Token
//            return new JwtSecurityTokenHandler().WriteToken(token);
//        }
//        #endregion
//    }
//}
