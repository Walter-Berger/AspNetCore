namespace BookService.Endpoints.ApiRoutes;

public static class MapEndpoints
{
    public static IEndpointRouteBuilder MapApiEndpoints(this IEndpointRouteBuilder endpoints)
    {
        // map book endpoints
        endpoints.MapCreateBook();
        endpoints.MapBuyBook();
        endpoints.MapLoanBook();
        endpoints.MapGetAllBooks();
        endpoints.MapGetBook();
        endpoints.MapUpdateBook();
        endpoints.MapDeleteBook();

        return endpoints;
    }
}