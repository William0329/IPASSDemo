using AutoMapper;

using IPASSData.Dtos.Authentication;
using IPASSData.Models;

using IPASSDemo.Helpers;

namespace IPASSDemo.Services;

/// <summary>
/// 驗證
/// </summary>
public interface IAuthenticationService
{
    /// <summary>
    /// 以AcSw登入
    /// </summary>
    /// <param name="loginRequestDto"></param>
    /// <returns></returns>
    Task<LoginResponseDto> LoginByAcSw(LoginRequestDto loginRequestDto);
}

public class AuthenticationService : IAuthenticationService
{
    private readonly IMapper _mapper;
    private readonly IPassDbContext _db;
    private readonly EncryptHelper _encryptHelper;
    private readonly JwtHelper _jwtHelper;
    public AuthenticationService(IMapper mapper, IPassDbContext db, EncryptHelper encryptHelper, JwtHelper jwtHelper)
    {
        _mapper = mapper;
        _db = db;
        _encryptHelper = encryptHelper;
        _jwtHelper = jwtHelper;
    }

    /// <summary>
    /// 以AcSw登入
    /// </summary>
    /// <returns></returns>
    public async Task<LoginResponseDto> LoginByAcSw(LoginRequestDto loginRequestDto)
    {
        LoginResponseDto result = new LoginResponseDto();
        loginRequestDto.Sw = _encryptHelper.HMACSHA256Password(loginRequestDto.Sw);
        var user = _db.Users.FirstOrDefault(x => !x.IsDelete &&
                                                  x.Is_Active &&
                                                  x.Ac == loginRequestDto.Ac &&
                                                  x.Sw == loginRequestDto.Sw);
        if (user != null)
        {
            result.Name = loginRequestDto.Ac;
            result.Token = _jwtHelper.GenerateToken(user.Ac);
            _mapper.Map(user, result);
        }
        return result;
    }
}
