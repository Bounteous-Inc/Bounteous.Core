using System;
using Bounteous.DotNet.Core.Commands;
using Bounteous.DotNet.Core.Extensions;
using Bounteous.DotNet.Core.Test.Factories;
using Xunit;

namespace Bounteous.DotNet.Core.Test
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