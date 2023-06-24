using AutoMapper;
using CrazyCards.Application.Contracts.Deck;
using CrazyCards.Domain.Entities.Deck;

namespace CrazyCards.Application.Mappers;

internal sealed class DeckMapper : Profile
{
    public DeckMapper()
    {
        CreateMap<BattleDeck, BattleDeckResponse>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.IsDefault, o => o.MapFrom(s => s.IsDefault))
            .ForMember(d => d.PlayerDeck, o => o.MapFrom(s => s.PlayerDeck))
            .ForMember(d => d.Hero, o => o.MapFrom(s => s.Hero))
            .ForMember(d => d.Cards, o => o.MapFrom(s => s.Cards))
            .ReverseMap();
        
        CreateMap<PlayerDeck, PlayerDeckResponse>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.Player, o => o.MapFrom(s => s.Player))
            .ForMember(d => d.BattleDecks, o => o.MapFrom(s => s.BattleDecks))
            .ReverseMap();
    }
}