using AutoMapper;
using CrazyCards.Application.Contracts.Game;
using CrazyCards.Domain.Entities.Game;

namespace CrazyCards.Application.Mappers;

internal sealed class BattleMapper : Profile
{
    public BattleMapper()
    {
        CreateMap<Battle, BattleResponse>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.WinnerId, o => o.MapFrom(s => s.Winner))
            .ForMember(d => d.LoserId, o => o.MapFrom(s => s.Loser))
            .ForMember(d => d.StartTime, o => o.MapFrom(s => s.StartTime))
            .ForMember(d => d.EndTime, o => o.MapFrom(s => s.EndTime))
            .ForMember(d => d.Player1, o => o.MapFrom(s => s.Player1))
            .ForMember(d => d.Player2, o => o.MapFrom(s => s.Player2))
            .ForMember(d => d.Player1Deck, o => o.MapFrom(s => s.Player1Deck))
            .ForMember(d => d.Player2Deck, o => o.MapFrom(s => s.Player2Deck))
            .ForMember(d => d.Rounds, o => o.MapFrom(s => s.Rounds));
    }
}