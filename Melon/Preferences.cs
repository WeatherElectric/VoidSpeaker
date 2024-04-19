// ReSharper disable MemberCanBePrivate.Global, these categories may be used outside of this namespace to create bonemenu options.

namespace WeatherElectric.VoidSpeaker.Melon;

internal static class Preferences
{
    public static readonly MelonPreferences_Category GlobalCategory = MelonPreferences.CreateCategory("Global");
    public static readonly MelonPreferences_Category OwnCategory = MelonPreferences.CreateCategory("VoidSpeaker");

    public static MelonPreferences_Entry<int> LoggingMode { get; set; }
    
    public static MelonPreferences_Entry<float> Volume { get; set; }
    public static MelonPreferences_Entry<bool> SendNotifications { get; set; }
    public static MelonPreferences_Entry<bool> UseTagLib { get; set; }
    public static MelonPreferences_Entry<float> NotificationDuration { get; set; }

    public static void Setup()
    {
        LoggingMode = GlobalCategory.GetEntry<int>("LoggingMode") ?? GlobalCategory.CreateEntry("LoggingMode", 0,
            "Logging Mode", "The level of logging to use. 0 = Important Only, 1 = All");
        GlobalCategory.SetFilePath(MelonUtils.UserDataDirectory + "/WeatherElectric.cfg");
        GlobalCategory.SaveToFile(false);
        Volume = OwnCategory.CreateEntry("Volume", 1f, "Volume", "The volume the music plays at.");
        SendNotifications = OwnCategory.CreateEntry("SendNotifications", true,
            "Send Notifications", "Send notifications when a new song starts playing.");
        UseTagLib = OwnCategory.CreateEntry("UseTagLib", !HelperMethods.IsAndroid(), "Use TagLib", "Use TagLib to get metadata from music files. If disabled, the mod will use the file name.");
        NotificationDuration = OwnCategory.CreateEntry("NotificationDuration", 2f, "Notification Duration",
            "The duration of the notification popup in seconds.");
        OwnCategory.SetFilePath(MelonUtils.UserDataDirectory + "/WeatherElectric.cfg");
        OwnCategory.SaveToFile(false);
        ModConsole.Msg("Finished preferences setup for VoidSpeaker", 1);
    }
}