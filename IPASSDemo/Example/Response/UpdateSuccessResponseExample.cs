using IPASSData.Dtos.Enums;
using IPASSData.Dtos.Shared;
using Swashbuckle.AspNetCore.Filters;

namespace IPASSDemo.Example.Response
{
    public class UpdateSuccessResponseExample : IExamplesProvider<ResponseModel<bool>>
    {
        public ResponseModel<bool> GetExamples()
        {
            return new ResponseModel<bool>
            {
                Status = ResponseStatusEnum.success.ToString("G"),
                Message = string.Empty,
                ResponseData = true
            };
        }
    }
}
