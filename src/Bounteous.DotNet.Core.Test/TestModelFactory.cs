using Bounteous.DotNet.Core.Commands;
using Bounteous.DotNet.Core.Test.Factories;
using Bounteous.DotNet.Core.Test.Model;

namespace Bounteous.DotNet.Core.Test
{
    public class TestModelFactory : ICommand
    {
        public void Run() => FactoryGirl.Define(() => new Person
        {
            FirstName = "Angelina",
            LastName = "Jolie",
            Age = 25,
            SocialSecurityNumber = 12345
        });
    }
}