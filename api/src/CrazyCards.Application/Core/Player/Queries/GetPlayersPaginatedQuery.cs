using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Common;
using CrazyCards.Application.Contracts.Players;

namespace CrazyCards.Application.Core.Player.Queries;

public record GetPlayersPaginatedQuery
    (int Page, int PageSize, string? Username, string? Email) : IQuery<PagedList<PlayerResponse>>;