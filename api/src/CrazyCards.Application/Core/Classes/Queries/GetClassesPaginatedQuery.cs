using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Classes;
using CrazyCards.Application.Contracts.Common;

namespace CrazyCards.Application.Core.Classes.Queries;

public record GetClassesPaginatedQuery(
    int Page,
    int PageSize,
    string? Name) : IQuery<PagedList<ClassResponse>>;