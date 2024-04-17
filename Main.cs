namespace WeatherElectric.VoidSpeaker;

public class Main : MelonMod
{
    internal const string Name = "VoidSpeaker";
    internal const string Description = "A music player for BONELAB";
    internal const string Author = "SoulWithMae";
    internal const string Company = "Weather Electric";
    internal const string Version = "0.0.1";
    internal const string DownloadLink = null;

    private static bool _hasRanSetup;

    public override void OnInitializeMelon()
    {
        ModConsole.Setup(LoggerInstance);
        Preferences.Setup();
        BoneMenu.Setup();
        UserData.Setup();
        MusicLoader.Load();
        Hooking.OnLevelInitialized += OnLevelLoad;
    }

    private static void OnLevelLoad(LevelInfo levelInfo)
    {
        if (_hasRanSetup) return;
        _hasRanSetup = true;
        var gameObj = new GameObject("MusicPlayer");
        gameObj.AddComponent<MusicPlayer>();
    }
}