using Bounteous.Core.Extensions;
using FluentAssertions;
using Xunit;

namespace Bounteous.Core.Test.Core.Commands
{
    [Collection("base")]
    public class CompositeCommandTest
    {
        private int count;

        public CompositeCommandTest() => count = 1;


        [Fact]
        public void CanRunMultipleCommands()
        {
            new TestCommand(() => count += 5)
                .Then(new TestCommand(() => count += 10))
                .Run();
            count.Should().Be(16);
        }
    }
}