using UnityEngine;
using UnityEngine.UI;
using SSWC.Common.UI;


namespace SSWC.Features.BeginScene.UI
{
    /// <summary>
    /// 开始面板
    /// </summary>
    public class StartPanel : BasePanel
    {
        //开始按钮
        public Button startButton;
        //设置按钮
        public Button settingButton;
        //关于按钮
        public Button aboutButton;
        //离开按钮
        public Button quitButton;

        /// <summary>
        /// 绑定各组件的回调
        /// </summary>
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
}
