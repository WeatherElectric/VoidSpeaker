using System.Collections.Generic;
using BoneLib.Notifications;
using Random = System.Random;

namespace WeatherElectric.VoidSpeaker.Music.Helpers;

internal static class ExtensionMethods
{
    public static void Shuffle<T>(this List<T> list)
    {
        Random rng = new Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }
    }

    // keep the musicplayer's awake a bit cleaner and just do it from an extension method
    public static void Setup(this AudioSource audioSource)
    {
        audioSource.spatialBlend = 0;
        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.outputAudioMixerGroup = Audio.MusicMixer;
    }

    public static void Send(this Notification notif)
    {
        Notifier.Send(notif);
    }
}