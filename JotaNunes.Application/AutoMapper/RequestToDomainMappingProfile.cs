using AutoMapper;
using JotaNunes.Application.UseCases.Item.Commands.Requests;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models;

namespace JotaNunes.Application.AutoMapper;

public class RequestToDomainMappingProfile : Profile
{
    public RequestToDomainMappingProfile(IUser user)
    {
        CreateMap<CreateItemRequest, Item>().CreateMapper(user);
    }
}