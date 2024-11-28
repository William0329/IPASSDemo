using IPASSData.CustomAttribute.Validations;
using IPASSData.Dtos.Authentication;
using IPASSData.Dtos.Enums;
using IPASSData.Dtos.Shared;
using IPASSData.Extensions;

using IPASSDemo.Example.Request.Authentication;
using IPASSDemo.Example.Response;
using IPASSDemo.Services;

using Microsoft.AspNetCore.Mvc;

using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace IPASSDemo.Controllers.Authentication
{
    [ApiController]
    [Route("api/Authentication")]
    //[Authorize(Policy = "Demo")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger,
                              IConfiguration configuration,
                              IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }
        /// <summary>
        /// 查詢帳號 列表
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        [SwaggerResponse(StatusCodes.Status200OK, "查詢成功", typeof(ResponseWithPageModel<List<UserDto>>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "格式錯誤", typeof(ValidFailResponseExample))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "系統錯誤", typeof(ExErrorResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ValidFailResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ExErrorResponseExample))]
        [SwaggerOperation(Tags = new[] { "Authentication" })]
        [HttpGet("User")]
        public async Task<IActionResult> GetUserList([FromQuery] ListRequestDto requestDto)
        {
            var (_result, _total) = await _userService.GetUsers(requestDto);
            ResponseWithPageModel<List<UserDto>> _response =
            new()
            {
                Status = ResponseStatusEnum.success.ToString("G"),
                Message = string.Empty,
                ResponseData = new ResponseWithPageDataModel<List<UserDto>> { Total = _total, Rows = _result }
            };

            return Ok(_response);
        }
        /// <summary>
        /// 查詢帳號
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [SwaggerResponse(StatusCodes.Status200OK, "查詢成功", typeof(ResponseModel<UserDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "格式錯誤", typeof(ValidFailResponseExample))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "系統錯誤", typeof(ExErrorResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ValidFailResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ExErrorResponseExample))]
        [SwaggerOperation(Tags = new[] { "Authentication" })]
        [HttpGet("User/{Id}")]
        public async Task<IActionResult> GetUser([RequiredCheck] string Id)
        {
            var result = await _userService.GetUser(Id);
            return Ok(new ResponseModel<UserDto>
            {
                Status = ResponseStatusEnum.success.ToString("G"),
                Message = string.Empty,
                ResponseData = result
            });
        }
        /// <summary>
        /// 新增 帳號
        /// </summary>
        /// <param name="UserDto"></param>
        /// <returns></returns>
        [SwaggerResponse(StatusCodes.Status200OK, "新增成功", typeof(ResponseModel<string>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "格式錯誤", typeof(ValidFailResponseExample))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "系統錯誤", typeof(ExErrorResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(AddSuccessResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ValidFailResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ExErrorResponseExample))]
        [SwaggerRequestExample(typeof(AddUserDto), typeof(AddUserRequestExample))]
        [SwaggerOperation(Tags = new[] { "Authentication" })]
        [HttpPost("User")]
        public async Task<IActionResult> AddUser(AddUserDto UserDto)
        {
            var AccountCheck = await _userService.IsAccountExist(null, UserDto.Ac);
            if (AccountCheck)
            {
                return BadRequest(new ResponseModel<string>
                {
                    Status = ResponseStatusEnum.error.ToString("G"),
                    Message = ReturnMessageEnum.AccountExist.GetDisplayName() ?? "",
                    ResponseData = null
                });
            }
            var result = await _userService.AddUser(UserDto);
            if (Guid.TryParse(result, out Guid tempGuid))
            {
                return Ok(new ResponseModel<string>
                {
                    Status = ResponseStatusEnum.success.ToString("G"),
                    Message = string.Empty,
                    ResponseData = result ?? ""
                });
            }
            else
            {
                return BadRequest(new ResponseModel<bool>
                {
                    Status = ResponseStatusEnum.fail.ToString("G"),
                    Message = result ?? "",
                    ResponseData = false
                });
            }
        }
        /// <summary>
        /// 更新 帳號
        /// </summary>
        /// <param name="UserDto"></param>
        /// <returns></returns>
        [SwaggerResponse(StatusCodes.Status200OK, "更新成功", typeof(ResponseModel<bool>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "格式錯誤", typeof(ValidFailResponseExample))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "系統錯誤", typeof(ExErrorResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(UpdateSuccessResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ValidFailResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ExErrorResponseExample))]
        [SwaggerRequestExample(typeof(UpdateUserDto), typeof(UpdateUserRequestExample))]
        [SwaggerOperation(Tags = new[] { "Authentication" })]
        [HttpPut("User")]
        public async Task<IActionResult> UpdateUser(UpdateUserDto UserDto)
        {
            var AccountCheck = await _userService.IsAccountExist(UserDto.Id, UserDto.Ac);
            if (AccountCheck)
            {
                return BadRequest(new ResponseModel<string>
                {
                    Status = ResponseStatusEnum.error.ToString("G"),
                    Message = ReturnMessageEnum.AccountExist.GetDisplayName() ?? "",
                    ResponseData = null
                });
            }
            var result = await _userService.UpdateUser(UserDto);
            if (string.IsNullOrEmpty(result))
            {
                return Ok(new ResponseModel<bool>
                {
                    Status = ResponseStatusEnum.success.ToString("G"),
                    Message = string.Empty,
                    ResponseData = true
                });
            }
            else
            {
                return BadRequest(new ResponseModel<bool>
                {
                    Status = ResponseStatusEnum.fail.ToString("G"),
                    Message = result ?? "",
                    ResponseData = false
                });
            }
        }
        /// <summary>
        /// 刪除 帳號
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [SwaggerResponse(StatusCodes.Status200OK, "刪除成功", typeof(ResponseModel<bool>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "格式錯誤", typeof(ValidFailResponseExample))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "系統錯誤", typeof(ExErrorResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(UpdateSuccessResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ValidFailResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ExErrorResponseExample))]
        [SwaggerOperation(Tags = new[] { "Authentication" })]
        [HttpDelete("User/{Id}")]
        public async Task<IActionResult> DeleteUser([RequiredCheck] string Id)
        {
            var result = await _userService.DeleteUser(Id);
            if (string.IsNullOrEmpty(result))
            {
                return Ok(new ResponseModel<bool>
                {
                    Status = ResponseStatusEnum.success.ToString("G"),
                    Message = string.Empty,
                    ResponseData = true
                });
            }
            else
            {
                return BadRequest(new ResponseModel<bool>
                {
                    Status = ResponseStatusEnum.fail.ToString("G"),
                    Message = result ?? "",
                    ResponseData = false
                });
            }
        }
        /// <summary>
        /// 更新帳號密碼 
        /// </summary>
        /// <param name="updatePasswordDto"></param>
        /// <returns></returns>
        [SwaggerResponse(StatusCodes.Status200OK, "更新成功", typeof(ResponseModel<bool>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "格式錯誤", typeof(ValidFailResponseExample))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "系統錯誤", typeof(ExErrorResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(UpdateSuccessResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(ValidFailResponseExample))]
        [SwaggerResponseExample(StatusCodes.Status500InternalServerError, typeof(ExErrorResponseExample))]
        [SwaggerRequestExample(typeof(UpdatePasswordDto), typeof(UpdatePasswordRequestExample))]
        [SwaggerOperation(Tags = new[] { "Authentication" })]
        [HttpPost("UpdateSw")]
        public async Task<IActionResult> UpdateSw(UpdatePasswordDto updatePasswordDto)
        {
            var result = await _userService.UpdatePassword(updatePasswordDto);
            if (string.IsNullOrEmpty(result))
            {
                return Ok(new ResponseModel<bool>
                {
                    Status = ResponseStatusEnum.success.ToString("G"),
                    Message = string.Empty,
                    ResponseData = true
                });
            }
            else
            {
                return BadRequest(new ResponseModel<bool>
                {
                    Status = ResponseStatusEnum.success.ToString("G"),
                    Message = result,
                    ResponseData = false
                });
            }
        }
        [SwaggerOperation(Tags = new[] { "Demo" })]
        [HttpPost("IsHoliday")]
        public async Task<IActionResult> IsHoliday(HolidayRequestDto holidayRequestDto)
        {
            var result = await _userService.IsHoliday(holidayRequestDto);
            return Ok(result);          
        }
    }
}
