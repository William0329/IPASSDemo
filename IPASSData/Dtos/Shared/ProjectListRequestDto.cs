using IPASSData.CustomAttribute.Validations;

namespace IPASSData.Dtos.Shared
{
    public class ProjectListRequestDto : ListRequestDto
    {
        /// <summary>
        /// 專案Id
        /// </summary>
        [RequiredCheck]
        public string ProjectId { get; set; }
    }
}
