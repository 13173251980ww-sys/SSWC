using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TowerPanel : MonoBehaviour
    {
        public Image imgPic;
        public TextMeshProUGUI tmpTip;
        public TextMeshProUGUI tmpMoney;
        

        public void InitInfo(int id,string tip)
        {
            TowerInfo info = DataManager.Instance.TowerInfoDatas[id - 1];
            tmpTip.text = tip;
            imgPic.sprite = Resources.Load<Sprite>(info.imgRes);
            tmpMoney.text = info.money.ToString();
        }
    }
}