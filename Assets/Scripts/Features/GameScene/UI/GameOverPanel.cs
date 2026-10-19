using SSWC.Common.UI;
using SSWC.Data;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SSWC.Features.GameScene.UI
{
    /// <summary>
    /// 游戏结束面板
    /// </summary>
    public class GameOverPanel : BasePanel
    {
        //游戏状态：胜利/失败
        public TextMeshProUGUI tmpStatus;

        //内容：获得胜利奖励/获得失败奖励
        public TextMeshProUGUI tmpContent;

        //获得的金币奖励
        public TextMeshProUGUI tmpMoney;

        //确认按钮
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

        /// <summary>
        /// 初始化游戏结束UI面板数据，并更新玩家金钱
        /// </summary>
        /// <param name="money">金钱</param>
        /// <param name="isWin">是否胜利</param>
        public void InitInfo(int money, bool isWin)
        {
            tmpStatus.text = isWin ? "胜利" : "失败";
            tmpContent.text = isWin ? "获得胜利奖励" : "获得失败奖励";
            tmpMoney.text = "￥" + money;

            DataManager.Instance.PlayerData.gold += money;
            DataManager.Instance.SavePlayerData();
        }
    }
}