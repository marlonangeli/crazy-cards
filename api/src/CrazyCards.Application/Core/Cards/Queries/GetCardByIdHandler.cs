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

    public async Task<Result<CardResponse>> Handle(GetCardByIdQuery request, CancellationToken cancellationToken)
    {
        var cardQuery = _dbContext.Set<Card>()
            .AsNoTracking()
            .Include(i => i.Class)
            .Include(i => i.Habilities)
            .Include(i => i.Image)
            .Include(i => i.Skin);

        return await _mapper.ProjectTo<CardResponse>(cardQuery)
                   .FirstOrDefaultAsync(c => c.Id == request.Id,
                       cancellationToken: cancellationToken) ??
               (Result<CardResponse>)Result.Failure(new Error("Card.NotFound", "Carta não encontrada"));
    }
}