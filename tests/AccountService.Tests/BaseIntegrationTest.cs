using AccountService.Data;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AccountService.Tests;

public abstract class BaseIntegrationTest : IClassFixture<EndpointFactory>
{
    private readonly IServiceScope _scope;
    protected readonly ISender Sender;
    protected readonly DatabaseContext DbContext;

    protected BaseIntegrationTest(EndpointFactory factory)
    {
        _scope = factory.Services.CreateScope();

        Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
        DbContext = _scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    }
}
