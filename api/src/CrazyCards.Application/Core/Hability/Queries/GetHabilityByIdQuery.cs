using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Hability;
using CrazyCards.Domain.Primitives.Result;

namespace CrazyCards.Application.Core.Hability.Queries;

public record GetHabilityByIdQuery(Guid Id) : IQuery<Result<HabilityResponse>>;