using IPASSData.Dtos.Authentication;

using Swashbuckle.AspNetCore.Filters;

namespace IPASSDemo.Example.Request.Authentication
{
    public class AddUserRequestExample : IExamplesProvider<AddUserDto>
    {
        public AddUserDto GetExamples()
        {
            return new AddUserDto
            {
                Username = "測試",
                Ac = "administrator@ch-si.com.tw",
                Sw = "1qaz@WSX",
            };
        }
    }
}
