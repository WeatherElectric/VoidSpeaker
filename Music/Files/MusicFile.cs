namespace WeatherElectric.VoidSpeaker.Music.Files;

internal class MusicFile
{
    public readonly string FilePath;
    public readonly string FileName;
    public readonly AudioClip AudioClip;
    public Texture2D CachedArt;
    public string CachedTitle;
    public string CachedArtist;
    
    public MusicFile(string filePath, string fileName, AudioClip audioClip)
    {
        FilePath = filePath;
        FileName = fileName;
        AudioClip = audioClip;
    }
}