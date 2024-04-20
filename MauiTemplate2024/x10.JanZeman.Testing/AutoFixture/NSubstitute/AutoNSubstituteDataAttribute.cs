namespace JanZeman.Testing.AutoFixture.NSubstitute;

public class AutoNSubstituteDataAttribute : AutoDataAttribute
{
    public AutoNSubstituteDataAttribute() : base(() => new Fixture().Customize(new AutoNSubstituteCustomization()))
    {
    }

    //public AutoNSubstituteDataAttribute(Type dataCustomizationType)
    //    : this(fixture => fixture.Customize((ICustomization)Activator.CreateInstance(dataCustomizationType)!))
    //{
    //}

    //private AutoNSubstituteDataAttribute(Action<IFixture> fixtureAction) : base(() =>
    //{
    //    var fixture = new Fixture();
    //    fixture.Customize(new AutoNSubstituteCustomization { ConfigureMembers = true });
    //    fixtureAction(fixture);
    //    return fixture;
    //})
    //{
    //}

    //public AutoNSubstituteDataAttribute() : this(_ => { })
    //{
    //}
}