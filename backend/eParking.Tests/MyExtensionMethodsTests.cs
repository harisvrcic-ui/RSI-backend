using eParking.Helper;
using Xunit;

namespace eParking.Tests;

public class MyExtensionMethodsTests
{
    [Fact]
    public void RemoveTags_StripsHtmlTags()
    {
        var input = "<p>Hello</p><br/>World";
        var result = input.RemoveTags();
        Assert.Equal("HelloWorld", result);
    }

    [Fact]
    public void RemoveTags_EmptyString_ReturnsEmpty()
    {
        var result = "".RemoveTags();
        Assert.Equal("", result);
    }

    [Fact]
    public void Pluralize_City_ReturnsCities()
    {
        var result = "City".Pluralize();
        Assert.Equal("Cities", result);
    }

    [Fact]
    public void Pluralize_ClassWord_ReturnsClasses()
    {
        var result = "Class".Pluralize();
        Assert.Equal("Classes", result);
    }
}
