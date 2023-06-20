using AutoMapper;
using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Classes;
using CrazyCards.Application.Interfaces;
using CrazyCards.Domain.Entities.Card;
using CrazyCards.Domain.Primitives.Result;

namespace CrazyCards.Application.Core.Classes.Commands.CreateClass;

internal sealed class CreateClassHandler : ICommandHandler<CreateClassCommand, Result<ClassResponse>>
{
    private readonly IDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateClassHandler(IDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Result<ClassResponse>> Handle(CreateClassCommand request, CancellationToken cancellationToken)
    {
        var @class = _mapper.Map<Class>(request);
        
        _dbContext.Insert(@class);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ClassResponse>(@class);
    }
}