namespace AccountService.Endpoints;

public static class GetAllUsers
{
    public static IEndpointRouteBuilder MapGetAllUsers(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(EndpointRoutes.User.GetAll, async (ISender mediator, CancellationToken ct) =>
        {
            var qry = new GetAllUsersQry();
            var results = await mediator.Send(qry, ct);

            var responses = new List<GetUserResponse>();
            foreach (var result in results)
            {
                responses.Add(new GetUserResponse(
                    Id: result.Id,
                    Email: result.Email,
                    FirstName: result.FirstName,
                    LastName: result.LastName,
                    BirthDate: result.BirthDate));
            }

            return Results.Ok(responses);
        })
        .RequireAuthorization();

        return endpoints;
    }
}
