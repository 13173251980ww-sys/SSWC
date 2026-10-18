using UnityEngine;
using UnityEngine.UI;

public class StartPanel : BasePanel
{
    public Button startButton;
    public Button settingButton;
    public Button aboutButton;
    public Button quitButton;

    public override void Init()
    {
        startButton.onClick.AddListener(() =>
        {
            Camera.main.GetComponent<CameraAnimator>().LeftTurn(() =>
            {
                UIManager.Instance.ShowPanel<SelectPanel>();
            });
            
            UIManager.Instance.HidePanel<StartPanel>();
        });
        
        settingButton.onClick.AddListener(() =>
        {
            UIManager.Instance.ShowPanel<SettingPanel>();
        });
        
        aboutButton.onClick.AddListener(() =>
        {
            //显示关于面板
        });
        
        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
