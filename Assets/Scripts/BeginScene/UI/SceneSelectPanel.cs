using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSelectPanel : BasePanel
{
   public TextMeshProUGUI nameText;
   public TextMeshProUGUI descriptionText;
   public Image sceneImg;
   public Button btnLeft;
   public Button btnRight;
   public Button btnBack;
   public Button btnStart;
   
   public int nowSceneId;
   public SceneData NowSceneData;
   
   public override void Init()
   {
      btnLeft.onClick.AddListener(() =>
      {
         nowSceneId--;
         if(nowSceneId < 0)
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
         if(ao == null)
            return;
         
         ao.completed += (asyncOperation) =>
         {
            UIManager.Instance.ShowPanel<GamePanel>();
            GameLevelManager.Instance.InitPlayerInfo();
         };
      });
      
      ChangeScene();
   }
   
   public void ChangeScene()
   {
      NowSceneData = DataManager.Instance.SceneDatas[nowSceneId];
      nameText.text = NowSceneData.name;
      descriptionText.text = NowSceneData.tips;
      sceneImg.sprite = Resources.Load<Sprite>(NowSceneData.imgRes);
   }
}
