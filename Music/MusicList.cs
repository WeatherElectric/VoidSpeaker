namespace WeatherElectric.VoidSpeaker.Music;

internal static class MusicList
{
    public static readonly List<MusicFile> Music = [];
    
    public static void CacheValues()
    {
        foreach (var file in Music)
        {
            file.CacheValues();
        }
    }
}