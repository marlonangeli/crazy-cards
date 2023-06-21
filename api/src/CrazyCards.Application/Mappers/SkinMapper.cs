using AutoMapper;
using CrazyCards.Application.Contracts.Images;
using CrazyCards.Application.Contracts.Skin;
using CrazyCards.Domain.Entities.Card;
using CrazyCards.Domain.Entities.Shared;

namespace CrazyCards.Application.Mappers;

public class SkinMapper : Profile
{
    public SkinMapper()
    {
        CreateMap<Skin, SkinResponse>()
            .IncludeBase<Image, ImageResponse>()
            .ForMember(d => d.Name, o => o.MapFrom(s => s.Name));
    }
}