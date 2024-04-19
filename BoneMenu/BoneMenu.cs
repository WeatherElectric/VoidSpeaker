using BoneLib.BoneMenu;
using BoneLib.BoneMenu.Elements;

namespace WeatherElectric.VoidSpeaker.Menu;

internal static class BoneMenu
{
    private static FunctionElement _pauseElement;
    public static void Setup()
    {
        MenuCategory mainCat = MenuManager.CreateCategory("Weather Electric", "#6FBDFF");
        MenuCategory subCat = mainCat.CreateCategory("Void Speaker", "#bdd9da");
        SubPanelElement settingsPanel = subCat.CreateSubPanel("Settings", Color.gray);

        subCat.CreateFloatElement("Volume", Color.white, 1f, 0.1f, 0f, 1f, f =>
        {
            MusicPlayer.Instance.SetVolume(f);
        });
        
        subCat.CreateFunctionElement("Play", Color.green, () =>
        {
            MusicPlayer.Instance.Play();
        });
        
        subCat.CreateFunctionElement("Stop", Color.red, () =>
        {
            MusicPlayer.Instance.Stop();
            _pauseElement.SetName("Pause");
            _pauseElement.SetColor(Color.yellow);
            MusicPlayer.Instance.Unpause();
        });
        
        _pauseElement = subCat.CreateFunctionElement("Pause", Color.yellow, () =>
        {
            var paused = MusicPlayer.Instance.PauseUnpause();
            _pauseElement.SetName(paused ? "Unpause" : "Pause");
            _pauseElement.SetColor(paused ? Color.green : Color.yellow);
        });
        
        subCat.CreateFunctionElement("Skip", Color.white, () =>
        {
            MusicPlayer.Instance.Skip();
        });
        
        subCat.CreateFunctionElement("Shuffle", Color.white, () =>
        {
            MusicPlayer.Instance.Shuffle();
        });
        
        #region Settings
        
        settingsPanel.CreateBoolPreference("Send Notifications", Color.white, Preferences.SendNotifications, Preferences.OwnCategory);
        settingsPanel.CreateBoolPreference("Use TagLib", Color.white, Preferences.UseTagLib, Preferences.OwnCategory);
        settingsPanel.CreateFloatPreference("Notification Duration", Color.white, 0.5f, 0.5f, 10f, Preferences.NotificationDuration, Preferences.OwnCategory);
        settingsPanel.CreateFunctionElement("Refresh Music", Color.white, () =>
        {
            MusicLoader.RemoveMissingFiles();
            MusicLoader.LoadNewFiles();
        });

        #endregion
    }
}