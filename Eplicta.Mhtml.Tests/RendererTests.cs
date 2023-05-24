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
        var sut = new Renderer(Mock.Of<PageData>(x => x.MainContent == "<html><head><title>AAA</title></head><body><p>Test AAA</p><img src=\"https://i.stack.imgur.com/yrOh8b.jpg\" /></body></html>"));

        //Act
        var result = sut.GetStream();

        //Assert
        result.ToArray().Should().NotBeNull();
    }
}