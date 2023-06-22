using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Classes;
using CrazyCards.Domain.Primitives.Result;

namespace CrazyCards.Application.Core.Classes.Commands;

public record CreateClassCommand(
    string Name,
    string Description,
    Guid ImageId,
    Guid SkinId) : ICommand<Result<ClassResponse>>;