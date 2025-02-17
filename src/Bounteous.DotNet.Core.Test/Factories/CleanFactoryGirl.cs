using Bounteous.DotNet.Core.Commands;

namespace Bounteous.DotNet.Core.Test.Factories
{
    public class CleanFactoryGirl : ICommand
    {
        public void Run() => FactoryGirl.Clear();
    }
}