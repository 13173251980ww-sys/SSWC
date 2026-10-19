using System.Collections.Generic;
using SSWC.Common.UI;
using SSWC.Features.GameScene.Gameplay;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace SSWC.Features.GameScene.UI
{
    /// <summary>
    /// 游戏内部面板
    /// </summary>
    public class GamePanel : BasePanel
    {
        //血条图像
        public Image imgHP;
        //血量
        public TextMeshProUGUI tmpHp;
        //波数
        public TextMeshProUGUI tmpWave;
        //金钱
        public TextMeshProUGUI tmpGold;
        //血条图像的宽度
        public float hpW = 500;
        //退出按钮
        public Button btnQuit;
        //下面的塔按钮的显示范围
        public Transform botTrans;
        //塔面板列表
        public List<TowerPanel> TowerPanels;
        //是否检测造塔
        private bool _checkInput;
        //当前选择的造塔点
        private TowerPoint _nowSelectPoint;
        
        /// <summary>
        /// 绑定各组件的回调
        /// </summary>
        public override void Init()
        {
            btnQuit.onClick.AddListener(() =>
            {
                UIManager.Instance.HidePanel<GamePanel>();
                //清空场景数据
                GameLevelManager.Instance.ClearInfo();
                //跳转到开始界面
                SceneManager.LoadScene("BeginScene");
            });
            
            //一开始隐藏下方的按钮
            botTrans.gameObject.SetActive(false);
        }
        
        /// <summary>
        /// 更新UI面板上的血量
        /// </summary>
        /// <param name="hp">当前血量</param>
        /// <param name="maxHp">最大血量</param>
        public void UpdateHp(int hp,int maxHp)
        {
            tmpHp.text = hp + "/" + maxHp;
            imgHP.rectTransform.sizeDelta = new Vector2((float)hp/maxHp * hpW, imgHP.rectTransform.sizeDelta.y);
        }
        
        /// <summary>
        /// 更新UI面板上的金钱
        /// </summary>
        /// <param name="gold">金钱</param>
        public void UpdateGold(int gold)
        {
            tmpGold.text = gold.ToString();
        }
        
        /// <summary>
        /// 更新UI面板上的波数
        /// </summary>
        /// <param name="wave">当前波数</param>
        /// <param name="maxWave">最大波数</param>
        public void UpdateWave(int wave,int maxWave)
        {
            tmpWave.text = wave + "/" + maxWave;
        }
        
        /// <summary>
        /// 根据当前传入的造塔点显示不同的UI界面
        /// </summary>
        /// <param name="point">当前造塔点</param>
        public void UpdateSelTower(TowerPoint point)
        {
            _nowSelectPoint = point;
            if (_nowSelectPoint == null)
            {
                _checkInput = false;
                botTrans.gameObject.SetActive(false);
            }
            else
            {
                _checkInput = true;
                botTrans.gameObject.SetActive(true);
                //如果没有造过塔
                if (_nowSelectPoint.nowTowerInfo == null)
                {
                    for (int i = 0; i < TowerPanels.Count; i++)
                    {
                        TowerPanels[i].gameObject.SetActive(true);
                        TowerPanels[i].InitInfo(_nowSelectPoint.chooseIDs[i],"数字键"+(i+1));
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
                    TowerPanels[1].InitInfo(_nowSelectPoint.nowTowerInfo.next,"空格键");
                }
            }
        }
        
        
        /// <summary>
        /// 进入造塔点后，在热路径中不断检测按键输入。离开后终止检测
        /// </summary>
        protected override void Update()
        {
            base.Update();
            
            if (!_checkInput)
                return;
            //如果没有造过塔
            if (_nowSelectPoint.nowTowerInfo == null)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    _nowSelectPoint.CreateTower(_nowSelectPoint.chooseIDs[0]);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    _nowSelectPoint.CreateTower(_nowSelectPoint.chooseIDs[1]);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    _nowSelectPoint.CreateTower(_nowSelectPoint.chooseIDs[2]);
                }
            }
            //如果造过塔
            else
            {
                if( Input.GetKeyDown(KeyCode.Space) )
                {
                    _nowSelectPoint.CreateTower(_nowSelectPoint.nowTowerInfo.next);
                }
            }
        }
    }
}



