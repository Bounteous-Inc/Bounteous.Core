using Bounteous.Core.Extensions;
using FluentAssertions;
using Xunit;

namespace Bounteous.Core.Test.Core.Extensions
{
    public class NumericExtensionsTest
    {
        [Fact]
        public void DoubleRoundedTo() => double.Parse("6.01").RoundedTo(2).Should().Be(6.01);
    }
}