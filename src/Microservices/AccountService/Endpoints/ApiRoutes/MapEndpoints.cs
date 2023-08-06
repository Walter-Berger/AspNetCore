namespace AccountService.Endpoints.ApiRoutes;


public static class MapEndpoints
{
    public static IEndpointRouteBuilder MapApiEndpoints(this IEndpointRouteBuilder endpoints)
    {
        // map user endpoints
        endpoints.MapCreateUser();
        endpoints.MapGetUser();
        endpoints.MapGetAllUsers();
        endpoints.MapUpdateUser();
        endpoints.MapDeleteUser();

        return endpoints;
    }
}