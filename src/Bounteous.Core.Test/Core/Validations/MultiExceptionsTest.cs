using System;
using System.Linq;
using Bounteous.Core.Validations;
using FluentAssertions;
using Xunit;

namespace Bounteous.Core.Test.Core.Validations
{
    public class MultiExceptionsTest
    {
        [Fact]
        public void OneException()
        {
            var multi = new MultiException("one", new ValidationException("one"));
            multi.InnerExceptions.Count().Should().Be(1);
            multi.Message.Should().Be("one");
        }

        [Fact]
        public void MultiExceptions()
        {
            var multi = new MultiException("one",
                new[]
                {
                    new ValidationException("one"),
                    new ValidationException("two")
                });

            multi.InnerExceptions.Count().Should().Be(2);
            multi.Message.Should().Be($"one{Environment.NewLine}two");
        }
    }
}