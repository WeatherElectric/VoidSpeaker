namespace WeatherElectric.VoidSpeaker.Menu;

internal static class BoneMenu
{
    public static void Setup()
    {
        MenuCategory mainCat = MenuManager.CreateCategory("Weather Electric", "#6FBDFF");
        MenuCategory subCat = mainCat.CreateCategory("Void Speaker", Color.green);
        SubPanelElement settingsPanel = subCat.CreateSubPanel("Settings", Color.grey);
        
        subCat.CreateFunctionElement("Play", Color.green, () =>
        {
            MusicPlayer.Instance.Play();
        });
        
        subCat.CreateFunctionElement("Pause", Color.yellow, () =>
        {
            MusicPlayer.Instance.Pause();
        });
        
        subCat.CreateFunctionElement("Resume", Color.green, () =>
        {
            MusicPlayer.Instance.Resume();
        });
        
        subCat.CreateFunctionElement("Skip", Color.white, () =>
        {
            MusicPlayer.Instance.Skip();
        });
        
        subCat.CreateFunctionElement("Shuffle", Color.white, () =>
        {
            MusicPlayer.Instance.Shuffle();
        });
        
        subCat.CreateFunctionElement("Stop", Color.red, () =>
        {
            MusicPlayer.Instance.Stop();
        });
        
        #region Settings
        
        settingsPanel.CreateBoolPreference("Send Notifications", Color.white, Preferences.SendNotifications, Preferences.OwnCategory);
        settingsPanel.CreateBoolPreference("Use TagLib", Color.white, Preferences.UseTagLib, Preferences.OwnCategory);
        settingsPanel.CreateFloatPreference("Notification Duration", Color.white, 0.5f, 0.5f, 10f, Preferences.NotificationDuration, Preferences.OwnCategory);
        
        #endregion
    }
}