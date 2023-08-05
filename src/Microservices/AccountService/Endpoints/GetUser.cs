namespace AccountService.Endpoints;

public static class GetUser
{
    public static IEndpointRouteBuilder MapGetUser(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(EndpointRoutes.User.Get, async (Guid id, ISender mediator, CancellationToken ct) =>
        {
            var qry = new GetUserQry(id);
            var result = await mediator.Send(qry, ct);
            var response = new GetUserResponse(
                Id: result.Id,
                Email: result.Email,
                FirstName: result.FirstName,
                LastName: result.LastName,
                BirthDate: result.BirthDate);

            return Results.Ok(response);
        })
        .RequireAuthorization();

        return endpoints;
    }
}
  