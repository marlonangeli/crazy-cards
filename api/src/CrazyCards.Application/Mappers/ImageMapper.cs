using AutoMapper;
using CrazyCards.Application.Contracts.Images;
using CrazyCards.Domain.Entities.Shared;

namespace CrazyCards.Application.Mappers;

internal sealed class ImageMapper : Profile
{
    public ImageMapper()
    {
        CreateMap<Image, ImageResponse>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.Size, o => o.MapFrom(s => s.Size))
            .ForMember(d => d.MimeType, o => o.MapFrom(s => s.MimeType))
            .ReverseMap();
    }
}