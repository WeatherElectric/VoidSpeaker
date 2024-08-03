﻿using BoneLib.Notifications;

namespace WeatherElectric.VoidSpeaker.Music.Behaviours;

[RegisterTypeInIl2Cpp]
internal class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer Instance { get; private set; }
    
    private AudioSource _audioSource;
    private int _currentMusicIndex;
    private bool _paused;
    private bool _playingAtAll;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.Setup();
    }

    private void Update()
    {
        if (_paused) return;
        if (!_playingAtAll) return;
        if (_audioSource.isPlaying) return;
        _currentMusicIndex++;
        if (_currentMusicIndex >= MusicList.Music.Count)
        {
            _currentMusicIndex = 0;
        }
        Play();
    }

    public void SetVolume(float volume)
    {
        _audioSource.volume = volume;
    }

    public void Play()
    {
        if (_playingAtAll) return;
        _playingAtAll = true;
        var musicFile = MusicList.Music[_currentMusicIndex];
        _audioSource.clip = musicFile.AudioClip;
        _audioSource.Play();

        if (Preferences.SendNotifications.Value) SendNotification(musicFile);
    }

    private static void SendNotification(MusicFile musicFile)
    {
        if (Preferences.UseTagLib.Value)
        {
            var isTitleCached = musicFile.CachedTitle != null;
            var isArtistCached = musicFile.CachedTitle != null;
            bool isAlbumArtCached = musicFile.CachedArt;
            
            if (!isTitleCached)
            {
                var title = TagLibWrapper.GetTag(musicFile.Path, TagLibWrapper.Tag.Title);
                if (title == null) return;
                musicFile.CachedTitle = title;
            }
            if (!isArtistCached)
            {
                var artist = TagLibWrapper.GetTag(musicFile.Path, TagLibWrapper.Tag.Artist);
                if (artist == null) return;
                musicFile.CachedArtist = artist;
            }
            if (!isAlbumArtCached)
            {
                var albumArt = TagLibWrapper.GetCover(musicFile.Path);
                if (!albumArt) return;
                var resizedAlbumArt = albumArt.ProperResize(336, 336);
                musicFile.CachedArt = resizedAlbumArt;
                Destroy(albumArt);
            }

            if (musicFile.CachedTitle == null || musicFile.CachedArtist == null || !musicFile.CachedArt)
            {
                var notification = new Notification
                {
                    Title = "Now Playing:",
                    Message = $"{musicFile.Name}",
                    Type = NotificationType.Information,
                    PopupLength = Preferences.NotificationDuration.Value,
                    ShowTitleOnPopup = true
                };
                notification.Send();
                return;
            }
            
            var notif = new Notification
            {
                Title = "Now Playing:",
                Message = $"{musicFile.CachedTitle} by {musicFile.CachedArtist}",
                CustomIcon = musicFile.CachedArt,
                Type = NotificationType.CustomIcon,
                PopupLength = Preferences.NotificationDuration.Value,
                ShowTitleOnPopup = true
            };
            notif.Send();
        }
        else
        {
            var notif = new Notification
            {
                Title = "Now Playing:",
                Message = $"{musicFile.Name}",
                Type = NotificationType.Information,
                PopupLength = Preferences.NotificationDuration.Value,
                ShowTitleOnPopup = true
            };
            notif.Send();
        }
    }

    public bool PauseUnpause()
    {
        if (_playingAtAll == false) return false;
        _paused = !_paused;
        if (_paused)
        {
            _audioSource.Pause();
        }
        else
        {
            _audioSource.UnPause();
        }
        return _paused;
    }
    
    public void Pause()
    {
        if (_playingAtAll == false) return;
        _paused = true;
        _audioSource.Pause();
    }
    
    public void Unpause()
    {
        if (_playingAtAll == false) return;
        _paused = false;
        _audioSource.UnPause();
    }

    public void Shuffle()
    {
        if (_playingAtAll)
        {
            Stop();
            MusicList.Music.Shuffle();
            Play();
        }
        else
        {
            MusicList.Music.Shuffle();
        }
    }

    public void Skip()
    {
        if (_playingAtAll == false) return;
        _currentMusicIndex++;
        if (_currentMusicIndex >= MusicList.Music.Count)
        {
            _currentMusicIndex = 0;
        }
        Stop(false);
        Play();
    }

    public void Stop(bool resetIndex = true)
    {
        if (!_playingAtAll) return;
        _playingAtAll = false;
        _audioSource.Stop();
        if (resetIndex) _currentMusicIndex = 0;
    }

#pragma warning disable CA1822
    private void OnDestroy()
    {
        Instance = null;
    }
#pragma warning restore CA1822
    
    // ReSharper disable once ConvertToPrimaryConstructor
    public MusicPlayer(IntPtr ptr) : base(ptr) { }
}