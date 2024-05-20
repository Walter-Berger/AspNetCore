namespace AccountService.Endpoints;

public static class GetUser
{
    public static IEndpointRouteBuilder MapGetUser(this IEndpointRouteBuilder endpoints)
    {
        // Returns a single user with the given id
        endpoints.MapGet("/api/users/{id}", async (Guid id, ISender mediator, CancellationToken ct) =>
        {
            var qry = new GetUserQry(id);
            var result = await mediator.Send(qry, ct);
            var response = new GetUserResponse(
                Id: result.Id,
                Email: result.Email,
                FirstName: result.FirstName,
                LastName: result.LastName);

            return Results.Ok(response);
        })
        .RequireAuthorization();

        // Returns all users without parameters
        endpoints.MapGet("/api/users", async (ISender mediator, CancellationToken ct) =>
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
                    LastName: result.LastName));
            }

            return Results.Ok(responses);
        })
        .RequireAuthorization();

        return endpoints;
    }
}
