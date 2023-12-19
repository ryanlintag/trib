using FluentAssertions;
using NetArchTest.Rules;
using Presentation;
using Xunit;

namespace ArchitectureTests
{
    public class PresentationTests
    {
        [Fact]
        public void All_Presentation_EnpointDefinitions_should_inherit_from_IEndpointDefinitions()
        {
            //Arrange
            var presentationAssembly = typeof(Presentation.AssemblyReference).Assembly;
            var moduleNamespace = "Presentation.EndpointDefinitions";
            var endpointDefinitionInterface = typeof(IEndpointDefinition);

            //Act
            var result = Types.InAssembly(presentationAssembly)
                            .That()
                            .ResideInNamespaceStartingWith(moduleNamespace)
                            .Should()
                            .ImplementInterface(endpointDefinitionInterface)
                            .GetResult();

            //Assert
            result.IsSuccessful.Should().BeTrue();
        }
        [Fact]
        public void All_Presentation_EnpointDefinitions_should_be_sealed_and_public()
        {
            //Arrange
            var presentationAssembly = typeof(Presentation.AssemblyReference).Assembly;
            var moduleNamespace = "Presentation.EndpointDefinitions";

            //Act
            var result = Types.InAssembly(presentationAssembly)
                            .That()
                            .ResideInNamespaceStartingWith(moduleNamespace)
                            .Should()
                            .BeSealed()
                            .And()
                            .BePublic()
                            .GetResult();

            //Assert
            result.IsSuccessful.Should().BeTrue();
        }
        [Fact]
        public void All_Presentation_EnpointDefinitions_that_inherit_from_IEnpointDefinitions_should_end_in_EnpointDefinition_name()
        {
            //Arrange
            var presentationAssembly = typeof(Presentation.AssemblyReference).Assembly;
            var moduleNamespace = "Presentation.EndpointDefinitions";
            var IModuleInterfaceName = typeof(IEndpointDefinition);
            var endpointDefinitionPostName = "EndpointDefinition";

            //Act
            var result = Types.InAssembly(presentationAssembly)
                            .That()
                            .ResideInNamespaceStartingWith(moduleNamespace)
                            .And()
                            .ImplementInterface(IModuleInterfaceName)
                            .Should()
                            .HaveNameEndingWith(endpointDefinitionPostName)
                            .GetResult();

            //Assert
            result.IsSuccessful.Should().BeTrue();
        }
    }
}
