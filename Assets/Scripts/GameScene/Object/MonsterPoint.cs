using System.Collections.Generic;
using UnityEngine;

namespace GameScene.Object
{
    public class MonsterPoint:MonoBehaviour
    {
        //怪物波数
        public int waveNum;
        //每波的怪物只数
        public int monsterNum;
        //每波的时间间隔
        public int waveInterval;
        //第一波的时间间隔
        public int firstWaveInterval;
        //每一只怪物生成的时间间隔
        public int monsterInterval;
        //怪物的ID列表
        public List<int> monsterIds;
        //当前波剩余的怪物只数
        private int _nowMonster;
        //当前的怪物ID
        private int _nowMonsterId;
        void Start()
        {
            GameLevelManager.Instance.AddMonsterPoint(this);
            GameLevelManager.Instance.UpdateWaveNum(waveNum);
            Invoke("CreateMonsterWave",firstWaveInterval);
        }
        
        public void CreateMonsterWave()
        {
            _nowMonster = monsterNum;
            waveNum--;
            CreateMonster();
            GameLevelManager.Instance.ChangeWaveNum(1);
        }
        
        public void CreateMonster()
        {
            _nowMonsterId = monsterIds[Random.Range(0, monsterIds.Count)];
            MonsterInfoData info = DataManager.Instance.MonsterInfoDatas[_nowMonsterId-1];
            GameObject obj = Instantiate(Resources.Load<GameObject>(info.res), this.transform.position, Quaternion.identity);
            //为我们创建出的怪物预设体 添加怪物脚本 进行初始化
            MonsterObject monsterObj = obj.GetComponent<MonsterObject>();
            monsterObj.InitMonster(info);
            
            _nowMonster--;
            GameLevelManager.Instance.MonsterNum++;
            
            if (_nowMonster == 0)
            {
                if (waveNum > 0)
                {
                    Invoke("CreateMonsterWave",waveInterval);
                }
            }
            else
            {
                Invoke("CreateMonster",monsterInterval);
            }
        }
        
        public bool CheckOver()
        {
            return _nowMonster == 0 && waveNum == 0;
        }
    }
}