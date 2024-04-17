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
}