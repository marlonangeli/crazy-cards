using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Hability;
using CrazyCards.Domain.Enum;
using CrazyCards.Domain.Primitives.Result;

namespace CrazyCards.Application.Core.Hability.Commands;

public record CreateHabilityCommand(
    Guid CardId,
    CreateActionRequest? Action,
    HabilityType? Type) : ICommand<Result<HabilityResponse>>;