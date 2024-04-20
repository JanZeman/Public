namespace MauiTemplate2024.SqLiteStorage.Tests;

public class SqLiteStorageServiceTests
{
    [Theory]
    [AutoNSubstituteData]
    public async void CanStorePotatoes(IFixture fixture)
    {
        // Arrange
        var expectedPotatoes = fixture.Create<Potato>();
        var sqLiteStorageServiceSut = fixture.Build<SqLiteStorageService>().With(p => p.IsTest, true).Create();

        // Act
        var actualChangesCount = await sqLiteStorageServiceSut.SavePotato(expectedPotatoes);
        await sqLiteStorageServiceSut.Close();

        // Assert
        actualChangesCount.Should().Be(1);
    }
}