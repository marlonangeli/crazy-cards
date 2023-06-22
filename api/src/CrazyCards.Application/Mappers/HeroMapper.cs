using AutoMapper;
using CrazyCards.Application.Contracts.Heroes;
using CrazyCards.Application.Core.Heroes.Commands;
using CrazyCards.Domain.Entities.Card;

namespace CrazyCards.Application.Mappers;

internal sealed class HeroMapper : Profile
{
    public HeroMapper()
    {
        CreateMap<Hero, HeroResponse>()
            .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
            .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
            .ForMember(d => d.Description, o => o.MapFrom(s => s.Description))
            .ForMember(d => d.Image, o => o.MapFrom(s => s.Image))
            .ForMember(d => d.Skin, o => o.MapFrom(s => s.Skin))
            .ForMember(d => d.Class, o => o.MapFrom(s => s.Class))
            .ForMember(d => d.Weapon, o => o.MapFrom(s => s.Weapon))
            .ReverseMap();
        
        CreateMap<CreateHeroCommand, Hero>()
            .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
            .ForMember(d => d.Description, o => o.MapFrom(s => s.Description))
            .ForMember(d => d.ImageId, o => o.MapFrom(s => s.ImageId))
            .ForMember(d => d.SkinId, o => o.MapFrom(s => s.SkinId))
            .ForMember(d => d.ClassId, o => o.MapFrom(s => s.ClassId))
            .ForMember(d => d.WeaponId, o => o.MapFrom(s => s.WeaponId))
            .ReverseMap();
    }
}