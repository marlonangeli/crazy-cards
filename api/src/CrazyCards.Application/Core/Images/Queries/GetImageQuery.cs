using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Images;
using CrazyCards.Domain.Primitives.Result;

namespace CrazyCards.Application.Core.Images.Queries;

public record GetImageQuery(Guid Id) : IQuery<Result<ImageResponse>>;