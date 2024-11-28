using System.ComponentModel.DataAnnotations;

namespace IPASSData.CustomAttribute.Validations
{
    /// <summary>
    /// 必填檢查
    /// </summary>
    public class RequiredCheck : RequiredAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            return !String.IsNullOrEmpty(ErrorMessage)
                ? ErrorMessage
                : $"請確認{name}是否填寫";
        }
    }

    /// <summary>
    /// 新增 據點
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class RequireTrongholdTypeCheck : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //var instance = validationContext.ObjectInstance as AddStrongholdDto;
            //Tronghold_Type類型, [1]:Service_Points(營業據點) 與 [2]: Factory(工廠)
            //if (instance != null && instance.Stronghold_Type == "Factory")
            //{
            //    if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            //    {
            //        var propertyName = validationContext.DisplayName; //取得屬性名稱

            //        return new ValidationResult($"請確認 {propertyName} 是否填寫");
            //    }
            //}
            var instance = validationContext.ObjectInstance;
            var strongholdTypeProperty = instance.GetType().GetProperty("Stronghold_Type");

            
            if (strongholdTypeProperty != null)
            {
                var strongholdTypeValue = strongholdTypeProperty.GetValue(instance) as string;

                if (strongholdTypeValue == "Factory")
                {
                    if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                    {
                        var propertyName = validationContext.DisplayName; //取得屬性名稱

                        return  new ValidationResult($"請確認 {propertyName} 是否填寫");
                    }
                }
            }

            return ValidationResult.Success;
        }
    }
}
