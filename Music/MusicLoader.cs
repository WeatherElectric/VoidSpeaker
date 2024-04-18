using System.Collections.Generic;
using System.IO;
using System.Linq;

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

    public static void LoadNewFiles()
    {
#if DEBUG
        ModConsole.Warning("Checking for new files.");
#endif
        var filePaths = Directory.GetFiles(UserData.ModPath);
        List<MusicFile> musicFiles = new();
        foreach (MusicFile file in MusicList.Music)
        {
            foreach (var path in filePaths)
            {
                if (path == file.Path) continue;
#if DEBUG
                ModConsole.Warning($"File {file.Path} not found in music list, loading.");
#endif
                var audioClip = AudioImportLib.API.LoadAudioClip(path);
                var musicFile = new MusicFile(path, Path.GetFileNameWithoutExtension(path), audioClip);
                if (Preferences.UseTagLib.Value) musicFile.CacheValues();
                MusicPlayer.Instance.Stop();
                musicFiles.Add(musicFile);
            }
        }
        if (musicFiles.Count == 0) return;
        foreach (var file in musicFiles)
        {
            MusicList.Music.Add(file);
            ModConsole.Msg($"Loaded audio clip from file: {file.Path}", 1);
        }
    }
    
    public static void RemoveMissingFiles()
    {
#if DEBUG
        ModConsole.Warning("Checking for missing files.");
#endif
        List<MusicFile> filesToDelete = new();
        foreach (var file in MusicList.Music.Where(file => !File.Exists(file.Path)))
        {
#if DEBUG
            ModConsole.Warning($"File {file.Path} no longer exists, removing from music list.");
#endif
            filesToDelete.Add(file);
            file.Dispose();
        }
        if (filesToDelete.Count == 0) return;
        foreach (var file in filesToDelete)
        {
            MusicList.Music.Remove(file);
        }
    }
}