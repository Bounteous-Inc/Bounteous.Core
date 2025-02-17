using System;
using Bounteous.Core.Test.Factories;
using Bounteous.Core.Test.Model;
using Bounteous.Core.Utilities;
using FluentAssertions;
using Xunit;

namespace Bounteous.Core.Test.Utilities
{
    [Collection("Test Models")]
    public class ReflectionEqualsTest
    {
        [Fact]
        public void Equal()
        {
            var angelina = FactoryGirl.Build<Person>();
            var angelinaClone = FactoryGirl.Build<Person>();

            angelina.ReflectionEquals(angelinaClone).Should().BeTrue();
        }

        [Fact]
        public void NotEqualFirstName()
        {
            Not((x, y) => x.FirstName = $"{y.FirstName}-x");
        }

        [Fact]
        public void NotEqualLastName()
        {
            Not((x, y) => x.LastName = $"{y.LastName}-x");
        }

        [Fact]
        public void NotEqualAge()
        {
            Not((x, y) => x.Age = y.Age - 1);
        }

        [Fact]
        public void NotEqualSocialSecurityNumber()
        {
            Not((x, y) => x.SocialSecurityNumber = y.SocialSecurityNumber - 1);
        }

        private static void Not(Action<Person, Person> initializer)
        {
            var first = FactoryGirl.Build<Person>();
            var notEqual = FactoryGirl.Build<Person>();
            initializer(first, FactoryGirl.Build<Person>());

            first.ReflectionEquals(notEqual).Should().BeFalse();
        }
    }
}