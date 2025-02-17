using Bounteous.Core.Commands;

namespace Bounteous.Core.Test.Factories
{
    public class CleanFactoryGirl : ICommand
    {
        public void Run() => FactoryGirl.Clear();
    }
}