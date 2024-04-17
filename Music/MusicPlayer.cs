using BoneLib.Notifications;
using WeatherElectric.VoidSpeaker.Music.Helpers;

namespace WeatherElectric.VoidSpeaker.Music;

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
            var songName = TagLibWrapper.GetTag(musicFile.FilePath, TagLibWrapper.Tag.Title);
            var artistName = TagLibWrapper.GetTag(musicFile.FilePath, TagLibWrapper.Tag.Artist);
            Texture2D albumArt = TagLibWrapper.GetCover(musicFile.FilePath);
            
            Notification notif = new Notification()
            {
                Title = "Now Playing:",
                Message = $"{songName} by {artistName}",
                CustomIcon = albumArt,
                Type = NotificationType.CustomIcon,
                PopupLength = Preferences.NotificationDuration.Value,
                ShowTitleOnPopup = true
            };
            Notifier.Send(notif);
        }
        else
        {
            Notification notif = new Notification()
            {
                Title = "Now Playing:",
                Message = $"{musicFile.FileName}",
                Type = NotificationType.Information,
                PopupLength = Preferences.NotificationDuration.Value,
                ShowTitleOnPopup = true
            };
            Notifier.Send(notif);
        }
    }

    public void Resume()
    {
        _paused = false;
        _audioSource.UnPause();
    }

    public void Pause()
    {
        _paused = true;
        _audioSource.Pause();
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
        if (!_playingAtAll) return;
        _currentMusicIndex++;
        if (_currentMusicIndex >= MusicList.Music.Count)
        {
            _currentMusicIndex = 0;
        }
        Play();
    }

    public void Stop()
    {
        if (!_playingAtAll) return;
        _playingAtAll = false;
        _audioSource.Stop();
        _currentMusicIndex = 0;
    }

    private void OnDestroy()
    {
        Instance = null;
    }
    
    public MusicPlayer(IntPtr ptr) : base(ptr) { }
}