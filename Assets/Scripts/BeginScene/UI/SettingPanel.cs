using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : BasePanel
{
    public Slider sliderMusic;
    public Slider sliderSfx;
    public Toggle togMusic;
    public Toggle togSfx;
    public Button btnQuit;

    public override void Init()
    {
        MusicData musicData = DataManager.Instance.MusicData;
        sliderMusic.value=musicData.MusicVolume;
        sliderSfx.value=musicData.SfxVolume;
        togMusic.isOn=musicData.IsOpenMusic;
        togSfx.isOn=musicData.IsOpenSfx;
        
        sliderMusic.onValueChanged.AddListener((value) =>
        {
            musicData.MusicVolume = value;
            BkMusic.Instance.SetVolume(value);
        });
        
        sliderSfx.onValueChanged.AddListener((value) =>
        {
            musicData.SfxVolume = value;
        });
        
        togMusic.onValueChanged.AddListener((isOn) =>
        {
            musicData.IsOpenMusic = isOn;
            BkMusic.Instance.SetIsOpen(isOn);
        });
        
        togSfx.onValueChanged.AddListener((isOn) =>
        {
            musicData.IsOpenSfx = isOn;
        });
        
        btnQuit.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<SettingPanel>();
            DataManager.Instance.SaveMusicData();
        });
    }
}
