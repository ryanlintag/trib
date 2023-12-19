using Domain.Abstractions;
using FluentAssertions;
using NetArchTest.Rules;
using System.Reflection;
using Xunit;

namespace ArchitectureTests
{
    public class DomainTests
    {
        private static readonly Assembly DomainAssembly = Domain.AssemblyReference.Assembly;
        [Fact]
        public void DomainEvents_should_have_a_name_ending_in_DomainEvent()
        {
            //Arrange
            var domainEventType = typeof(IDomainEvent);

            //Act
            var result = Types.InAssembly(DomainAssembly)
                            .That()
                            .ImplementInterface(domainEventType)
                            .Should()
                            .HaveNameEndingWith("DomainEvent")
                            .GetResult();

            //Assert
            result.IsSuccessful.Should().BeTrue();
        }
        [Fact]
        public void EntityTypes_should_have_paramaterless_private_constructor()
        {
            //Arrange
            var entityType = typeof(Entity<EntityId>);
            var failingEntityTypes = new List<Type>();
            var entityTypes = Types.InAssembly(DomainAssembly)
                                    .That()
                                    .Inherit(entityType)
                                    .GetTypes();

            //Act
            foreach(var type in entityTypes)
            {
                var constructors = type.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);
                if(!constructors.Any(c => c.IsPrivate && c.GetParameters().Length == 0))
                {
                    failingEntityTypes.Add(type);
                }
            }

            //Assert
            failingEntityTypes.Should().BeEmpty();
        }
        [Fact]
        public void EntityTypes_should_never_have_paramaterless_public_constructor()
        {
            //Arrange
            var entityType = typeof(Entity<EntityId>);
            var failingEntityTypes = new List<Type>();
            var entityTypes = Types.InAssembly(DomainAssembly)
                                    .That()
                                    .Inherit(entityType)
                                    .GetTypes();

            //Act
            foreach (var type in entityTypes)
            {
                var constructors = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public);
                if (constructors.Any(c => c.IsPublic && c.GetParameters().Length == 0))
                {
                    failingEntityTypes.Add(type);
                }
            }

            //Assert
            failingEntityTypes.Should().BeEmpty();
        }
    }
}
