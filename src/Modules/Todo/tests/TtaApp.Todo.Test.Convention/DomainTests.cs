using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using NetArchTest.Rules;
using TtaApp.Shared.Domain.Base;
using Xunit;

namespace TtaApp.Todo.Test.Convention
{
    public class DomainTests
    {
        [Fact]
        public void DomainEvent_Should_Be_Immutable()
        {
            var types = Types.InAssembly(DomainAssembly)
                .That()
                .ImplementInterface(typeof(IDomainEvent))
                .GetTypes();

            AssertAreImmutable(types);
        }

        private static Assembly DomainAssembly 
            => typeof(Domain.Todos.Todo).Assembly;

        private static void AssertAreImmutable(
            IEnumerable<Type> types
        )
        {
            IList<Type> failingTypes = new List<Type>();
            foreach (var type in types)
            {
                if (type.GetFields().Any(x => !x.IsInitOnly) || type.GetProperties().Any(x => x.CanWrite))
                {
                    failingTypes.Add(type);
                    break;
                }
            }

            AssertFailingTypes(failingTypes);
        }

        private static void AssertFailingTypes(
            IEnumerable<Type> types
        )
        {
            types
                .Should()
                .BeNullOrEmpty();
        }
    }
}
