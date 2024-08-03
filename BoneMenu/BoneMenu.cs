using BoneLib.BoneMenu;

namespace WeatherElectric.VoidSpeaker.BoneMenu;

internal static class BoneMenu
{
    private static FunctionElement _pauseElement;
    public static void Setup()
    {
        var mainCat = Page.Root.CreatePage("<color=#6FBDFF>Weather Electric</color>", Color.white);
        var subCat = mainCat.CreatePage("<color=#bdd9da>Void Speaker</color>", Color.white);
        
        subCat.CreateFloat("Volume", Color.white, Preferences.Volume.Value, 0.05f, 0f, 1f, f =>
        {
            MusicPlayer.Instance.SetVolume(f);
            Preferences.Volume.Value = f;
            Preferences.OwnCategory.SaveToFile(false);
        });
        
        subCat.CreateFunction("Play", Color.green, () =>
        {
            MusicPlayer.Instance.Play();
        });
        
        subCat.CreateFunction("Stop", Color.red, () =>
        {
            MusicPlayer.Instance.Stop();
            _pauseElement.ElementName = "Pause";
            _pauseElement.ElementColor = Color.yellow;
            MusicPlayer.Instance.Unpause();
        });
        
        _pauseElement = subCat.CreateFunction("Pause", Color.yellow, () =>
        {
            var paused = MusicPlayer.Instance.PauseUnpause();
            _pauseElement.ElementName = paused ? "Unpause" : "Pause";
            _pauseElement.ElementColor = paused ? Color.green : Color.yellow;
        });
        
        subCat.CreateFunction("Skip", Color.white, () =>
        {
            MusicPlayer.Instance.Skip();
        });
        
        subCat.CreateFunction("Shuffle", Color.white, () =>
        {
            MusicPlayer.Instance.Shuffle();
        });
        
        var settingsPanel = subCat.CreatePage("Settings", Color.gray);
        
        #region Settings
        
        settingsPanel.CreateBoolPreference("Send Notifications", Color.white, Preferences.SendNotifications, Preferences.OwnCategory);
        settingsPanel.CreateBoolPreference("Use TagLib", Color.white, Preferences.UseTagLib, Preferences.OwnCategory);
        settingsPanel.CreateFloatPreference("Notification Duration", Color.white, 0.5f, 0.5f, 10f, Preferences.NotificationDuration, Preferences.OwnCategory);
        settingsPanel.CreateFunction("Refresh Music", Color.white, () =>
        {
            Menu.DisplayDialog("Warning!", "This will freeze the game for a while, depending on how many songs you added!", null, RefreshMusic);
        });
        
        return;

        void RefreshMusic()
        {
            MusicLoader.RemoveMissingFiles();
            MusicLoader.LoadNewFiles();
        }

        #endregion
    }
}