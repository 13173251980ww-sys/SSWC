using SSWC.Common.UI;
using SSWC.Data;
using SSWC.Features.GameScene.UI;
using UnityEngine;

namespace SSWC.Features.GameScene.Gameplay
{
    /// <summary>
    /// 主塔物体
    /// </summary>
    public class MainTowerObject : MonoBehaviour
    {
        //主塔的当前血量
        private int _hp;
        //主塔的最大血量
        private int _maxHp;
        //主塔是否死亡
        private bool _isDead;
    
        private static MainTowerObject _instance;
        public static MainTowerObject Instance => _instance;

        void Awake()
        {
            _instance = this;
        }
        /// <summary>
        /// 更新UI面板上的血量
        /// </summary>
        /// <param name="hp">当前HP</param>
        /// <param name="maxHp">最大的HP</param>
        public void UpdateHp(int hp, int maxHp)
        {
            this._hp = hp;
            this._maxHp = maxHp;
        
            UIManager.Instance.GetPanel<GamePanel>().UpdateHp(_hp, _maxHp);
        }
    
        /// <summary>
        /// 主塔受伤
        /// </summary>
        /// <param name="damage">攻击伤害</param>
        public void Wound(int damage)
        {
            if (_isDead)
                return;
            this._hp -= damage;
            if (this._hp <= 0)
            {
                _hp = 0;
                _isDead = true;
                //游戏失败
                GameOverPanel panel=UIManager.Instance.ShowPanel<GameOverPanel>();
                panel.InitInfo((int)(DataManager.Instance.NowRoleInfoData.money*0.5f),false);
            }
        
            UpdateHp(_hp, _maxHp);
        }
    }
}
