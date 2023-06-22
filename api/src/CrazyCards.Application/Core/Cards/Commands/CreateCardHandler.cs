using AutoMapper;
using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Cards;
using CrazyCards.Application.Interfaces;
using CrazyCards.Domain.Entities.Card;
using CrazyCards.Domain.Primitives.Result;

namespace CrazyCards.Application.Core.Cards.Commands.CreateCard;

internal sealed class CreateCardHandler : ICommandHandler<CreateCardCommand, Result<CardResponse>>
{
    private readonly IDbContext _dbContext;
    private readonly IMapper _mapper;
    
    public CreateCardHandler(IDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Result<CardResponse>> Handle(CreateCardCommand request, CancellationToken cancellationToken)
    {
        var card = CardFactory.CreateCardFromRequest(request);

        var entity = await _dbContext.Set<Card>().AddAsync(card, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return Result.Success(_mapper.Map<CardResponse>(entity.Entity));
    }
}