using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 关卡管理器
/// </summary>
public class GameLevelManager 
{
    private static GameLevelManager _instance;
    
    public static GameLevelManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new GameLevelManager();
            return _instance;
        }
    }
    
    //记录当前 还有多少波怪物
    private int _nowWaveNum;
    //记录 一共有多少波怪物
    private int _maxWaveNum;
    
    //记录一共有多少只怪物
    public int MonsterNum=0;
    
    //有多少个刷怪点
    private List<MonsterPoint> _monsterPoints = new List<MonsterPoint>();
    
    //怪物
    public List<MonsterObject> MonsterObjects = new List<MonsterObject>();
    
    /// <summary>
    /// 初始化玩家信息
    /// </summary>
    public void InitPlayerInfo()
    {
        UIManager.Instance.ShowPanel<GamePanel>();
        
        RoleInfoData info = DataManager.Instance.NowRoleInfoData;
        Transform roleTransform = GameObject.Find("RoleInfoPos").transform;
        GameObject heroObj = Object.Instantiate(Resources.Load<GameObject>(info.res), roleTransform.position, roleTransform.rotation);
        PlayerObject player = heroObj.AddComponent<PlayerObject>();
        player.InitPlayer(info.atk,DataManager.Instance.PlayerData.gold);
        GameObject gunPoint = GameObject.Find("gunPoint");
        if(gunPoint!=null)
            player.GetComponent<PlayerObject>().gunPoint = gunPoint.transform;
        
        Camera.main.GetComponent<CameraMove>().SetTarget(heroObj.transform);
        
        MainTowerObject.Instance.UpdateHp(info.towerHP,info.towerHP);
    }
    
    /// <summary>
    /// 将刷怪点缓存到列表中
    /// </summary>
    /// <param name="monsterPoint">刷怪点</param>
    public void AddMonsterPoint(MonsterPoint monsterPoint)
    {
        _monsterPoints.Add(monsterPoint);
    }
    
    /// <summary>
    /// 更新波数
    /// </summary>
    /// <param name="num">要增加的波数</param>
    public void UpdateWaveNum(int num)
    {
        _maxWaveNum += num;
        _nowWaveNum = _maxWaveNum;
        //更新界面
        UIManager.Instance.GetPanel<GamePanel>().UpdateWave(_nowWaveNum,_maxWaveNum);
    }
    
    /// <summary>
    /// 减少波数
    /// </summary>
    /// <param name="num">要减少的数量</param>
    public void ChangeWaveNum(int num)
    {
        _nowWaveNum -= num;
        UIManager.Instance.GetPanel<GamePanel>().UpdateWave(_nowWaveNum,_maxWaveNum);
    }
    
    /// <summary>
    /// 判断是否胜利
    /// </summary>
    /// <returns>是否胜利</returns>
    public bool CheckOver()
    {
        for (var i = 0; i < _monsterPoints.Count; i++)
        {
            if (!_monsterPoints[i].CheckOver())
            {
                return false;
            }
        }
        
        if (MonsterNum != 0)
        {
            return false;
        }
        
        return true;
    }
    
    /// <summary>
    /// 查找距离玩家最近的怪物
    /// </summary>
    /// <param name="pos">炮塔的位置</param>
    /// <param name="range">炮塔的攻击范围</param>
    /// <returns></returns>
    public MonsterObject FindMonster(Vector3 pos,int range)
    {
        //在怪物列表中查找符合距离的怪物
        for (var i = 0; i < MonsterObjects.Count; i++)
        {
            if (!MonsterObjects[i].isDead && Vector3.Distance(pos,MonsterObjects[i].transform.position) <= range)
            {
                return MonsterObjects[i];
            }
        }

        return null;
    }
    
    /// <summary>
    /// 查找距离玩家最近的怪物们
    /// </summary>
    /// <param name="pos">炮塔的位置</param>
    /// <param name="range">炮塔的攻击范围</param>
    /// <returns></returns>
    public List<MonsterObject> FindMonsters(Vector3 pos,int range)
    {
        List<MonsterObject> monsters = new List<MonsterObject>();
        //在怪物列表中查找符合距离的怪物
        for (var i = 0; i < MonsterObjects.Count; i++)
        {
            if (!MonsterObjects[i].isDead && Vector3.Distance(pos,MonsterObjects[i].transform.position) <= range)
            {
                monsters.Add(MonsterObjects[i]);
            }
        }
        
        if(monsters.Count !=0)
            return monsters;
        return null;
    }
    
    /// <summary>
    /// 清空场景缓存的信息，用于在切换场景时使用。
    /// </summary>
    public void ClearInfo()
    {
        _monsterPoints.Clear();
        _maxWaveNum = _nowWaveNum = _nowWaveNum= 0;
        _instance = null;
    }
}
