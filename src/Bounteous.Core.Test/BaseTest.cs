using Bounteous.Core.Test.Fixtures;
using Xunit;

namespace Bounteous.Core.Test
{
    [CollectionDefinition("Tests")]
    public class BaseTest : ICollectionFixture<ModelFixture>
    {
    }
}