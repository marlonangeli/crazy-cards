using AutoMapper;
using CrazyCards.Application.Contracts.Classes;
using CrazyCards.Application.Core.Classes.Commands.CreateClass;
using CrazyCards.Domain.Entities.Card;

namespace CrazyCards.Application.Mappers;

internal sealed class ClassMapper : Profile
{
    public ClassMapper()
    {
        CreateMap<Class, ClassResponse>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
            .ForMember(d => d.Description, o => o.MapFrom(s => s.Description))
            .ForMember(d => d.Image, o => o.MapFrom(s => s.Image))
            .ForMember(d => d.Skin, o => o.MapFrom(s => s.Skin))
            .ReverseMap();
        
        CreateMap<CreateClassCommand, Class>()
            .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
            .ForMember(d => d.Description, o => o.MapFrom(s => s.Description))
            .ForMember(d => d.ImageId, o => o.MapFrom(s => s.ImageId))
            .ForMember(d => d.SkinId, o => o.MapFrom(s => s.SkinId))
            .ReverseMap();
    }
}