using System;
using Bounteous.Core.Commands;

namespace Bounteous.Core.Test.Core.Commands
{
    internal class TestCommand : ICommand
    {
        private readonly Action action;

        public TestCommand(Action action)=> this.action = action;
        public void Run() => action();
    }
}