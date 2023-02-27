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

        public AuthController(IEmailManager emailManager, ILogger<AuthController> logger)
        {
            _logger = logger;
            _emailManager = emailManager;
        }

        // POST auth/email
        [Route("email")]
        [HttpPost]
        public void Post([FromBody] Login email)
        {
            var networkSettings = new NetworkSettings
            {
                Host = "127.0.0.1",
                Port = 25
            };
            var message = new EmailMessage
            {
                Body = email.Email,
                Subject = "Test",
                FromEmail = "mail@testmail.com",
                ToEmail = "d.deshko@itransition.com" //email.Email
            };

            _emailManager.SendMessage(networkSettings, message);
            _logger.LogInformation($"Message from {message.FromEmail} to {message.ToEmail}. Subject is \"{message.Subject}\". Body is \"{message.Body}\"");
        }

        // POST auth/token
        [Route("token")]
        [HttpPost]
        public void SubmitCode([FromBody] AuthCode code)
        {
            _logger.LogInformation($"Code {code.Code} has been submitted.");
        }
    }
}
