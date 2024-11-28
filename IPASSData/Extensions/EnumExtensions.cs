using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace IPASSData.Extensions
{
    /// <summary>
    /// Enum 擴充
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// 取得顯示名稱
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string? GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                           ?.GetName();
        }
        /// <summary>
        /// 取得描述
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string? GetDescription(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DescriptionAttribute>()
                           ?.Description;
        }
        /// <summary>
        /// 將所有列舉值聚合。
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="enumValues"></param>
        /// <returns></returns>
        public static TEnum Sum<TEnum>(this IEnumerable<TEnum> enumValues) where TEnum : struct, Enum
        {
            if (enumValues is null || enumValues.Count() == 0)
                return default(TEnum);

            return enumValues.Aggregate(Attach);
        }
        /// <summary>
        /// 將列舉的值加讓特定值。
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="augend">要被加的列舉。</param>
        /// <param name="addend">要加入的列舉。</param>
        /// <returns></returns>
        public static TEnum Attach<TEnum>(this TEnum augend, TEnum addend) where TEnum : struct, Enum
        {
            return EnumExpressionBuilder<TEnum>.AttachDelegate(augend, addend);
        }
    }
}
