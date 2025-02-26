
using common;

namespace api.Endpoints
{
    public sealed class TestEndpoint : BaseEndpoint
    {
        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/testing");

            group.MapGet("", GetTest);
        }

        private async Task<IResult> GetTest()
        {
            throw new ProblemException("Test Code", "This is a test exception.");
            return Results.Ok("Hello, World!");
        }
    }
}
