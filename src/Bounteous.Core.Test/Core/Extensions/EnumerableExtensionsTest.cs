using Bounteous.Core.Extensions;
using FluentAssertions;
using Xunit;

namespace Bounteous.Core.Test.Core.Extensions
{
    public class EnumerableExtensionsTest
    {
        [Theory]
        [InlineData("dxpw-admin", "nutrien.user@nutrien.com", "nutrien.user@nutrien.com")]
        [InlineData("testuser22@nutrien.com", "dwpw-admin", "testuser22@nutrien.com")]
        [InlineData("dxpw-admin", "Joan.Lynn.External@nutrien.com", "Joan.Lynn.External@nutrien.com")]
        [InlineData("dxpw-admin", "stagedaniel@gmail.test", "stagedaniel@gmail.test")]
        [InlineData("dxpw-admin", "newemail@gmail.ca", "newemail@gmail.ca")]
        public void GetEmail(string item1, string item2, string expected)
            => new[] { item1, item2 }.GetEmail().Should().Be(expected);

        [Fact]
        public void NoEmail() => new[] { "dxpw-admin", "nutrien.user@", "blah" }.GetEmail().Should().BeNull();
    }
}