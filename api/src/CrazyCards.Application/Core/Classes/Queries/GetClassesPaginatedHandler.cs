using AutoMapper;
using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Classes;
using CrazyCards.Application.Contracts.Common;
using CrazyCards.Application.Interfaces;
using CrazyCards.Domain.Entities.Card;
using Microsoft.EntityFrameworkCore;

namespace CrazyCards.Application.Core.Classes.Queries;

internal sealed class GetClassesPaginatedHandler : IQueryHandler<GetClassesPaginatedQuery, PagedList<ClassResponse>>
{
    private readonly IDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetClassesPaginatedHandler(IDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<PagedList<ClassResponse>> Handle(GetClassesPaginatedQuery request, CancellationToken cancellationToken)
    {
        var classesQuery = _dbContext.Set<Class>()
            .AsNoTracking()
            .IgnoreAutoIncludes()
            .Include(i => i.Skin)
            .Include(i => i.Image)
            .AsQueryable();
        
        if (!string.IsNullOrWhiteSpace(request.Name))
            classesQuery = classesQuery.Where(w => w.Name.Contains(request.Name));
        
        var classes = await classesQuery
            .OrderBy(x => x.Name)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);
        
        var count = await classesQuery.CountAsync(cancellationToken);
        
        return new PagedList<ClassResponse>(
            _mapper.Map<List<ClassResponse>>(classes),
            count,
            request.Page,
            request.PageSize);
    }
}