using System.Collections.Generic;
using Data;
using GameScene.Object;
using UnityEngine;

namespace UI
{
    public class TowerPoint : MonoBehaviour
    {
        //造塔点关联的塔对象
        private GameObject towerObj = null;
        //造塔点关联的塔的数据
        public TowerInfo nowTowerInfo = null;
        //可以建造的三个塔的ID是什么
        public List<int> chooseIDs;
        
        public void CreateTower(int id)
        {
            TowerInfo info = DataManager.Instance.TowerInfoDatas[id - 1];
            //如果钱不够，就造不了
            if (info.money > DataManager.Instance.PlayerData.gold)
                return;
            
            //扣钱
            DataManager.Instance.PlayerData.gold -= info.money;
            if (towerObj != null)
            {
                Destroy(towerObj);
                towerObj = null;
            }
            
            Debug.Log(info.res);
            
            //实例化塔对象
            towerObj = Instantiate(Resources.Load<GameObject>(info.res), this.transform.position, Quaternion.identity);
            //初始化塔
            towerObj.GetComponent<TowerObject>().Init(info);
            nowTowerInfo = info;
            
            //塔建造完毕 更新游戏界面上的内容
            if(nowTowerInfo.next != 0)
            {
                UIManager.Instance.GetPanel<GamePanel>().UpdateSelTower(this);
            }
            else
            {
                UIManager.Instance.GetPanel<GamePanel>().UpdateSelTower(null);
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (nowTowerInfo != null && nowTowerInfo.next == 0)
                return;
            UIManager.Instance.GetPanel<GamePanel>().UpdateSelTower(this);
        }
        
        private void OnTriggerExit(Collider other)
        {
            UIManager.Instance.GetPanel<GamePanel>().UpdateSelTower(null);
        }
    }
}