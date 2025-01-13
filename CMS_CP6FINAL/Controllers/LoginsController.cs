using CMS_CP6FINAL.Model;
using CMS_CP6FINAL.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CMS_CP6FINAL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ILoginService _loginService;
        private readonly CmsCamp6finalContext _context;

        public LoginsController(IConfiguration config, ILoginService loginService, CmsCamp6finalContext context)
        {
            _config = config;
            _loginService = loginService;
            _context = context;
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

        [HttpGet("{username}/{password}")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _loginService.AuthenticateUser(username, password);
            if (user != null)
            {
                var tokenString = string.Empty;
                object responsePayload = null;

                if (user.RoleId == 3)
                {
                    var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.StaffId == user.StaffId);
                    tokenString = GenerateJWTToken(user, doctor?.DoctorId);
                    responsePayload = new
                    {
                        uName = user.Username,
                        roleId = user.RoleId,
                        docId = doctor?.DoctorId,
                        token = tokenString
                    };
                }
                else
                {
                    tokenString = GenerateJWTToken(user);
                    responsePayload = new
                    {
                        uName = user.Username,
                        roleId = user.RoleId,
                        token = tokenString
                    };
                }

                return Ok(responsePayload);
            }
            return Unauthorized(new { message = "Invalid credentials." });
        }

        private string GenerateJWTToken(UserRegistration user, int? doctorId = null)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim("roleId", user.RoleId.ToString())
            };

            if (doctorId.HasValue)
            {
                claims.Add(new Claim("docId", doctorId.Value.ToString()));
            }

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"], claims, expires: DateTime.Now.AddMinutes(20),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

