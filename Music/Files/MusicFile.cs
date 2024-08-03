namespace WeatherElectric.VoidSpeaker.Music.Files;

internal class MusicFile(string path, string name, AudioClip audioClip)
{
    public readonly string Path = path;
    public readonly string Name = name;
    public readonly AudioClip AudioClip = audioClip;
    
    public Texture2D CachedArt;
    public string CachedTitle;
    public string CachedArtist;

    public void CacheValues()
    {
        CachedTitle = TagLibWrapper.GetTag(Path, TagLibWrapper.Tag.Title);
        CachedArtist = TagLibWrapper.GetTag(Path, TagLibWrapper.Tag.Artist);
        var albumArt = TagLibWrapper.GetCover(Path);
        if (albumArt == null) return;
        var resizedAlbumArt = albumArt.ProperResize(336, 336);
        CachedArt = resizedAlbumArt;
        Object.Destroy(albumArt);
    }

    public void Dispose()
    {
        Object.Destroy(AudioClip);
        Object.Destroy(CachedArt);
        CachedArt = null;
        CachedTitle = null;
    }
}