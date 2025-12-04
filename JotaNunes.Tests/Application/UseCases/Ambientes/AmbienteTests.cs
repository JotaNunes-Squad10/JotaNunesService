using JotaNunes.Application.UseCases.Ambiente.Commands.Handlers;
using JotaNunes.Application.UseCases.Ambiente.Commands.Requests;
using JotaNunes.Application.UseCases.Ambiente.Responses;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using JotaNunes.Tests.Application.UseCases.Base;
using NSubstitute;

namespace JotaNunes.Tests.Application.UseCases.Ambientes;

public class AmbienteTests : IClassFixture<ClassFixture>
{
    private readonly CreateAmbienteHandler _handler;

    public AmbienteTests(ClassFixture fixture)
    {
        var domainService = fixture.DomainService;
        var repoMock = Substitute.For<IAmbienteRepository>();
        _handler = new CreateAmbienteHandler(domainService, repoMock);
    }

    [Fact]
    public async Task CreateAmbiente()
    {
        var request = new CreateAmbienteRequest
        {
            Nome = "Academia",
            TopicoId = 1
        };

        var result = await _handler.Handle(request, CancellationToken.None);

        var data = Assert.IsType<AmbienteResponse>(result.Data);

        Assert.NotNull(result);
        Assert.NotNull(data);
        Assert.Equal(request.Nome, data.Nome);
        Assert.True(result.ValidationResult.IsValid);
    }
}