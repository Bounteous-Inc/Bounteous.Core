using Xunit;

namespace Bounteous.DotNet.Core.Test
{
    [CollectionDefinition("base")]
    public class BaseTest : ICollectionFixture<ModelFixture>
    {
    }
}