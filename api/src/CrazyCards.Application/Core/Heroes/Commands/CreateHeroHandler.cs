using AutoMapper;
using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Heroes;
using CrazyCards.Application.Interfaces;
using CrazyCards.Domain.Entities.Card;
using CrazyCards.Domain.Primitives.Result;

namespace CrazyCards.Application.Core.Heroes.Commands;

internal sealed class CreateHeroHandler : ICommandHandler<CreateHeroCommand, Result<HeroResponse>>
{
    private readonly IDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateHeroHandler(IDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Result<HeroResponse>> Handle(CreateHeroCommand request, CancellationToken cancellationToken)
    {
        var hero = _mapper.Map<Hero>(request);
        
        var entity = await _dbContext.Set<Hero>().AddAsync(hero, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return Result.Success(_mapper.Map<HeroResponse>(entity.Entity));
    }
}