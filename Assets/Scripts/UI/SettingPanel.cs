using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : BasePanel
{
    public Slider sliderMusic;
    public Slider sliderSFX;
    public Toggle togMusic;
    public Toggle togSFX;
    public Button btnQuit;

    public override void Init()
    {
        MusicData musicData = DataManager.Instance._musicData;
        sliderMusic.value=musicData.MusicVolume;
        sliderSFX.value=musicData.SfxVolume;
        togMusic.isOn=musicData.IsOpenMusic;
        togSFX.isOn=musicData.IsOpenSfx;
        
        sliderMusic.onValueChanged.AddListener((value) =>
        {
            Debug.Log("当前音乐音量"+value);
            musicData.MusicVolume = value;
            BkMusic.Instance.SetVolume(value);
        });
        
        sliderSFX.onValueChanged.AddListener((value) =>
        {
            Debug.Log("当前音效音量"+value);
            musicData.SfxVolume = value;
        });
        
        togMusic.onValueChanged.AddListener((isOn) =>
        {
            Debug.Log("当前音乐开关状态为"+isOn);
            musicData.IsOpenMusic = isOn;
            BkMusic.Instance.SetIsOpen(isOn);
        });
        
        togSFX.onValueChanged.AddListener((isOn) =>
        {
            Debug.Log("当前音效开关状态为"+isOn);
            musicData.IsOpenSfx = isOn;
        });
        
        btnQuit.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<SettingPanel>();
            DataManager.Instance.SaveMusicData();
        });
    }
}
