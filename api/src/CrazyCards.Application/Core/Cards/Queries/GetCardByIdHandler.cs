using AutoMapper;
using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Cards;
using CrazyCards.Application.Interfaces;
using CrazyCards.Domain.Entities.Card;
using CrazyCards.Domain.Primitives;
using CrazyCards.Domain.Primitives.Result;
using Microsoft.EntityFrameworkCore;

namespace CrazyCards.Application.Core.Cards.Queries;

internal sealed class GetCardByIdHandler : IQueryHandler<GetCardByIdQuery, Result<CardResponse>>
{
    private readonly IDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetCardByIdHandler(IDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Result<CardResponse>> Handle(GetCardByIdQuery request, CancellationToken cancellationToken)
    {
        var cardResponse = await _dbContext.Set<Card>()
            .AsNoTracking()
            .IgnoreAutoIncludes()
            .Include(i => i.Class)
            .Include(i => i.Habilities)
            .Include(i => i.Image)
            .Include(i => i.Skin)
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        return _mapper.Map<CardResponse>(cardResponse) ??
               (Result<CardResponse>)Result.Failure(
                   new Error("Card.NotFound", "Carta não encontrada"));
    }
}