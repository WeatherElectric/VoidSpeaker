using System.Collections.Generic;

namespace WeatherElectric.VoidSpeaker.Music;

internal static class MusicList
{
    public static readonly List<MusicFile> Music = new();
    
    public static void CacheValues()
    {
        foreach (MusicFile file in Music)
        {
            file.CacheValues();
        }
    }
}