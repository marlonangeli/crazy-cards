using AutoMapper;
using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Hability;
using CrazyCards.Application.Interfaces;
using CrazyCards.Domain.Primitives;
using CrazyCards.Domain.Primitives.Result;
using Microsoft.EntityFrameworkCore;

namespace CrazyCards.Application.Core.Hability.Queries;

internal sealed class GetHabilityByIdHandler : IQueryHandler<GetHabilityByIdQuery, Result<HabilityResponse>>
{
    private readonly IDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetHabilityByIdHandler(IDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Result<HabilityResponse>> Handle(GetHabilityByIdQuery request, CancellationToken cancellationToken)
    {
        var hability = await _dbContext.Set<Domain.Entities.Card.Hability.Hability>()
            .AsNoTracking()
            .IgnoreAutoIncludes()
            .Include(i => i.Card)
            .Include(x => x.Action)
            .ThenInclude(i => i.InvokeCard)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (hability is null)
        {
            return Result.Failure<HabilityResponse>(new Error(
                "Hability.NotFound",
                "Habilidade não encontrada"));
        }

        return Result.Success(_mapper.Map<HabilityResponse>(hability));
    }
}