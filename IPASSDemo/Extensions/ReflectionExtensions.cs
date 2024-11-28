using System.Reflection;

namespace IPASSDemo.Extensions
{
    public static partial class ReflectionExtensions
    {
        public static bool IsDefined<TAttribute>(this MemberInfo target) where TAttribute : Attribute
        {
            return target?.IsDefined(typeof(TAttribute)) ?? false;
        }
        /// <summary>
        /// 是否為IEnumerable 類型
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static bool IsIEnumerable(this PropertyInfo property)
        {
            return property.PropertyType != typeof(string) &&
                   property.PropertyType.GetInterfaces().Contains(typeof(System.Collections.IEnumerable));
        }
    }
}
