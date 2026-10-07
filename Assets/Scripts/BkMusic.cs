using Unity.VisualScripting;
using UnityEngine;

public class BkMusic : MonoBehaviour
{
    private static BkMusic _instance;
    
    public static BkMusic Instance => _instance;
    
    public AudioSource _audioSource;
    
    void Awake()
    {
        _instance = this;
        _audioSource=this.AddComponent<AudioSource>();
        _audioSource.clip = Resources.Load<AudioClip>("Music/BKMusic");
        SetIsOpen(DataManager.Instance._musicData.IsOpenMusic);
        SetVolume(DataManager.Instance._musicData.MusicVolume);
    }
    
    public void SetIsOpen(bool isOpen)
    {
        if (isOpen)
        {
            _audioSource.Play();
        }
        else
        {
            _audioSource.Stop();
        }
    }
    
    public void SetVolume(float volume)
    {
        _audioSource.volume = volume;
    }
}
