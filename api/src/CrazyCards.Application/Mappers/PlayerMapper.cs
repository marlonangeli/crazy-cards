using AutoMapper;
using CrazyCards.Application.Contracts.Players;

namespace CrazyCards.Application.Mappers;

internal sealed class PlayerMapper : Profile
{
    public PlayerMapper()
    {
        CreateMap<Domain.Entities.Player.Player, PlayerResponse>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.Username, o => o.MapFrom(s => s.Username))
            .ForMember(d => d.Email, o => o.MapFrom(s => s.Email))
            .ForMember(d => d.PlayerDeck, o => o.MapFrom(s => s.PlayerDeck))
            .ReverseMap();
    }
}