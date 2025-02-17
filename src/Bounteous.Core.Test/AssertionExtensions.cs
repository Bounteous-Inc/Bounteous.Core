using Bounteous.Core.Test.Model;
using Bounteous.Core.Validations;

namespace Bounteous.Core.Test
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