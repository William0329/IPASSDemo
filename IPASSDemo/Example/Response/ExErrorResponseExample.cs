using IPASSData.Dtos.Enums;
using IPASSData.Dtos.Shared;
using Swashbuckle.AspNetCore.Filters;

namespace IPASSDemo.Example.Response
{
    public class ExErrorResponseExample : IExamplesProvider<ResponseModel<object>>
    {
        public ResponseModel<object> GetExamples()
        {
            return new ResponseModel<object>()
            {
                Status = ResponseStatusEnum.error.ToString(),
                Message = "error msg",
                ResponseData = null
            };
        }
    }
}
