using System;
using Bounteous.Core.Commands;
using Bounteous.Core.Extensions;
using Bounteous.Core.Test.Factories;
using Xunit;

namespace Bounteous.Core.Test
{
    public class ModelFixture : IDisposable
    {
        public ModelFixture() =>new ClearFactory().Then(new TestModelFactory()).Run();
        public void Dispose() => FactoryGirl.Clear(); 
    }

    public class ClearFactory : ICommand
    {
        public void Run() => FactoryGirl.Clear();
    }

    [CollectionDefinition("Test Models")]
    public class ModelCollection : ICollectionFixture<ModelFixture>
    {
    }
}