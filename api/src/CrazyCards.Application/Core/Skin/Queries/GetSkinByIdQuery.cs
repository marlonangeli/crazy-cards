using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Skin;
using CrazyCards.Domain.Primitives.Result;

namespace CrazyCards.Application.Core.Skin.Commands.Queries;

public record GetSkinByIdQuery(Guid Id) : IQuery<Result<SkinResponse>>;