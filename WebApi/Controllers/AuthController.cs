using ITEPortal.Data.Models;
using ITEPortal.Domain.Models.Configuration;
using ITEPortal.Domain.Services.Interfaces;
using MessengerService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.ViewModels.Auth;
using WebApi.ViewModels.Login;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Authorize]
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthOldController> _logger;
        private readonly IEmailManager _emailManager;
        private readonly IUserService _userService;
        private readonly IAuthCodeService _authCodeService;
        private readonly JwtConfiguration _jwtConfig;

        public AuthController(
            IEmailManager emailManager,
            IUserService userService,
            IAuthCodeService authCodeService,
            ILogger<AuthOldController> logger,
            IOptions<JwtConfiguration> jwtConfig)
        {
            _logger = logger;
            _emailManager = emailManager;
            _userService = userService;
            _authCodeService = authCodeService;
            _jwtConfig = jwtConfig.Value;
        }

        // POST auth/email
        [AllowAnonymous]
        [Route("email")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] Login email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userService.GetByEmailAsync(email.Email);
            if (user == null)
            {
                return BadRequest(ModelState);
            }

            var authCode = await _authCodeService.AddAuthCodeAsync(user.Id);

            var networkSettings = new NetworkSettings
            {
                Host = "127.0.0.1",
                Port = 25
            };
            var message = new EmailMessage
            {
                Body = authCode.CodeNumber,
                Subject = "Verification code",
                FromEmail = "mail@testmail.com",
                ToEmail = email.Email
            };

            await _emailManager.SendMessage(networkSettings, message);
            _logger.LogInformation($"Message from {message.FromEmail} to {message.ToEmail}. Subject is \"{message.Subject}\". Body is \"{message.Body}\"");

            // TODO: code is sent to response temporarily and only for dev purposes!!
            return Ok(authCode.CodeNumber);
        }

        // POST auth/token
        [AllowAnonymous]
        [Route("token")]
        [HttpPost]
        public async Task<IActionResult> GetToken([FromBody] AuthCodeRequest code)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userService.GetByEmailAsync(code.Email);
            if (user == null)
            {
                return BadRequest(ModelState);
            }

            var authCode = await _authCodeService.GetLastByUserIdAsync(user.Id);
            if (authCode == null || !authCode.CodeNumber.Equals(code.Code, StringComparison.InvariantCultureIgnoreCase))
            {
                return BadRequest(ModelState);
            }
            var jwtToken = GetToken(user);

            return Ok(new { token = jwtToken });
        }

        [Route("refresh-token")]
        [HttpGet]
        public async Task<IActionResult> RefreshToken()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity == null)
            {
                return BadRequest();
            }

            var userEmail = identity.FindFirst("Email")?.Value;

            var user = await _userService.GetByEmailAsync(userEmail);
            if (user == null)
            {
                return BadRequest();
            }

            var jwtToken = GetToken(user);

            return Ok(new { token = jwtToken });
        }

        private string GetToken(User user)
        {
            var issuer = _jwtConfig.Issuer;
            var audience = _jwtConfig.Audience;
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id.ToString()),
                    new Claim("Email", user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);

            return jwtToken;
        }
    }
}