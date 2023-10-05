using Microsoft.AspNetCore.Mvc;
using Authentication.Models.Dto;
using Authentication.Service.IService;
using HmsMessageBus;

namespace Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private IUserInterface _userInterface;
        private readonly ResponseDto _response;
        private readonly IMessageBus _messageBus;
        private readonly IConfiguration _configuration;
        public UserController(IUserInterface userInterface, IConfiguration configuration, IMessageBus message)
        {
            _userInterface = userInterface;
            _response = new ResponseDto();
            _configuration = configuration;
            _messageBus = message;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ResponseDto>> AddUser(RegisterDto registerDto)
        {
            var errorMessage = await _userInterface.RegisterUser(registerDto);
            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                //error
                _response.IsSuccess = false;
                _response.Message = errorMessage;

                return BadRequest(_response);
            }
            else
            {
                // var queueName = _configuration.GetSection("QueueandTopics:RegisterUser").Get<string>();
                var message = new UserMessage()
                {
                    Email = registerDto.Email,
                    Name = registerDto.Name,
                };
                await _messageBus.PublishMessage(message, "registeruser");
                // System.Console.WriteLine(queueName);
            }



            return Ok(_response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ResponseDto>> LoginUser(LoginDto loginDto)
        {
            var response = await _userInterface.Login(loginDto);
            if (response.User == null)
            {
                //error
                _response.IsSuccess = false;
                _response.Message = "Invalid Credential";

                return BadRequest(_response);
            }
            _response.Result = response;
            return Ok(_response);
        }

        [HttpPost("AssignRole")]
        public async Task<ActionResult<ResponseDto>> AssignRole(RegisterDto registerDto)
        {
            var response = await _userInterface.AssignUserRole(registerDto.Email, registerDto.Role);
            if (!response)
            {
                //error
                _response.IsSuccess = false;
                _response.Message = "Error Occured";

                return BadRequest(_response);
            }
            _response.Result = response;
            return Ok(_response);
        }
    }
}