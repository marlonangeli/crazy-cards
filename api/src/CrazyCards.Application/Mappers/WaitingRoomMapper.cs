using AutoMapper;
using CrazyCards.Application.Contracts.Game;
using CrazyCards.Domain.Entities.Game;

namespace CrazyCards.Application.Mappers;

internal sealed class WaitingRoomMapper : Profile
{
    public WaitingRoomMapper()
    {
        CreateMap<WaitingRoom, WaitingRoomResponse>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.Player, o => o.MapFrom(s => s.Player))
            .ForMember(d => d.BattleDeck, o => o.MapFrom(s => s.BattleDeck))
            .ForMember(d => d.IsWaiting, o => o.MapFrom(s => s.IsWaiting))
            .ForMember(d => d.Battle, o => o.MapFrom(s => s.Battle));
    }
}