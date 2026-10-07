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
        CameraAnimator cameraAnimator =Camera.main.GetComponent<CameraAnimator>();
        startButton.onClick.AddListener(() =>
        {
            cameraAnimator.LeftTurn(() =>
            {
                UIManager.Instance.HidePanel<StartPanel>();
                Debug.Log("1");
            });
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
