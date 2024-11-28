using IPASSData.Dtos.Authentication;

using Swashbuckle.AspNetCore.Filters;

namespace IPASSDemo.Example.Request.Authentication
{
    public class UpdatePasswordRequestExample : IExamplesProvider<UpdatePasswordDto>
    {
        public UpdatePasswordDto GetExamples()
        {
            return new UpdatePasswordDto
            {
                GroupId = "4c24e405-7c66-478f-b611-b27b25565b70",
                Ac = "administrator@ch-si.com.tw",
                OldSw = "1qaz@WSX",
                NewSw = "1qaz@WSX3edc"
            };
        }
    }
}
