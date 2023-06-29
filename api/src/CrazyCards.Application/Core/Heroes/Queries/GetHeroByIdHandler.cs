using AutoMapper;
using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Heroes;
using CrazyCards.Application.Interfaces;
using CrazyCards.Domain.Entities.Card;
using CrazyCards.Domain.Primitives;
using CrazyCards.Domain.Primitives.Result;
using Microsoft.EntityFrameworkCore;

namespace CrazyCards.Application.Core.Heroes.Queries;

internal sealed class GetHeroByIdHandler : IQueryHandler<GetHeroByIdQuery, Result<HeroResponse>>
{
    private readonly IDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetHeroByIdHandler(IDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Result<HeroResponse>> Handle(GetHeroByIdQuery request, CancellationToken cancellationToken)
    {
        var hero = await _dbContext.Set<Hero>()
            .AsNoTracking()
            .IgnoreAutoIncludes()
            .Include(i => i.Class)
            .Include(i => i.Skin)
            .Include(i => i.Image)
            .Include(i => i.Weapon)
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
        
        return hero is null 
            ? Result.Failure<HeroResponse>(new Error("Hero.NotFound", "Herói não encontrado")) 
            : _mapper.Map<HeroResponse>(hero);
    }
}