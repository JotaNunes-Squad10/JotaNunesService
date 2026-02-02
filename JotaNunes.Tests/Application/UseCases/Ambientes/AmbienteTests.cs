using JotaNunes.Application.UseCases.Ambiente.Commands.Handlers;
using JotaNunes.Application.UseCases.Ambiente.Commands.Requests;
using JotaNunes.Application.UseCases.Ambiente.Responses;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Tests.Application.UseCases.Base;
using NSubstitute;

namespace JotaNunes.Tests.Application.UseCases.Ambientes;

public class AmbienteTests : IClassFixture<ClassFixture>
{
    private readonly CreateAmbienteHandler _handler;
    private IAmbienteRepository repoMock { get; } = Substitute.For<IAmbienteRepository>();

    public AmbienteTests(ClassFixture fixture)
    {
        _handler = new CreateAmbienteHandler(fixture.DomainService, repoMock);
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

        await repoMock.Received().InsertAsync(
            Arg.Do<Domain.Models.Public.Ambiente>(a => _ = a)
        );

        Assert.NotNull(data);
        Assert.Equal(
            new { request.Nome, request.TopicoId },
            new { data.Nome, TopicoId = data.Topico.Id });
        Assert.True(result.ValidationResult.IsValid);
    }
}