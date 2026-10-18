using System.Collections.Generic;
using Data;
using UnityEngine;

namespace GameScene.Object
{
    public class TowerObject : MonoBehaviour
    {
        public Transform head;
        private float _roundSpeed = 2;
        
        //炮台关联的数据
        private TowerInfo _info;
        
        //当前要攻击的目标
        private MonsterObject _targetObj;
        //当前要攻击的目标们
        private List<MonsterObject> _targetObjs;

        private float preTime=0;
        
        public void Init(TowerInfo info)
        {
            this._info = info;
        }
        
        
        void Update()
        {
            if (_info.type == 1)
            {
                //单体攻击逻辑
                MonsterObject monsterObject = GameLevelManager.Instance.FindMonster(this.transform.position,_info.atkRange);
                
                if (monsterObject == null)
                    return;
                
                Quaternion targetRotation = Quaternion.LookRotation(monsterObject.transform.position - head.transform.position);
                head.transform.rotation = Quaternion.Slerp(head.transform.rotation, targetRotation, _roundSpeed * Time.deltaTime);
                
                //当炮台的枪口根怪物之间的角度相差小于5度后，开枪攻击
                if(Quaternion.Angle(head.transform.rotation ,targetRotation)<5f && Time.time - preTime >= _info.offsetTime)
                {
                    preTime = Time.time;
                    GameObject effObj = Instantiate(Resources.Load<GameObject>(_info.eff));
                    effObj.transform.position = monsterObject.transform.position;
                    effObj.transform.rotation = targetRotation;
                    monsterObject.Wound(_info.atk);
                }
            }
            else if (_info.type == 2)
            {
                //群体攻击逻辑
                List<MonsterObject> monsterObjs = GameLevelManager.Instance.FindMonsters(this.transform.position,200);
                
                if (monsterObjs == null)
                    return;
                
                for (var i = 0; i < monsterObjs.Count; i++)
                {
                    monsterObjs[i].Wound(_info.atk);
                }
                
                if (monsterObjs.Count == 0)
                    return;
                
                GameObject effObj = Instantiate(Resources.Load<GameObject>(_info.eff));
                effObj.transform.position = this.transform.position;
                effObj.transform.rotation = this.transform.rotation;
            }
        }
    }
}