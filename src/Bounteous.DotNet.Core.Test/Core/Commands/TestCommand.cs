using System;
using Bounteous.DotNet.Core.Commands;

namespace Bounteous.DotNet.Core.Test.Core.Commands
{
    internal class TestCommand : ICommand
    {
        private readonly Action action;

        public TestCommand(Action action)=> this.action = action;
        public void Run() => action();
    }
}