using System;
using System.Threading.Tasks;
using Bounteous.Core.Commands;
using Bounteous.Core.Extensions;
using FluentAssertions;
using Xunit;

namespace Bounteous.Core.Test.Core.Commands
{
    [Collection("base")]
    public class CompositeAwaitedCommandTest
    {
        private int count;

        public CompositeAwaitedCommandTest() => count = 1;

        [Fact]
        public async Task CanRunMultipleCommands()
        {
            await new TestWaitedCommand(() => count = 5)
                .Then(new TestWaitedCommand(() => count = 10))
                .RunAsync();
            count.Should().Be(10);
        }
    }

    internal class TestWaitedCommand : IWaitedCommand
    {
        private readonly Action action;
        public TestWaitedCommand(Action action) => this.action = action;
        public async Task RunAsync() => await Task.Run(() => action());
    }
}