using MessengerService;
using Microsoft.AspNetCore.Mvc;
using WebApi.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IEmailManager _emailManager;

        public LoginController(IEmailManager emailManager, ILogger<LoginController> logger)
        {
            _logger = logger;
            _emailManager = emailManager;
        }

        // GET: api/<LoginController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<LoginController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<LoginController>
        [HttpPost]
        public void Post([FromBody] UserEmail userEmail)
        {
            var networkSettings = new NetworkSettings
            {
                Host = "127.0.0.1",
                Port = 25
            };
            var message = new EmailMessage
            {
                Body = userEmail.EmailValue,
                Subject = "Test",
                FromEmail = "mail@testmail.com",
                ToEmail = "d.deshko@itransition.com" //userEmail.EmailValue
            };

            _emailManager.SendMessage(networkSettings, message);
            _logger.LogInformation($"Message from {message.FromEmail} to {message.ToEmail}. Subject is \"{message.Subject}\". Body is \"{message.Body}\"");
        }

        // Submit api/<LoginController>
        [Route("submit")]
        [HttpPost]
        public void SubmitCode([FromBody] AuthCode code)
        {
            _logger.LogInformation($"Code {code.Code} has been submitted.");
        }

        // PUT api/<LoginController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LoginController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
