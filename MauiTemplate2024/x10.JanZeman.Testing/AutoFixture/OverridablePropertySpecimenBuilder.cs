using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace JanZeman.Testing.AutoFixture;

/// <summary>
/// Fixture can populate read-only properties for the given type
/// </summary>
/// <see cref="http://stackoverflow.com/questions/47391406/autofixture-and-read-only-properties"/>
public class OverridablePropertySpecimenBuilder<TTarget, TProperty> : ISpecimenBuilder
{
    private readonly PropertyInfo _propertyInfo;
    private readonly TProperty _value;

    public OverridablePropertySpecimenBuilder(Expression<Func<TTarget, TProperty>> expr, TProperty value)
    {
        _propertyInfo = (expr.Body as MemberExpression)?.Member as PropertyInfo ?? throw new InvalidOperationException("Invalid property expression");
        _value = value;
    }

    public object Create(object request, ISpecimenContext context)
    {
        if (request is not ParameterInfo pi)
            return new NoSpecimen();

        var camelCase = Regex.Replace(_propertyInfo.Name, @"(\w)(.*)", m => m.Groups[1].Value.ToLower() + m.Groups[2]);

        if (pi.ParameterType != typeof(TProperty) || pi.Name != camelCase)
            return new NoSpecimen();

        return _value;
    }
}