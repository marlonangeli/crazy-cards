using AutoMapper;
using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Classes;
using CrazyCards.Application.Interfaces;
using CrazyCards.Domain.Entities.Card;
using CrazyCards.Domain.Primitives;
using CrazyCards.Domain.Primitives.Result;
using Microsoft.EntityFrameworkCore;

namespace CrazyCards.Application.Core.Classes.Queries;

internal sealed class GetClassByIdQueryHandler : IQueryHandler<GetClassByIdQuery, Result<ClassResponse>>
{
    private readonly IDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetClassByIdQueryHandler(IDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Result<ClassResponse>> Handle(GetClassByIdQuery request, CancellationToken cancellationToken)
    {
        var query = _dbContext.Set<Class>()
            .AsNoTracking()
            .Include(i => i.Cards)
            .Include(i => i.Skin)
            .Include(i => i.Image);

        return await _mapper.ProjectTo<ClassResponse>(query)
                   .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken) ??
               (Result<ClassResponse>)Result.Failure(new Error("Class.NotFound", "Classe não encontrada"));
    }
}