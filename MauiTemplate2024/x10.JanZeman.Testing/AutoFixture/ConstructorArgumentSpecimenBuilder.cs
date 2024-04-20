namespace JanZeman.Testing.AutoFixture;

/// <summary>
/// Fixture can populate constructor arguments for given type
/// </summary>
/// <see cref="http://stackoverflow.com/questions/16819470/autofixture-automoq-supply-a-known-value-for-one-constructor-parameter/16954699#16954699"/>
public class ConstructorArgumentSpecimenBuilder<TTarget, TArgument> : ISpecimenBuilder
{
    private readonly string _argumentName;
    private readonly TArgument _argumentValue;

    public ConstructorArgumentSpecimenBuilder(string argumentName, TArgument argumentValue)
    {
        _argumentName = argumentName ?? throw new ArgumentNullException(nameof(argumentName));
        _argumentValue = argumentValue ?? throw new ArgumentNullException(nameof(argumentValue));
    }

    public object Create(object request, ISpecimenContext context)
    {
        if (_argumentValue == null || request is not ParameterInfo pi || pi.Member.DeclaringType != typeof(TTarget) || pi.Member.MemberType != MemberTypes.Constructor || pi.ParameterType != typeof(TArgument) || pi.Name != _argumentName)
            return new NoSpecimen();

        return _argumentValue;
    }
}