using JotaNunes.Infrastructure.CrossCutting.Commons.Patterns.Response;
using MediatR;

namespace JotaNunes.Application.UseCases.Base.Commands;

public class BaseRequest : IRequest<DefaultResponse>
{
    public long Id { get; set; }
}