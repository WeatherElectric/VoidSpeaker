using System.IO;
using Object = UnityEngine.Object;

namespace WeatherElectric.VoidSpeaker.Music;

internal static class MusicLoader
{
    public static void Load()
    {
        if (Directory.GetFiles(UserData.ModPath).Length == 0)
        {
            ModConsole.Error("No music files found in the mod folder!");
            return;
        }
        
        var filePaths = Directory.GetFiles(UserData.ModPath);
        
        foreach (var filePath in filePaths)
        {
            var fileName = Path.GetFileNameWithoutExtension(filePath);
            
            var audioClip = AudioImportLib.API.LoadAudioClip(filePath);
            if (audioClip == null)
            {
                ModConsole.Error($"Failed to load audio clip from file: {filePath}");
                continue;
            }
            
            var musicFile = new MusicFile(filePath, fileName, audioClip);
            MusicList.Music.Add(musicFile);
            ModConsole.Msg($"Loaded audio clip from file: {filePath}", 1);
        }
    }
}