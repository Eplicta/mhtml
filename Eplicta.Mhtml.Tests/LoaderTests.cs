using System;
using System.Net.Http;
using Moq;
using Xunit;

namespace Eplicta.Mhtml.Tests;

public class LoaderTests
{
    [Fact]
    public void Load()
    {
        //Arrange
        var sut = new Loader(Mock.Of<HttpClient>());

        //Act
        var result = sut.Get(new Uri("https://eplicta.se/"));

        //Assert
    }
}