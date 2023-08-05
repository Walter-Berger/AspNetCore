namespace IdentityService.Endpoints;

public static class Login
{
    public static IEndpointRouteBuilder MapLogin(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(EndpointRoutes.Auth.Login, async (HttpContext context, ICredentialService credentialService, ISender mediator, CancellationToken ct) =>
        {
            var authHeader = context.Request.Headers.Authorization.ToString();
            var (userName, password) = credentialService.ExtractUsernameAndPassword(authHeader);

            var qry = new LoginQry(userName, password);
            var result = await mediator.Send(qry, ct);
            var response = new LoginResponse(result.accessToken);
            return Results.Ok(response);
        });

        return endpoints;
    }
}