using Unity.VisualScripting;
using SSWC.Data;
using UnityEngine;

namespace SSWC.Common.Audio
{
    /// <summary>
    /// 背景音乐组件，用于播放背景音乐 
    /// </summary>
    public class BkMusic : MonoBehaviour
    {
        private static BkMusic _instance;
    
        public static BkMusic Instance => _instance;
    
        //音源组件
        public AudioSource audioSource;
    
        void Awake()
        {
            _instance = this;
            audioSource=this.AddComponent<AudioSource>();
            audioSource.clip = Resources.Load<AudioClip>("Music/BKMusic");
            SetIsOpen(DataManager.Instance.MusicData.IsOpenMusic);
            SetVolume(DataManager.Instance.MusicData.MusicVolume);
        }
    
        /// <summary>
        /// 设置背景音乐是否播放
        /// </summary>
        /// <param name="isOpen">背景音乐是否打开</param>
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
    
        /// <summary>
        /// 设置背景音乐音量
        /// </summary>
        /// <param name="volume">音量值</param>
        public void SetVolume(float volume)
        {
            audioSource.volume = volume;
        }
    }
}
