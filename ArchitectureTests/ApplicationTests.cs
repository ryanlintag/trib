using Application;
using FluentAssertions;
using NetArchTest.Rules;
using Xunit;

namespace ArchitectureTests
{
    public class ApplicationTests
    {
        [Fact]
        public void GetQueries_that_implements_SearchQuery_should_have_names_that_Start_with_Get_and_End_with_Query()
        {
            //Arrange
            var assembly = typeof(Application.AssemblyReference).Assembly;
            var searchQueryRecord = typeof(SearchQuery);
            //Act
            var result = Types.InAssembly(assembly)
                          .That()
                          .Inherit(searchQueryRecord)
                          .Should()
                          .HaveNameStartingWith("Get")
                          .And()
                          .HaveNameEndingWith("Query")
                          .GetResult();
            //Assert
            result.IsSuccessful.Should().BeTrue();
        }
        [Fact]
        public void GetQueries_that_implements_SearchQuery_should_be_sealed()
        {
            //Arrange
            var assembly = typeof(Application.AssemblyReference).Assembly;
            var searchQueryRecord = typeof(SearchQuery);
            //Act
            var result = Types.InAssembly(assembly)
                          .That()
                          .Inherit(searchQueryRecord)
                          .Should()
                          .BeSealed()
                          .GetResult();
            //Assert
            result.IsSuccessful.Should().BeTrue();
        }
        [Fact]
        public void IRepository_interfaces_inside_Repositories_Folder_should_end_in_RepositoryName()
        {
            //Arrange
            var assembly = typeof(Application.AssemblyReference).Assembly;
            var nameSpace = "Application.Repositories";
            //Act
            var result = Types.InAssembly(assembly)
                    .That()
                    .ResideInNamespace(nameSpace)
                    .Should()
                    .BeInterfaces()
                    .And()
                    .HaveNameEndingWith("Repository", StringComparison.Ordinal)
                    .GetResult();
            //Assert
            result.IsSuccessful.Should().BeTrue();
        }
    }
}
