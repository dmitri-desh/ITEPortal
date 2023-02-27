using ITEPortal.Domain.Dto;
using ITEPortal.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            return GetResponse(StatusCodes.Status200OK, await _userService.GetAllAsync());
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById([FromRoute] long userId)
        {
            return GetResponse(StatusCodes.Status200OK, await _userService.GetByIdAsync(userId));
        }

        [Route("add")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] UserDto userDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return GetResponse(StatusCodes.Status200OK, await _userService.AddUserAsync(userDto));
        }

        [Route("delete")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromBody] UserDto userDto)
        {
            return GetResponse(StatusCodes.Status200OK, await _userService.DeleteUserAsync(userDto));
        }

        [Route("update")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] UserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return GetResponse(StatusCodes.Status201Created, await _userService.UpdateUserAsync(userDto));
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
