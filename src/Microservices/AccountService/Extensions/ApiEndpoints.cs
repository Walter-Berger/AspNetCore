namespace AccountService.Extensions;


public static class ApiEndpoints
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