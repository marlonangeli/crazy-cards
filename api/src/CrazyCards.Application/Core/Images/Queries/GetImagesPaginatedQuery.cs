using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Common;
using CrazyCards.Application.Contracts.Images;

namespace CrazyCards.Application.Core.Images.Queries;

public record GetImagesPaginatedQuery(int Page, int PageSize) : IQuery<PagedList<ImageResponse>>;