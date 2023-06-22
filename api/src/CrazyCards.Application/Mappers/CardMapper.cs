using AutoMapper;
using CrazyCards.Application.Contracts.Cards;
using CrazyCards.Application.Core.Cards.Commands.CreateCard;
using CrazyCards.Domain.Entities.Card;

namespace CrazyCards.Application.Mappers;

internal sealed class CardMapper : Profile
{
    public CardMapper()
    {
        CreateMap<CreateCardCommand, Card>()
            .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
            .ForMember(d => d.Description, o => o.MapFrom(s => s.Description))
            .ForMember(d => d.ImageId, o => o.MapFrom(s => s.ImageId))
            .ForMember(d => d.SkinId, o => o.MapFrom(s => s.SkinId))
            .ForMember(d => d.ClassId, o => o.MapFrom(s => s.ClassId))
            .ForMember(d => d.ManaCost, o => o.MapFrom(s => s.ManaCost))
            .ForMember(d => d.Type, o => o.MapFrom(s => s.Type))
            .ForMember(d => d.Rarity, o => o.MapFrom(s => s.Rarity))
            .ReverseMap();

        CreateMap<CreateCardCommand, MinionCard>()
            .IncludeBase<CreateCardCommand, Card>()
            .ForMember(d => d.Attack, o => o.MapFrom(s => s.AdditionalProperties[nameof(MinionCard.Attack)]))
            .ForMember(d => d.Health, o => o.MapFrom(s => s.AdditionalProperties[nameof(MinionCard.Health)]))
            .ReverseMap();

        CreateMap<CreateCardCommand, SpellCard>()
            .IncludeBase<CreateCardCommand, Card>()
            .ForMember(d => d.Damage, o => o.MapFrom(s => s.AdditionalProperties[nameof(SpellCard.Damage)]))
            .ForMember(d => d.Heal, o => o.MapFrom(s => s.AdditionalProperties[nameof(SpellCard.Heal)]))
            .ReverseMap();

        CreateMap<Card, CardResponse>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.ManaCost, o => o.MapFrom(s => s.ManaCost))
            .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
            .ForMember(d => d.Description, o => o.MapFrom(s => s.Description))
            .ForMember(d => d.Image, o => o.MapFrom(s => s.Image))
            .ForMember(d => d.Skin, o => o.MapFrom(s => s.Skin))
            .ForMember(d => d.Class, o => o.MapFrom(s => s.Class))
            .ForMember(d => d.Rarity, o => o.MapFrom(s => s.Rarity))
            .ForMember(d => d.Type, o => o.MapFrom(s => s.Type))
            .ForMember(d => d.Habilities, o => o.MapFrom(s => s.Habilities))
            .ReverseMap();
        
        CreateMap<MinionCard, CardResponse>()
            .IncludeBase<Card, CardResponse>()
            .ForMember(d => d.AdditionalProperties, o => o.MapFrom(s => new Dictionary<string, object>
            {
                { nameof(MinionCard.Attack), s.Attack },
                { nameof(MinionCard.Health), s.Health }
            }));
        
        CreateMap<SpellCard, CardResponse>()
            .IncludeBase<Card, CardResponse>()
            .ForMember(d => d.AdditionalProperties, o => o.MapFrom(s => new Dictionary<string, object>
            {
                { nameof(SpellCard.Damage), s.Damage },
                { nameof(SpellCard.Heal), s.Heal }
            }));
    }
}