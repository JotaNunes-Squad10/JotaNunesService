using AutoMapper;
using JotaNunes.Domain.Interfaces;
using JotaNunes.Domain.Models.Base;

namespace JotaNunes.Application.AutoMapper;

public static class AutoMapperExtension
{
    public static IMappingExpression<TSource, TDestination> CreateMapper<TSource, TDestination>(
        this IMappingExpression<TSource, TDestination> map, IUser user)
        where TDestination : BaseAuditEntity
    {
        map.ForMember(d => d.UsuarioInclusaoId, opt => opt.MapFrom(x => user.Id));
        map.ForMember(d => d.DataHoraInclusao, opt => opt.MapFrom(x => DateTime.Now));
        map.ForMember(d => d.UsuarioAlteracaoId, opt => opt.MapFrom(x => user.Id));
        map.ForMember(d => d.DataHoraAlteracao, opt => opt.MapFrom(x => DateTime.Now));
        map.ForMember(d => d.Excluido, opt => opt.MapFrom(x => false));

        return map;
    }

    public static IMappingExpression<TSource, TDestination> UpdateMapper<TSource, TDestination>(
        this IMappingExpression<TSource, TDestination> map, IUser user)
        where TDestination : BaseAuditEntity
    {
        map.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        map.ForMember(d => d.UsuarioAlteracaoId, opt => opt.MapFrom(x => user.Id));
        map.ForMember(d => d.DataHoraAlteracao, opt => opt.MapFrom(x => DateTime.Now));

        return map;
    }
}