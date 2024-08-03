using MelonLoader.Utils;

namespace WeatherElectric.VoidSpeaker.Melon;

internal static class UserData
{
    private static readonly string WeatherElectricPath = Path.Combine(MelonEnvironment.UserDataDirectory, "Weather Electric");
    public static readonly string ModPath = Path.Combine(MelonEnvironment.UserDataDirectory, "Weather Electric/Void Speaker");

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
        
        if (Directory.GetFiles(ModPath).Length == 0)
        {
            var bytes = HelperMethods.GetResourceBytes(Main.ModAsm, "WeatherElectric.VoidSpeaker.Resources.Default.mp3");
            File.WriteAllBytes(Path.Combine(ModPath, "Don't Fence Me In.mp3"), bytes);
        }
    }
}