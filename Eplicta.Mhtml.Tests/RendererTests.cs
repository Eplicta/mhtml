using FluentAssertions;
using Moq;
using Xunit;

namespace Eplicta.Mhtml.Tests;

public class RendererTests
{
    [Fact]
    public void Render()
    {
        //Arrange
        var sut = new Renderer(Mock.Of<PageData>());

        //Act
        var result = sut.GetStream();

        //Assert
        result.ToArray().Should().NotBeNull();
    }
}