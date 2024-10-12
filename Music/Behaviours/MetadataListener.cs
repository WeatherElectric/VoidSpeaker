using Il2CppUltEvents;
// ReSharper disable UnassignedField.Global

namespace WeatherElectric.VoidSpeaker.Music.Behaviours;

public class MetadataListener : MonoBehaviour
{
    internal static readonly List<MetadataListener> Listeners = [];
    
    public RenderTexture renderTexture;
    
    public UltEvent<Texture2D> RecieveArt;
    public UltEvent<string> RecieveTitle;
    public UltEvent<string> RecieveArtist;
    public UltEvent<string> RecieveFilename;
    
    private void Awake()
    {
        Listeners.Add(this);
    }
    
    private void UpdateRenderTexture(Texture2D art)
    {
        if (!enabled) return;
        Graphics.Blit(art, renderTexture);
    }
    
    public void SetArtOfMaterial(Material material, Texture2D art)
    {
        if (!enabled) return;
        material.mainTexture = art;
    }
    
    internal void InvokeTitleChanged(string title)
    {
        if (!enabled) return;
        RecieveTitle.Invoke(title);
    }
    
    internal void InvokeArtistChanged(string artist)
    {
        if (!enabled) return;
        RecieveArtist.Invoke(artist);
    }
    
    internal void InvokeArtChanged(Texture2D art)
    {
        if (!enabled) return;
        UpdateRenderTexture(art);
        RecieveArt.Invoke(art);
    }
    
    internal void InvokeFilenameChanged(string filename)
    {
        if (!enabled) return;
        RecieveFilename.Invoke(filename);
    }
    
    private void OnDestroy()
    {
        Listeners.Remove(this);
    }
    
    // ReSharper disable once ConvertToPrimaryConstructor
    public MetadataListener(IntPtr ptr) : base(ptr) { }
}