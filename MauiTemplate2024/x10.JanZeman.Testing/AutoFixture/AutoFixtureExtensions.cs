using System.Linq.Expressions;

namespace JanZeman.Testing.AutoFixture;

public static class AutoFixtureExtensions
{
    /// <summary>
    /// Fixture can populate constructor arguments for given type
    /// </summary>
    /// <see cref="http://stackoverflow.com/questions/16819470/autofixture-automoq-supply-a-known-value-for-one-constructor-parameter/16954699#16954699"/>
    public static IFixture WithConstructorArgumentFor<TTarget, TArgument>(this IFixture fixture, string argumentName, TArgument argumentValue)
    {
        fixture.Customizations.Add(new ConstructorArgumentSpecimenBuilder<TTarget, TArgument>(argumentName, argumentValue));
        return fixture;
    }

    /// <summary>
    /// Fixture can populate read-only properties for the given type
    /// </summary>
    /// <see cref="http://stackoverflow.com/questions/47391406/autofixture-and-read-only-properties"/>
    public static IFixture WithOverridablePropertyFor<TTarget, TProperty>(this IFixture fixture, Expression<Func<TTarget, TProperty>> expr, TProperty value)
    {
        fixture.Customizations.Add(new OverridablePropertySpecimenBuilder<TTarget, TProperty>(expr, value));
        return fixture;
    }
}