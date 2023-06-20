using CrazyCards.Application.Abstractions;
using CrazyCards.Application.Contracts.Skin;
using CrazyCards.Domain.Primitives.Result;

namespace CrazyCards.Application.Core.Skin.Commands.CreateSkin;

public record CreateSkinCommand(string Name, Stream Stream, string MimeType) : ICommand<Result<SkinResponse>>;