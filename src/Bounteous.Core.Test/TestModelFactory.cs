using Bounteous.Core.Commands;
using Bounteous.Core.Test.Factories;
using Bounteous.Core.Test.Model;

namespace Bounteous.Core.Test
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