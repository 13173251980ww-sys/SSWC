using System.Collections.Generic;
using GameScene.Object;
using UnityEngine;

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
    
    //初始化玩家信息
    public void InitPlayerInfo()
    {
        UIManager.Instance.ShowPanel<GamePanel>();
        
        //玩家的创建
        //获取之前存储的全局的玩家数据
        RoleInfoData info = DataManager.Instance.NowRoleInfoData;
        //获取玩家的出生位置
        Transform roleTransform = GameObject.Find("RoleInfoPos").transform;
        //实例化玩家
        GameObject heroObj = Object.Instantiate(Resources.Load<GameObject>(info.res), roleTransform.position, roleTransform.rotation);
        PlayerObject player = heroObj.AddComponent<PlayerObject>();
        player.InitPlayer(info.atk,DataManager.Instance.PlayerData.gold);
        GameObject gunPoint = GameObject.Find("gunPoint");
        if(gunPoint!=null)
            player.GetComponent<PlayerObject>().gunPoint = gunPoint.transform;
        
        Camera.main.GetComponent<CameraMove>().SetTarget(heroObj.transform);
        
        MainTowerObject.Instance.UpdateHp(info.towerHP,info.towerHP);
    }
    
    public void AddMonsterPoint(MonsterPoint monsterPoint)
    {
        _monsterPoints.Add(monsterPoint);
    }
    
    //更新波数
    public void UpdateWaveNum(int num)
    {
        _maxWaveNum += num;
        _nowWaveNum = _maxWaveNum;
        //更新界面
        UIManager.Instance.GetPanel<GamePanel>().UpdateWave(_nowWaveNum,_maxWaveNum);
    }
    
    public void ChangeWaveNum(int num)
    {
        _nowWaveNum -= num;
        UIManager.Instance.GetPanel<GamePanel>().UpdateWave(_nowWaveNum,_maxWaveNum);
    }
    
    //判断是否胜利：1.波数要结束 2.场上没有怪物
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
    
    public void ClearInfo()
    {
        _monsterPoints.Clear();
        _maxWaveNum = _nowWaveNum = _nowWaveNum= 0;
        _instance = null;
    }
}
