using CrazyCards.Domain.Enum;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CrazyCards.Domain.Extension;

public static class JsonConverterSetup
{
    public static List<JsonConverter> GetConverters() =>
        new()
        {
            new StringEnumConverter(),
            new EnumerationConverter<CardType>(),
            new EnumerationConverter<Rarity>(),
            new EnumerationConverter<HabilityType>()
        };
}