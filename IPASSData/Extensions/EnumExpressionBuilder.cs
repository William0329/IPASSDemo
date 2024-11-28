using System.Linq.Expressions;

namespace IPASSData.Extensions
{
    internal static class EnumExpressionBuilder<TEnum> where TEnum : struct, Enum
    {
        static EnumExpressionBuilder()
        {
            var t = typeof(TEnum);
            var underly = Enum.GetUnderlyingType(t);

            var src = Expression.Parameter(t);
            var val = Expression.Parameter(t);

            var castedSrc = Expression.Convert(src, underly);
            var castedVal = Expression.Convert(val, underly);

            var implies = Expression.Equal(Expression.And(castedSrc, castedVal), castedVal);
            ImpliesDelegate = Expression.Lambda<Func<TEnum, TEnum, bool>>(implies, src, val).Compile();

            var attach = Expression.Convert(Expression.Or(castedSrc, castedVal), t);
            AttachDelegate = Expression.Lambda<Func<TEnum, TEnum, TEnum>>(attach, src, val).Compile();

            var detech = Expression.Convert(Expression.And(castedSrc, Expression.OnesComplement(castedVal)), t);
            DetachDelegate = Expression.Lambda<Func<TEnum, TEnum, TEnum>>(detech, src, val).Compile();

            IsDefaultDelegate = Expression.Lambda<Func<TEnum, bool>>(Expression.Equal(src, Expression.Default(t)), src).Compile();
        }

        internal readonly static Func<TEnum, TEnum, bool> ImpliesDelegate;
        internal readonly static Func<TEnum, TEnum, TEnum> AttachDelegate;
        internal readonly static Func<TEnum, TEnum, TEnum> DetachDelegate;

        internal readonly static Func<TEnum, bool> IsDefaultDelegate;
    }
}
