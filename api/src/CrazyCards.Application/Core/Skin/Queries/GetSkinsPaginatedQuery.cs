using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Common;
using CrazyCards.Application.Contracts.Skin;

namespace CrazyCards.Application.Core.Skin.Queries;

public record GetSkinsPaginatedQuery(int Page, int PageSize) : IQuery<PagedList<SkinResponse>>;