using IPASSData.CustomAttribute.Validations;

namespace IPASSData.Dtos.Shared
{
    public class ProjectRequestDto
    {
        /// <summary>
        /// 專案Id
        /// </summary>
        [RequiredCheck]
        public string ProjectId { get; set; }
    }
}
