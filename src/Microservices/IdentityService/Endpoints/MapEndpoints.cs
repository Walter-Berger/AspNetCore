namespace IdentityService.Endpoints;

public static class MapEndpoints
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder endpoints)
    {
        // map auth endpoints
        endpoints.MapLogin();
        endpoints.MapRegister();

        return endpoints;
    }
}