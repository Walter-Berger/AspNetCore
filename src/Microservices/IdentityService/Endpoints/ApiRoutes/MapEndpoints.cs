namespace IdentityService.Endpoints.ApiRoutes;

public static class MapEndpoints
{
    public static IEndpointRouteBuilder MapApiEndpoints(this IEndpointRouteBuilder endpoints)
    {
        // map auth endpoints
        endpoints.MapLogin();
        endpoints.MapRegister();

        return endpoints;
    }
}