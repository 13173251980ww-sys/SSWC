using System.Collections.Generic;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePanel : BasePanel
{
    public Image imgHP;
    public TextMeshProUGUI tmpHp;
    public TextMeshProUGUI tmpWave;
    public TextMeshProUGUI tmpGold;
    public float hpW = 500;

    public Button btnQuit;

    public Transform botTrans;
    public List<TowerPanel> TowerPanels;
    
    private bool checkInput = false;
    private TowerPoint nowSelectPoint;
    
    public override void Init()
    {
        btnQuit.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<GamePanel>();
            //跳转到开始界面
            SceneManager.LoadScene("BeginScene");
        });
        
        //一开始隐藏下方的按钮
        botTrans.gameObject.SetActive(false);
    }
    
    //更新HP,金钱，波数
    public void UpdateHp(int hp,int maxHp)
    {
        tmpHp.text = hp + "/" + maxHp;
        imgHP.rectTransform.sizeDelta = new Vector2((float)hp/maxHp * hpW, imgHP.rectTransform.sizeDelta.y);
    }
    
    public void UpdateGold(int gold)
    {
        tmpGold.text = gold.ToString();
    }
    
    public void UpdateWave(int wave,int maxWave)
    {
        tmpWave.text = wave + "/" + maxWave;
    }
    
    public void UpdateSelTower(TowerPoint point)
    {
        nowSelectPoint = point;
        if (nowSelectPoint == null)
        {
            checkInput = false;
            botTrans.gameObject.SetActive(false);
        }
        else
        {
            checkInput = true;
            botTrans.gameObject.SetActive(true);
            //如果没有造过塔
            if (nowSelectPoint.nowTowerInfo == null)
            {
                for (int i = 0; i < TowerPanels.Count; i++)
                {
                    TowerPanels[i].gameObject.SetActive(true);
                    TowerPanels[i].InitInfo(nowSelectPoint.chooseIDs[i],"数字键"+(i+1));
                }
            }
            //如果造过塔
            else
            {
                for (int i = 0; i < TowerPanels.Count; i++)
                {
                    TowerPanels[i].gameObject.SetActive(false);
                }
                TowerPanels[1].gameObject.SetActive(true);
                TowerPanels[1].InitInfo(nowSelectPoint.nowTowerInfo.next,"空格键");
            }
        }
    }

    protected override void Update()
    {
        base.Update();
        
        if (!checkInput)
            return;
        //如果没有造过塔
        if (nowSelectPoint.nowTowerInfo == null)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                nowSelectPoint.CreateTower(nowSelectPoint.chooseIDs[0]);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                nowSelectPoint.CreateTower(nowSelectPoint.chooseIDs[1]);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                nowSelectPoint.CreateTower(nowSelectPoint.chooseIDs[2]);
            }
        }
        //如果造过塔
        else
        {
            if( Input.GetKeyDown(KeyCode.Space) )
            {
                nowSelectPoint.CreateTower(nowSelectPoint.nowTowerInfo.next);
            }
        }
    }
}
