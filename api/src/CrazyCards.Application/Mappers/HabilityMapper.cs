using AutoMapper;
using CrazyCards.Application.Contracts.Hability;
using CrazyCards.Application.Core.Hability.Commands;
using CrazyCards.Domain.Entities.Card.Hability;
using Action = CrazyCards.Domain.Entities.Card.Hability.Action;

namespace CrazyCards.Application.Mappers;

internal sealed class HabilityMapper : Profile
{
    public HabilityMapper()
    {
        CreateMap<CreateHabilityCommand, Hability>()
            .ForMember(d => d.CardId, o => o.MapFrom(s => s.CardId))
            .ForMember(d => d.Type, o => o.MapFrom(s => s.Type))
            .ForMember(d => d.Action, o => o.MapFrom(s => s.Action))
            .ReverseMap();

        CreateMap<CreateHabilityCommand, TauntHability>()
            .IncludeBase<CreateHabilityCommand, Hability>()
            .ReverseMap();
        
        CreateMap<CreateHabilityCommand, BattlecryHability>()
            .IncludeBase<CreateHabilityCommand, Hability>()
            .ReverseMap();
        
        CreateMap<CreateHabilityCommand, LastBreathHability>()
            .IncludeBase<CreateHabilityCommand, Hability>()
            .ReverseMap();

        CreateMap<Hability, HabilityResponse>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.Card, o => o.MapFrom(s => s.Card))
            .ForMember(d => d.Action, o => o.MapFrom(s => s.Action))
            .ForMember(d => d.Type, o => o.MapFrom(s => s.Type))
            .ReverseMap();
        
        CreateMap<TauntHability, HabilityResponse>()
            .IncludeBase<Hability, HabilityResponse>()
            .ForMember(d => d.Name, o => o.MapFrom(s => TauntHability.Name))
            .ForMember(d => d.Description, o => o.MapFrom(s => TauntHability.Description))
            .ReverseMap();
        
        CreateMap<BattlecryHability, HabilityResponse>()
            .IncludeBase<Hability, HabilityResponse>()
            .ForMember(d => d.Name, o => o.MapFrom(s => BattlecryHability.Name))
            .ForMember(d => d.Description, o => o.MapFrom(s => BattlecryHability.Description))
            .ReverseMap();
        
        CreateMap<LastBreathHability, HabilityResponse>()
            .IncludeBase<Hability, HabilityResponse>()
            .ForMember(d => d.Name, o => o.MapFrom(s => LastBreathHability.Name))
            .ForMember(d => d.Description, o => o.MapFrom(s => LastBreathHability.Description))
            .ReverseMap();
        
        CreateMap<CreateActionRequest, Action>()
            .ForMember(d => d.Description, o => o.MapFrom(s => s.Description))
            .ForMember(d => d.InvokeCardId, o => o.MapFrom(s => s.InvokeCardId))
            .ForMember(d => d.Damage, o => o.MapFrom(s => s.Damage))
            .ForMember(d => d.Heal, o => o.MapFrom(s => s.Heal))
            .ForMember(d => d.Shield, o => o.MapFrom(s => s.Shield))
            .ForMember(d => d.DamageToAll, o => o.MapFrom(s => s.DamageToAll))
            .ForMember(d => d.HealToAll, o => o.MapFrom(s => s.HealToAll))
            .ForMember(d => d.ShieldToAll, o => o.MapFrom(s => s.ShieldToAll))
            .ReverseMap();
        
        CreateMap<Action, ActionResponse>()
            .ForMember(d => d.Description, o => o.MapFrom(s => s.Description))
            .ForMember(d => d.InvokeCard, o => o.MapFrom(s => s.InvokeCard))
            .ForMember(d => d.InvokeCardType, o => o.MapFrom(s => s.InvokeCardType))
            .ForMember(d => d.Damage, o => o.MapFrom(s => s.Damage))
            .ForMember(d => d.Heal, o => o.MapFrom(s => s.Heal))
            .ForMember(d => d.Shield, o => o.MapFrom(s => s.Shield))
            .ForMember(d => d.DamageToAll, o => o.MapFrom(s => s.DamageToAll))
            .ForMember(d => d.HealToAll, o => o.MapFrom(s => s.HealToAll))
            .ForMember(d => d.ShieldToAll, o => o.MapFrom(s => s.ShieldToAll))
            .ReverseMap();
    }
}