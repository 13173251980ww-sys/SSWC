using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using SSWC.Common.UI;
using SSWC.Data;
using SSWC.Data.Config;
using SSWC.Features.GameScene;
using SSWC.Features.GameScene.UI;

namespace SSWC.Features.BeginScene.UI
{
    /// <summary>
    /// 场景选择面板
    /// </summary>
    public class SceneSelectPanel : BasePanel
    {
        //场景名字
        public TextMeshProUGUI nameText;

        //场景描述
        public TextMeshProUGUI descriptionText;

        //场景图片
        public Image sceneImg;

        //左切换键
        public Button btnLeft;

        //右切换键
        public Button btnRight;

        //返回键
        public Button btnBack;

        //开始键
        public Button btnStart;

        //当前选择场景的ID
        public int nowSceneId;

        //当前选择场景的数据
        public SceneData NowSceneData;

        /// <summary>
        /// 绑定各组件的回调
        /// </summary>
        public override void Init()
        {
            btnLeft.onClick.AddListener(() =>
            {
                nowSceneId--;
                if (nowSceneId < 0)
                    nowSceneId = DataManager.Instance.SceneDatas.Count - 1;
                //更新场景
                ChangeScene();
            });

            btnRight.onClick.AddListener(() =>
            {
                nowSceneId++;
                if (nowSceneId > DataManager.Instance.SceneDatas.Count - 1)
                    nowSceneId = 0;
                //更新场景
                ChangeScene();
            });

            btnBack.onClick.AddListener(() =>
            {
                //隐藏当前面板，显示选择角色面板
                UIManager.Instance.HidePanel<SceneSelectPanel>();
                UIManager.Instance.ShowPanel<SelectPanel>();
            });

            btnStart.onClick.AddListener(() =>
            {
                //隐藏当前面板，切换到对应游戏场景
                UIManager.Instance.HidePanel<SceneSelectPanel>();
                AsyncOperation ao = SceneManager.LoadSceneAsync(NowSceneData.sceneName);
                if (ao == null)
                    return;

                ao.completed += (asyncOperation) =>
                {
                    UIManager.Instance.ShowPanel<GamePanel>();
                    GameLevelManager.Instance.InitPlayerInfo();
                };
            });

            ChangeScene();
        }

        /// <summary>
        ///根据当前ID，更新UI，显示不同场景
        /// </summary>
        public void ChangeScene()
        {
            NowSceneData = DataManager.Instance.SceneDatas[nowSceneId];
            nameText.text = NowSceneData.name;
            descriptionText.text = NowSceneData.tips;
            sceneImg.sprite = Resources.Load<Sprite>(NowSceneData.imgRes);
        }
    }
}
