using IPASSData.Dtos.Authentication;

using Swashbuckle.AspNetCore.Filters;

namespace IPASSDemo.Example.Request.Authentication
{
    public class UpdateUserRequestExample : IExamplesProvider<UpdateUserDto>
    {
        public UpdateUserDto GetExamples()
        {
            return new UpdateUserDto
            {
                Id = "e35fd1f0-9f3e-4f94-94bf-b6903960887d",
                Username = "測試更新",
                Ac = "administrator@ch-si.com.tw",
                Sw = "",
                LegalEntityId = "75bb83d4-7ce7-4920-9024-f83ca31a675a",
                GroupId = "4c24e405-7c66-478f-b611-b27b25565b70",
                Is_Active = true
            };
        }
    }
}
