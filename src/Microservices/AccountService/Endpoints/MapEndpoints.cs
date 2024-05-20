namespace AccountService.Endpoints;

public static class MapEndpoints
{
    public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder endpoints)
    {
        // map user endpoints
        endpoints.MapCreateUser();
        endpoints.MapGetUser();
        endpoints.MapUpdateUser();
        endpoints.MapDeleteUser();

        return endpoints;
    }
}