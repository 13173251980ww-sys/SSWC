using UnityEngine;

public class MainTowerObject : MonoBehaviour
{
    private int _hp;
    private int _maxHp;
    private bool _isDead;
    
    private static MainTowerObject _instance;
    public static MainTowerObject Instance => _instance;

    void Awake()
    {
        _instance = this;
    }
    //受伤，更新血量
    public void UpdateHp(int hp, int maxHp)
    {
        this._hp = hp;
        this._maxHp = maxHp;
        
        UIManager.Instance.GetPanel<GamePanel>().UpdateHp(_hp, _maxHp);
    }

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
