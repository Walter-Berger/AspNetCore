namespace BookService.Endpoints;

public static class MapEndpoints
{
    public static IEndpointRouteBuilder MapBookEndpoints(this IEndpointRouteBuilder endpoints)
    {
        // map book endpoints
        endpoints.MapCreateBook();
        endpoints.MapBuyBook();
        endpoints.MapLoanBook();
        endpoints.MapGetBook();
        endpoints.MapUpdateBook();
        endpoints.MapDeleteBook();

        return endpoints;
    }
}