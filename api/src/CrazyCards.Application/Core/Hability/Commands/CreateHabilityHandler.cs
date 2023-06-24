using AutoMapper;
using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Hability;
using CrazyCards.Application.Interfaces;
using CrazyCards.Domain.Entities.Card.Hability;
using CrazyCards.Domain.Primitives.Result;

namespace CrazyCards.Application.Core.Hability.Commands;

internal sealed class CreateHabilityHandler : ICommandHandler<CreateHabilityCommand, Result<HabilityResponse>>
{
    private readonly IDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateHabilityHandler(IDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Result<HabilityResponse>> Handle(CreateHabilityCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.Card.Hability.Hability hability = request.Type switch
        {
            { Value: 1 } => _mapper.Map<TauntHability>(request),
            { Value: 11 } => _mapper.Map<BattlecryHability>(request),
            { Value: 12 } => _mapper.Map<LastBreathHability>(request),
            _ => throw new ArgumentOutOfRangeException(nameof(request.Type), request.Type, "Tipo de habilidade inválida")
        };

        var entity = await _dbContext.Set<Domain.Entities.Card.Hability.Hability>().AddAsync(hability, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return Result.Success(_mapper.Map<HabilityResponse>(entity.Entity));
    }
}