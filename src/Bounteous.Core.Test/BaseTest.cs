using Xunit;

namespace Bounteous.Core.Test
{
    [CollectionDefinition("base")]
    public class BaseTest : ICollectionFixture<ModelFixture>
    {
    }
}