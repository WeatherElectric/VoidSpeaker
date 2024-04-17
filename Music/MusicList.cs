using System.Collections.Generic;

namespace WeatherElectric.VoidSpeaker.Music;

internal static class MusicList
{
    public static readonly List<MusicFile> Music = new();
    
    public static void CacheValues()
    {
        foreach (MusicFile file in Music)
        {
            file.CachedTitle = TagLibWrapper.GetTag(file.Path, TagLibWrapper.Tag.Title);
            file.CachedArtist = TagLibWrapper.GetTag(file.Path, TagLibWrapper.Tag.Artist);
            Texture2D albumArt = TagLibWrapper.GetCover(file.Path);
            if (albumArt == null) return;
            Texture2D resizedAlbumArt = albumArt.ProperResize(336, 336);
            file.CachedArt = resizedAlbumArt;
            Object.Destroy(albumArt);
        }
    }
}