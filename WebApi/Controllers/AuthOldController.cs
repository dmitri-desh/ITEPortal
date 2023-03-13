using ITEPortal.Domain;
using ITEPortal.Domain.Dto;
using ITEPortal.Domain.Models;
using ITEPortal.Domain.Services.Implementation;
using ITEPortal.Domain.Services.Interfaces;
using MessengerService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.ViewModels.Auth;
using WebApi.ViewModels.Login;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Authorize, Route("auth")]
    [ApiController]
    public class AuthOldController : ControllerBase
    {
        private readonly ILogger<AuthOldController> _logger;
        private readonly IEmailManager _emailManager;
        private readonly IUserService _userService;
        private readonly IAuthCodeService _authCodeService;
        private readonly ITokenClaimsService _tokenClaimsService;

        public AuthOldController(
            IEmailManager emailManager,
            IUserService userService,
            IAuthCodeService authCodeService,
            ITokenClaimsService tokenClaimsService,
            ILogger<AuthOldController> logger)
        {
            _logger = logger;
            _emailManager = emailManager;
            _userService = userService;
            _authCodeService = authCodeService;
            _tokenClaimsService = tokenClaimsService;
        }

        // POST auth/email
        [HttpPost, AllowAnonymous, Route("email")]
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
            var authCodeDto = new AuthCodeDto
            {
                UserId = user.Id
            };
            var authCode = await _authCodeService.AddAuthCodeAsync(authCodeDto);

            var networkSettings = new NetworkSettings
            {
                Host = "127.0.0.1",
                Port = 25
            };
            var message = new EmailMessage
            {
                Body = authCode?.CodeNumber,
                Subject = "Verification code",
                FromEmail = "mail@testmail.com",
                ToEmail = email.Email
            };

            _emailManager.SendMessage(networkSettings, message);
            _logger.LogInformation($"Message from {message.FromEmail} to {message.ToEmail}. Subject is \"{message.Subject}\". Body is \"{message.Body}\"");

            return Ok();
        }

        // POST auth/token
        [HttpPost, AllowAnonymous, Route("token")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetToken([FromBody] AuthCode code)
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
            
            var token = await _tokenClaimsService.GetTokenAsync(code.Email);

            var result = new AuthenticateResultModel
            {
                AccessToken = token.AccessToken,
                ExpiresUTC = token.ExpiresUTC.Value,
            };

            _logger.LogInformation($"Code {code.Code} has been verified. Token has been granted for {code.Email}.");

            return Ok(result);
        }

        // POST auth/refresh-token
        [HttpPost, Route("refresh-token")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshToken token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var claimName = await _tokenClaimsService.ValidateTokenAsync(token.Token);

            if (claimName == null)
            {
                return BadRequest(ModelState);
            }
            var newToken = await _tokenClaimsService.GetTokenAsync(claimName);

            var result = new AuthenticateResultModel
            {
                AccessToken = newToken.AccessToken,
                ExpiresUTC = newToken.ExpiresUTC.Value,
            };

            _logger.LogInformation($"Token has been refreshed.");

            return Ok(result);
        }
    }
}
