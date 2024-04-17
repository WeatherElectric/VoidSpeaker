using System.IO;

namespace WeatherElectric.VoidSpeaker.Music.Helpers;

internal static class TagLibWrapper
{
    public enum Tag
    {
        Title,
        Artist,
        Album,
        AlbumArtist,
        Genre,
        Year,
        Track,
        Disc,
        Composer,
        Conductor
    }

    private static string _tag;
    
    public static string GetTag(string filepath, Tag tag)
    {
        var tagLibFile = TagLib.File.Create(filepath);
        _tag = tag switch
        {
            Tag.Title => tagLibFile.Tag.Title,
            Tag.Artist => tagLibFile.Tag.FirstPerformer,
            Tag.Album => tagLibFile.Tag.Album,
            Tag.AlbumArtist => tagLibFile.Tag.FirstAlbumArtist,
            Tag.Genre => tagLibFile.Tag.FirstGenre,
            Tag.Year => tagLibFile.Tag.Year.ToString(),
            Tag.Track => tagLibFile.Tag.Track.ToString(),
            Tag.Disc => tagLibFile.Tag.Disc.ToString(),
            Tag.Composer => tagLibFile.Tag.FirstComposer,
            Tag.Conductor => tagLibFile.Tag.Conductor,
            _ => null
        };
        if (_tag == null)
        {
            ModConsole.Error($"{filepath}'s {tag.ToString()} field is null!");
            return null;
        }
        return _tag;
    }
    
    public static Texture2D GetCover(string filepath)
    {
        var tagLibFile = TagLib.File.Create(filepath);
        var picture = tagLibFile.Tag.Pictures[0];
        if (picture == null)
        {
            ModConsole.Error($"{filepath}'s album art field is null!");
            return null;
        }
        var texture = new Texture2D(2, 2);
        // ReSharper disable once InvokeAsExtensionMethod, unhollowed unity extensions don't work well, have to call them directly
        ImageConversion.LoadImage(texture, picture.Data.Data, false);
        return texture;
    }
    
    public static string GetFilename(string filepath)
    {
        var title = Path.GetFileName(filepath);
        return title;
    }
}