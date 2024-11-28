using IPASSData.Dtos.Enums;
using IPASSData.Dtos.Shared;
using Swashbuckle.AspNetCore.Filters;

namespace IPASSDemo.Example.Response
{
    public class ValidFailResponseExample : IExamplesProvider<ResponseModel<object>>
    {
        public ResponseModel<object> GetExamples()
        {
            return new ResponseModel<object>()
            {
                Status = ResponseStatusEnum.fail.ToString(),
                Message = "fail msg",
                ResponseData = null
            };
        }
    }
}
