using AutoMapper;

using IPASSData.Dtos.Authentication;
using IPASSData.Dtos.Enums;
using IPASSData.Dtos.Shared;
using IPASSData.Extensions;
using IPASSData.Models;

using IPASSDemo.Helpers;

using System.Net.Http.Json;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace IPASSDemo.Services;

/// <summary>
/// 帳號
/// </summary>
public interface IUserService
{
    /// <summary>
    /// 取得帳號清單
    /// </summary>
    /// <returns></returns>
    Task<(List<UserDto> userDto, int total)> GetUsers(ListRequestDto requestDto);

    /// <summary>
    /// 取得指定的帳號
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    Task<UserDto> GetUser(string Id);
    /// <summary>
    /// 取得驗證器Id對應的帳號
    /// </summary>
    /// <param name="AuthenticatorUserId"></param>
    /// <returns></returns>
    Task<UserDto> GetUserByAuthenticatorUserId(string AuthenticatorUserId);
    /// <summary>
    /// 取得原始帳號 資料
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    Task<User> GetSourceUser(string Id);
    /// <summary>
    /// 新增帳號
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    Task<string?> AddUser(AddUserDto data);

    /// <summary>
    /// 更新帳號
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>/// 
    Task<string?> UpdateUser(UpdateUserDto data);

    /// <summary>
    /// 刪除帳號
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    Task<string?> DeleteUser(string Id);

    /// <summary>
    /// 帳號是否存在
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Ac"></param>
    /// <param name="GroupId"></param>
    /// <returns></returns>
    Task<bool> IsAccountExist(string? Id, string Ac);
    /// <summary>
    /// 更新密碼
    /// </summary>
    /// <param name="updatePasswordDto"></param>
    /// <returns></returns>
    Task<string?> UpdatePassword(UpdatePasswordDto updatePasswordDto);
    /// <summary>
    /// 是否為假日
    /// </summary>
    /// <param name="holidayRequestDto"></param>
    /// <returns></returns>
    Task<HolidayResponseDto> IsHoliday(HolidayRequestDto holidayRequestDto);
}

public class UserService : IUserService
{
    private readonly IPassDbContext _db;
    private readonly IMapper _mapper;
    private readonly EncryptHelper _encryptHelper;
    public UserService(IPassDbContext db, IMapper mapper, EncryptHelper encryptHelper)
    {
        _db = db;
        _mapper = mapper;
        _encryptHelper = encryptHelper;
    }

    /// <summary>
    /// 取得帳號清單
    /// </summary>
    /// <returns></returns>
    public async Task<(List<UserDto> userDto, int total)> GetUsers(ListRequestDto requestDto)
    {
        var Users = _db.Users.Where(x => !x.IsDelete);
        if (!string.IsNullOrEmpty(requestDto.KeyWord))
        {
            Users = Users.Where(x => (!string.IsNullOrEmpty(x.Ac) && x.Ac.Contains(requestDto.KeyWord)) ||
                                     (!string.IsNullOrEmpty(x.Username) && x.Username.Contains(requestDto.KeyWord)));
        }
        var total = Users.Count();
        var allUsers = Users.OrderByDescending(x => x.CreateAt)
                            .Skip((requestDto.Page - 1) * requestDto.PageSize)
                            .Take(requestDto.PageSize)
                            .ToList();
        var result = _mapper.Map<List<UserDto>>(allUsers);
        return (result, total);
    }

    /// <summary>
    /// 取得指定的帳號
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    public async Task<UserDto> GetUser(string Id)
    {
        UserDto result = null;
        var Users = _db.Users.Where(x => !x.IsDelete && x.Id == Id)
                             .FirstOrDefault();
        if (Users != null)
        {
            result = _mapper.Map<UserDto>(Users);
        }
        return result;
    }
    /// <summary>
    /// 取得驗證器Id對應的帳號
    /// </summary>
    /// <param name="AuthenticatorUserId"></param>
    /// <returns></returns>
    public async Task<UserDto> GetUserByAuthenticatorUserId(string AuthenticatorUserId)
    {
        UserDto result = null;
        var Users = _db.Users.Where(x => !x.IsDelete)
                             .FirstOrDefault(x => x.AuthenticatorUserId == AuthenticatorUserId);
        if (Users != null)
        {
            result = _mapper.Map<UserDto>(Users);
        }
        return result;
    }
    /// <summary>
    /// 取得原始帳號 資料
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    public async Task<User> GetSourceUser(string Id)
    {
        var result = _db.Users.Where(x => !x.IsDelete)
                              .FirstOrDefault(x => x.Id.ToString() == Id);
        return result;
    }
    /// <summary>
    /// 新增帳號
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public async Task<string?> AddUser(AddUserDto data)
    {
        _db.Database.BeginTransaction();
        try
        {
            data.Sw = _encryptHelper.HMACSHA256Password(data.Sw);
            var users = _mapper.Map<User>(data);
            _db.Users.Add(users);
            _db.SaveChanges();
            //產生驗證器User Id
            users.AuthenticatorUserId = Convert.ToBase64String(Encoding.UTF8.GetBytes(users.Id.ToString()));
            _db.SaveChanges();
            _db.Database.CommitTransaction();
            return users.Id.ToString();
        }
        catch (Exception ex)
        {
            _db.Database.RollbackTransaction();
            return $"Error : {ex.Message} StackTrace:{ex.StackTrace}"; ;
        }
    }

    /// <summary>
    /// 更新帳號
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public async Task<string?> UpdateUser(UpdateUserDto data)
    {
        var result = string.Empty;
        _db.Database.BeginTransaction();
        try
        {
            var users = _db.Users.FirstOrDefault(x => x.Id.ToString() == data.Id);
            if (users != null)
            {
                _mapper.Map(data, users);
                if (!string.IsNullOrEmpty(data.Sw))
                {
                    users.Sw = _encryptHelper.HMACSHA256Password(data.Sw);
                }
            }
            var flag = _db.SaveChanges() > 0;
            _db.Database.CommitTransaction();
            if (!flag)
            {
                result = $"Users Id : {data.Id} 更新異常";
            }
        }
        catch (Exception ex)
        {
            result = $"Error : {ex.Message} StackTrace:{ex.StackTrace}";
            _db.Database.RollbackTransaction();
        }
        return result;
    }

    /// <summary>
    /// 刪除帳號
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    public async Task<string?> DeleteUser(string Id)
    {
        var result = string.Empty;
        _db.Database.BeginTransaction();
        try
        {
            var users = _db.Users.FirstOrDefault(x => x.Id.ToString() == Id);
            if (users != null)
            {
                users.IsDelete = true;
                users.UpdateAt = DateTime.Now.ToUniversalTime();
            }
            var flag = _db.SaveChanges() > 0;
            _db.Database.CommitTransaction();
            if (!flag)
            {
                result = $"Users Id : {Id} 刪除異常";
            }
        }
        catch (Exception ex)
        {
            result = $"Error : {ex.Message} StackTrace:{ex.StackTrace}"; ;
            _db.Database.RollbackTransaction();
        }
        return result;
    }
    /// <summary>
    /// 帳號是否已存在
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Ac"></param>
    /// <param name="GroupId"></param>
    /// <returns></returns>
    public async Task<bool> IsAccountExist(string? Id, string Ac)
    {
        var user = _db.Users.Where(x => !x.IsDelete);
        if (!string.IsNullOrEmpty(Id))
        {
            user = user.Where(x => x.Id.ToString() != Id);
        }
        var result = user.FirstOrDefault(x => x.Ac == Ac);
        return result != null;
    }
    /// <summary>
    /// 更新密碼
    /// </summary>
    /// <param name="updatePasswordDto"></param>
    /// <returns></returns>
    public async Task<string?> UpdatePassword(UpdatePasswordDto updatePasswordDto)
    {
        var result = string.Empty;
        updatePasswordDto.OldSw = _encryptHelper.HMACSHA256Password(updatePasswordDto.OldSw);
        var user = _db.Users.FirstOrDefault(x => !x.IsDelete &&
                                                  x.Is_Active &&
                                                  x.Ac == updatePasswordDto.Ac &&
                                                  x.Sw == updatePasswordDto.OldSw);
        if (user != null)
        {
            updatePasswordDto.NewSw = _encryptHelper.HMACSHA256Password(updatePasswordDto.NewSw);
            _mapper.Map(updatePasswordDto, user);
            _db.SaveChanges();
        }
        else
        {
            result = ReturnMessageEnum.UserLoginError.GetDisplayName();
        }
        return result;
    }
    public async Task<HolidayResponseDto> IsHoliday(HolidayRequestDto holidayRequestDto)
    {
        var result = new HolidayResponseDto();
        var exceptionList = new List<string>() { "0501" };
        holidayRequestDto.SearchDate = holidayRequestDto.SearchDate.Replace("/", string.Empty);
        string apiUrl = "https://data.ntpc.gov.tw/api/datasets/308dcd75-6434-45bc-a95f-584da4fed251/json?page=0&size=100000";
        using (HttpClient httpClient = new HttpClient())
        {
            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var jsonResult = JsonConvert.DeserializeObject<List<ApiResponseDto>>(content);
                var target = jsonResult.FirstOrDefault(x => x.date == holidayRequestDto.SearchDate);
                if (target != null)
                {
                    result.isHoliday = true;
                    result.holidayCategory = target.holidaycategory;
                }
                else if (exceptionList.Exists(x=> holidayRequestDto.SearchDate.EndsWith(x)))
                {
                    result.isHoliday = true;
                    result.holidayCategory = "勞動節";
                }
            }
        }
        return result;
    }
}
