using SSWC.Data;
using SSWC.Data.Config;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace SSWC.Features.GameScene.UI
{
    /// <summary>
    /// 塔面板
    /// </summary>
    public class TowerPanel : MonoBehaviour
    {
        //塔的图标
        public Image imgPic;
        //塔的名字
        public TextMeshProUGUI tmpTip;
        //解锁塔所需的金钱
        public TextMeshProUGUI tmpMoney;
    
    
        /// <summary>
        /// 根据id和tip初始化当前面板
        /// </summary>
        /// <param name="id"></param>
        /// <param name="tip"></param>
        public void InitInfo(int id,string tip)
        {
            TowerInfo info = DataManager.Instance.TowerInfoDatas[id - 1];
            tmpTip.text = tip;
            imgPic.sprite = Resources.Load<Sprite>(info.imgRes);
            tmpMoney.text = info.money.ToString();
        }
    }
}

