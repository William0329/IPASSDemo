using IPASSData.Dtos.Authentication;
using IPASSData.Dtos.Enums;
using IPASSData.Dtos.Shared;
using Swashbuckle.AspNetCore.Filters;

namespace IPASSDemo.Example.Response.Authentication
{
    public class LoginSuccessResponseExample : IExamplesProvider<ResponseModel<LoginResponseDto>>
    {
        public ResponseModel<LoginResponseDto> GetExamples()
        {
            return new ResponseModel<LoginResponseDto>
            {
                Status = ResponseStatusEnum.success.ToString("G"),
                Message = string.Empty,
                ResponseData = new LoginResponseDto
                {
                    Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImFkbWluaXN0cmF0b3JAY2gtc2kuY29tLnR3IiwianRpIjoiNWUzZGI3MjgtNzQ5OC00MmMxLTk5NTYtNjg4MmM0OGFlODEwIiwibmJmIjoxNjkzOTY4NzE5LCJleHAiOjE2OTM5NzA1MTksImlhdCI6MTY5Mzk2ODcxOSwiaXNzIjoiQ0JBTSBTZXJ2aWNlIn0.HDonJ_lT4cHs21x5hbSNB8rIXhXX13xUvabLGGsCGdM",
                    Id = "6018765b-4232-410a-8aee-8c1a53899803",
                    Name = "測試",
                    Email = "administrator@ch-si.com.tw",
                }
            };
        }
    }
}
