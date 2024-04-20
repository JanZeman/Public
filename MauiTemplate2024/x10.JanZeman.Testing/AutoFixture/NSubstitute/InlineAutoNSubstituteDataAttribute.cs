namespace JanZeman.Testing.AutoFixture.NSubstitute;

/// <seealso cref="http://gist.github.com/3ken/2f2ea21d99fe24574a464c0e263751ed"/>
public class InlineAutoNSubstituteDataAttribute : InlineAutoDataAttribute
{
    public InlineAutoNSubstituteDataAttribute(params object[] objects) : base(new AutoNSubstituteDataAttribute(), objects)
    {
    }
}