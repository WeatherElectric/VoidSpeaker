using System.IO;

namespace WeatherElectric.VoidSpeaker.Melon;

internal static class UserData
{
    private static readonly string WeatherElectricPath = Path.Combine(MelonUtils.UserDataDirectory, "Weather Electric");
    public static readonly string ModPath = Path.Combine(MelonUtils.UserDataDirectory, "Weather Electric/Void Speaker");

    public static void Setup()
    {
        if (!Directory.Exists(WeatherElectricPath))
        {
            Directory.CreateDirectory(WeatherElectricPath);
        }

        if (!Directory.Exists(ModPath))
        {
            Directory.CreateDirectory(ModPath);
        }
    }
}