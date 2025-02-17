using Bounteous.Core.Validations;
using FluentAssertions;
using Microsoft.CSharp.RuntimeBinder;
using Xunit;

namespace Bounteous.Core.Test.Core.Validations
{
    public class ValidationExceptionTest
    {
        [Fact]
        public void CanConstruct()
        {
            var validationException = new ValidationException(new RuntimeBinderException("poopoo"));
            validationException.Message.Should().Be("poopoo");
        }
    }
}