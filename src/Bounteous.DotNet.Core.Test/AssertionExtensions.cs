using Bounteous.DotNet.Core.Test.Model;
using Bounteous.DotNet.Core.Validations;

namespace Bounteous.DotNet.Core.Test
{
    public static class AssertionExtensions
    {
        public static void Matches(Validation validation, Person actual, Person expected)
            => validation.IsNotNull(actual, "actual")
                .IsNotNull(expected, "expected")
                .Check()
                .IsEqual(actual.FirstName, expected.FirstName, "firstName")
                .IsEqual(actual.LastName, expected.LastName, "LastName")
                .Check();
    }
}