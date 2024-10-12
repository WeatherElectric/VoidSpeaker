using System.Reflection;
using FieldInjector;

namespace WeatherElectric.VoidSpeaker;

public class Main : MelonMod
{
    internal const string Name = "VoidSpeaker";
    internal const string Description = "A music player for BONELAB";
    internal const string Author = "FragileDeviations";
    internal const string Company = "Weather Electric";
    internal const string Version = "2.2.0";
    internal const string DownloadLink = "https://thunderstore.io/c/bonelab/p/SoulWithMae/VoidSpeaker/";

    private static bool _hasRanSetup;
    internal static Assembly ModAsm => Assembly.GetExecutingAssembly();

    public override void OnInitializeMelon()
    {
        ModConsole.Setup(LoggerInstance);
        Preferences.Setup();
        BoneMenu.BoneMenu.Setup();
        UserData.Setup();
        MusicLoader.Load();
        
        SerialisationHandler.Inject<MetadataListener>();
        
        Hooking.OnLevelLoaded += OnLevelLoad;
        
#if DEBUG
        ModConsole.Warning("This is a debug build! It may be unstable!");
#endif
    }

    private static void OnLevelLoad(LevelInfo levelInfo)
    {
        if (_hasRanSetup) return;
        _hasRanSetup = true;
        var gameObj = new GameObject("MusicPlayer");
        gameObj.AddComponent<MusicPlayer>();
        
        // if i do this earlier, it does nothing, prob since unityengine hasnt loaded anything yet lol
        if (Preferences.UseTagLib.Value) MusicList.CacheValues();
    }
}