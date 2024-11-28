using IPASSDemo.Example.Request.Authentication;
using IPASSDemo.Example.Response;
using IPASSDemo.Example.Response.Authentication;
using IPASSDemo.Services;

using IPASSData.Dtos.Authentication;
using IPASSData.Extensions;
using IPASSData.Dtos.Enums;
using IPASSData.Dtos.Shared;

using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace IPASSDemo.Controllers.Authentication
{
    [ApiController]
    [Route("api/Authentication")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(ILogger<AuthenticationController> logger, IAuthenticationService authenticationService)
        {
            _logger = logger;
            _authenticationService = authenticationService;
        }
        /// <summary>
        /// 使用者帳密登入
        /// </summary>
        /// <param name="loginRequestDto"></param>
        /// <returns></returns>
        [SwaggerResponse(StatusCodes.Status200OK, "登入成功", typeof(ResponseModel<LoginResponseDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "格式錯誤", typeof(ValidFailResponseExample))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "系統錯誤", typeof(ExErrorResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(LoginSuccessResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ValidFailResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ExErrorResponseExample))]
        [SwaggerRequestExample(typeof(LoginRequestDto), typeof(LoginRequestExample))]
        [SwaggerOperation(Tags = new[] { "Authentication" })]
        [HttpPost("UserLogin")]
        public async Task<IActionResult> UserLoginByAcSw([FromBody] LoginRequestDto loginRequestDto)
        {
            var loginResponseDto = await _authenticationService.LoginByAcSw(loginRequestDto);

            if (string.IsNullOrEmpty(loginResponseDto.Token))
            {
                return BadRequest(new ResponseModel<LoginResponseDto>
                {
                    Status = ResponseStatusEnum.fail.ToString("G"),
                    Message = ReturnMessageEnum.UserLoginError.GetDisplayName() ?? ""
                });
            }
            return Ok(new ResponseModel<LoginResponseDto>
            {
                Status = ResponseStatusEnum.success.ToString("G"),
                Message = string.Empty,
                ResponseData = loginResponseDto
            });
        }
    }
}