using JotaNunes.Application.UseCases.Base.Commands;

namespace JotaNunes.Application.UseCases.Material.Commands.Requests;

public class UpdateMaterialRequest : BaseRequest
{
    public string? Nome { get; set; }
}