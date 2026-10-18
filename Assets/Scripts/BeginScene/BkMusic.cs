using Unity.VisualScripting;
using UnityEngine;

public class BkMusic : MonoBehaviour
{
    private static BkMusic _instance;
    
    public static BkMusic Instance => _instance;
    
    public AudioSource audioSource;
    
    void Awake()
    {
        _instance = this;
        audioSource=this.AddComponent<AudioSource>();
        audioSource.clip = Resources.Load<AudioClip>("Music/BKMusic");
        SetIsOpen(DataManager.Instance.MusicData.IsOpenMusic);
        SetVolume(DataManager.Instance.MusicData.MusicVolume);
    }
    
    public void SetIsOpen(bool isOpen)
    {
        if (isOpen)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }
    
    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
