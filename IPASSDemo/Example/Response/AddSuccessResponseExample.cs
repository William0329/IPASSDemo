using IPASSData.Dtos.Enums;
using IPASSData.Dtos.Shared;
using Swashbuckle.AspNetCore.Filters;

namespace IPASSDemo.Example.Response
{
    public class AddSuccessResponseExample : IExamplesProvider<ResponseModel<string>>
    {
        public ResponseModel<string> GetExamples()
        {
            return new ResponseModel<string>
            {
                Status = ResponseStatusEnum.success.ToString("G"),
                Message = string.Empty,
                ResponseData = Guid.NewGuid().ToString()
            };
        }
    }
}
