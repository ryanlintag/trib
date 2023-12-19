using NetArchTest.Rules;
using Xunit;
using FluentAssertions;

namespace ArchitectureTests
{
    public class ProjectDependencyTests
    {

        [Fact]
        public void Domain_should_not_have_any_dependency_from_any_local_projects()
        {
            //Arrange
            var assembly = typeof(Domain.AssemblyReference).Assembly;
            var otherAssemblies = new[] {
                NamespaceConstants.ApplicationNamespace,
                NamespaceConstants.InfrastructureNamespace,
                NamespaceConstants.PersistanceNamespace,
                NamespaceConstants.PresentationNamespace
            };

            //Act
            var testResult = Types
                .InAssembly(assembly)
                .Should()
                .NotHaveDependencyOnAny(otherAssemblies)
                .GetResult();

            //Assert
            testResult.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void Application_should_not_have_any_dependency_from_any_other_assembly_than_Domain()
        {
            //Arrange
            var assembly = typeof(Application.AssemblyReference).Assembly;
            var otherAssemblies = new[] {
                NamespaceConstants.InfrastructureNamespace,
                NamespaceConstants.PersistanceNamespace,
                NamespaceConstants.PresentationNamespace
            };


            //Act
            var testResult = Types
                .InAssembly(assembly)
                .Should()
                .NotHaveDependencyOnAny(otherAssemblies)
                .GetResult();

            //Assert
            testResult.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void Infrastructure_should_not_have_any_dependency_from_any_other_assembly_than_Domain_and_Application()
        {
            //Arrange
            var assembly = typeof(Infrastructure.AssemblyReference).Assembly;
            var otherAssemblies = new[] {
                NamespaceConstants.DomainNamespace,
                NamespaceConstants.PresentationNamespace,
                NamespaceConstants.PersistanceNamespace
            };

            //Act
            var testResult = Types
                .InAssembly(assembly)
                .Should()
                .NotHaveDependencyOnAny(otherAssemblies)
                .GetResult();

            //Assert
            testResult.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void Persistance_should_not_have_any_dependency_from_any_other_assembly_than_Domain_and_Application()
        {
            //Arrange
            var assembly = typeof(Persistance.AssemblyReference).Assembly;
            var otherAssemblies = new[] {
                NamespaceConstants.PresentationNamespace,
                NamespaceConstants.InfrastructureNamespace
            };

            //Act
            var testResult = Types
                .InAssembly(assembly)
                .Should()
                .NotHaveDependencyOnAny(otherAssemblies)
                .GetResult();

            //Assert
            testResult.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void Presentation_should_not_have_any_dependency_from_any_other_assembly_than_Domain_and_Application()
        {
            //Arrange
            var assembly = typeof(Presentation.AssemblyReference).Assembly;
            var otherAssemblies = new[] {
                NamespaceConstants.InfrastructureNamespace,
                NamespaceConstants.PersistanceNamespace
            };

            //Act
            var testResult = Types
                .InAssembly(assembly)
                .Should()
                .NotHaveDependencyOnAny(otherAssemblies)
                .GetResult();

            //Assert
            testResult.IsSuccessful.Should().BeTrue();
        }
    }
}
