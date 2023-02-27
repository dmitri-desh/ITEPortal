using ITEPortal.Domain;
using ITEPortal.Domain.Dto;
using ITEPortal.Domain.Services.Interfaces;
using MessengerService;
using Microsoft.AspNetCore.Mvc;
using WebApi.ViewModels.Auth;
using WebApi.ViewModels.Login;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IEmailManager _emailManager;
        private readonly IUserService _userService;
        private readonly IAuthCodeService _authCodeService;

        public AuthController(
            IEmailManager emailManager,
            IUserService userService,
            IAuthCodeService authCodeService,
            ILogger<AuthController> logger)
        {
            _logger = logger;
            _emailManager = emailManager;
            _userService = userService;
            _authCodeService = authCodeService;
        }

        // POST auth/email
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
                Body = authCode.CodeNumber,
                Subject = "Verification code",
                FromEmail = "mail@testmail.com",
                ToEmail = email.Email
            };

            _emailManager.SendMessage(networkSettings, message);
            _logger.LogInformation($"Message from {message.FromEmail} to {message.ToEmail}. Subject is \"{message.Subject}\". Body is \"{message.Body}\"");

            return GetResponse(StatusCodes.Status200OK, new ResponseDto { Data = authCodeDto });
        }

        // POST auth/token
        [Route("token")]
        [HttpPost]
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
            var token = Helper.GetToken(code.Email);
            _logger.LogInformation($"Code {code.Code} has been verified for {code.Email}.");

            return Ok(token);
        }

        private IActionResult GetResponse(int successStatus, ResponseDto responseData)
        {
            if (responseData.Success)
            {
                return StatusCode(successStatus, responseData);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, responseData);
            }
        }
    }
}
