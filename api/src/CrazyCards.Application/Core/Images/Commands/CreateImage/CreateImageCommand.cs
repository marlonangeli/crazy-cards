using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Images;
using CrazyCards.Domain.Primitives.Result;

namespace CrazyCards.Application.Core.Images.Commands.CreateImage;

public record CreateImageCommand(Stream Stream, string MimeType)
    : ICommand<Result<ImageResponse>>;