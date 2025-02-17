using Bounteous.DotNet.Core.Extensions;
using Bounteous.DotNet.Core.Test.Factories;
using Bounteous.DotNet.Core.Test.Model;
using Bounteous.DotNet.Core.Validations;
using Xunit;

namespace Bounteous.DotNet.Core.Test.Core.Extensions
{
    [Collection("Test Models")]
    public class UtilityExtensionsTest
    {
        [Fact]
        public void CanConvertToBase64()
        {
            var actual = FactoryGirl.Build<Person>();
            Validate.Begin()
                .ComparesTo<Person>(actual.ToJson().ToBase64().FromBase64().FromJson<Person>(), actual,
                    AssertionExtensions.Matches);
        }

        [Fact]
        public void CanSerialize()
        {
            var actual = FactoryGirl.Build<Person>();
            Validate.Begin()
                .ComparesTo<Person>(actual.ToJson().FromJson<Person>(), actual, AssertionExtensions.Matches);
        }

        [Fact]
        public void CanZip()
        {
            var actual = FactoryGirl.Build<Person>();
            Validate.Begin()
                .ComparesTo<Person>(actual.ToJson().Zip().Unzip().FromJson<Person>(), actual,
                    AssertionExtensions.Matches);
        }
    }
}