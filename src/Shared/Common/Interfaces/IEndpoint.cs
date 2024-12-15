using Microsoft.AspNetCore.Routing;

namespace Common.Extensions;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}
