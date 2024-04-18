using BoneLib.Notifications;

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
        if (!_audioSource.isPlaying)
        {
            _currentMusicIndex++;
            if (_currentMusicIndex >= MusicList.Music.Count)
            {
                _currentMusicIndex = 0;
            }
            Play();
        }
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
            bool isTitleCached = musicFile.CachedTitle != null;
            bool isArtistCached = musicFile.CachedTitle != null;
            bool isAlbumArtCached = musicFile.CachedArt != null;
            
            if (!isTitleCached)
            {
                musicFile.CachedTitle = TagLibWrapper.GetTag(musicFile.Path, TagLibWrapper.Tag.Title);
            }
            if (!isArtistCached)
            {
                musicFile.CachedArtist = TagLibWrapper.GetTag(musicFile.Path, TagLibWrapper.Tag.Artist);
            }
            if (!isAlbumArtCached)
            {
                Texture2D albumArt = TagLibWrapper.GetCover(musicFile.Path);
                if (albumArt == null) return;
                Texture2D resizedAlbumArt = albumArt.ProperResize(336, 336);
                musicFile.CachedArt = resizedAlbumArt;
                Destroy(albumArt);
            }
            
            Notification notif = new Notification
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
            Notification notif = new Notification
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

    private void OnDestroy()
    {
        Instance = null;
    }
    
    public MusicPlayer(IntPtr ptr) : base(ptr) { }
}