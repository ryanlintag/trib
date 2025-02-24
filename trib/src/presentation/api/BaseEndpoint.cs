using Carter;

namespace api
{
    public abstract class BaseEndpoint : ICarterModule
    {
        public abstract void AddRoutes(IEndpointRouteBuilder app);
    }
}
