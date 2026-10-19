using UnityEngine.UI;

/// <summary>
/// 设置面板控件
/// </summary>
public class SettingPanel : BasePanel
{
    //背景音乐滚动条
    public Slider sliderMusic;
    //音效滚动条
    public Slider sliderSfx;
    //背景音乐开关
    public Toggle togMusic;
    //音效开关
    public Toggle togSfx;
    //退出按钮
    public Button btnQuit;

    /// <summary>
    /// 绑定各组件的回调
    /// </summary>
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
