using FluentAssertions;

namespace MauiTemplate2024.Core.Tests.Logic;

public class LogicServiceTests
{
    [Theory]
    [AutoNSubstituteData]
    public void ItemsListIsEmptyIfNotInitialized(IFixture fixture)
    {
        // Arrange
        var logicServiceSut = fixture.Create<LogicService>();

        // Act
        var actualItems = logicServiceSut.FilteredItems;

        // Assert
        actualItems.Should().BeEmpty();
    }
}