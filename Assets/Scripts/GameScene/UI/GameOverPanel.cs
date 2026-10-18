using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanel : BasePanel
{
    public TextMeshProUGUI tmpStatus;
    public TextMeshProUGUI tmpContent;
    public TextMeshProUGUI tmpMoney;
    
    public Button btnSure;
    
    public override void Init()
    {
        btnSure.onClick.AddListener(() =>
        {
            //隐藏面板
            UIManager.Instance.HidePanel<GameOverPanel>();
            UIManager.Instance.HidePanel<GamePanel>();
            
            //清空关卡数据
            GameLevelManager.Instance.ClearInfo();
            
            //切换场景
            SceneManager.LoadScene("BeginScene");
        });
    }
    
    public void InitInfo(int money, bool isWin)
    {
        tmpStatus.text = isWin ? "胜利" : "失败";
        tmpContent.text = isWin ? "获得胜利奖励" : "获得失败奖励";
        tmpMoney.text = "￥" + money;
        
        DataManager.Instance.PlayerData.gold += money;
        DataManager.Instance.SavePlayerData();
    }
}