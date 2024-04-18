namespace WeatherElectric.VoidSpeaker.Music.Files;

internal class MusicFile
{
    public readonly string Path;
    public readonly string Name;
    public readonly AudioClip AudioClip;
    
    public Texture2D CachedArt;
    public string CachedTitle;
    public string CachedArtist;
    
    public MusicFile(string path, string name, AudioClip audioClip)
    {
        Path = path;
        Name = name;
        AudioClip = audioClip;
    }

    public void CacheValues()
    {
        CachedTitle = TagLibWrapper.GetTag(Path, TagLibWrapper.Tag.Title);
        CachedArtist = TagLibWrapper.GetTag(Path, TagLibWrapper.Tag.Artist);
        Texture2D albumArt = TagLibWrapper.GetCover(Path);
        if (albumArt == null) return;
        Texture2D resizedAlbumArt = albumArt.ProperResize(336, 336);
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