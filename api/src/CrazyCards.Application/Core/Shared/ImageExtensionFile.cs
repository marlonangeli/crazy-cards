namespace CrazyCards.Application.Core.Shared;

public static class ImageExtensionFile
{
    public static string GetExtensionFile(this string extensionFile)
        => extensionFile switch
        {
            "image/svg+xml" => ".svg",
            "image/jpeg" => ".jpg",
            "image/png" => ".png",
            _ => throw new ArgumentOutOfRangeException(nameof(extensionFile), extensionFile,
                "Extension file not supported")
        };
}