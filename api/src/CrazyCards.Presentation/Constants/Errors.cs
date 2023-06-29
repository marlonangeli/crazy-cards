using CrazyCards.Domain.Primitives;

namespace CrazyCards.Presentation.Constants;

internal static class Errors
{
    internal static Error UnProcessableRequest => new(
        "API.UnProcessableRequest",
        "Oops! Parece que o nosso servidor ficou um pouco confuso com a sua requisição.");

    internal static Error ServerError => new(
        "API.ServerError",
        "Eita! Aconteceu um erro desconhecido no nosso servidor.");
}