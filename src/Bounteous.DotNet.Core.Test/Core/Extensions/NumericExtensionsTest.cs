using Bounteous.DotNet.Core.Extensions;
using FluentAssertions;
using Xunit;

namespace Bounteous.DotNet.Core.Test.Core.Extensions
{
    public class NumericExtensionsTest
    {
        [Fact]
        public void DoubleRoundedTo() => double.Parse("6.01").RoundedTo(2).Should().Be(6.01);
    }
}