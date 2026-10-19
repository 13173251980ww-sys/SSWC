using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using SSWC.Common.UI;
using SSWC.Data;
using SSWC.Data.Config;

namespace SSWC.Features.BeginScene.UI
{
    /// <summary>
    /// 人物选择面板
    /// </summary>
    public class SelectPanel : BasePanel
    {
        //左右键
        public Button btnLeft;
        public Button btnRight;

        //购买按钮
        public Button btnUnLock;
        public TextMeshProUGUI tmpUnLock;

        //开始返回
        public Button btnStart;
        public Button btnBack;

        //左上角拥有的钱
        public TextMeshProUGUI hasGold;

        //角色名称
        public TextMeshProUGUI roleName;

        //当前的角色
        public RoleInfoData NowRole;
        public int nowIndex;

        //角色位置
        public Transform heroPos;
        public Object heroObj;

        /// <summary>
        /// 绑定各组件回调
        /// </summary>
        public override void Init()
        {
            hasGold.text = "$:" + DataManager.Instance.PlayerData.gold;
            heroPos = GameObject.Find("HeroPos").transform;

            btnLeft.onClick.AddListener(() =>
            {
                nowIndex--;
                if (nowIndex < 0)
                    nowIndex = DataManager.Instance.RoleInfoDatas.Count - 1;
                //改变显示的模型
                ChangeRole();
            });

            btnRight.onClick.AddListener(() =>
            {
                nowIndex++;
                if (nowIndex > DataManager.Instance.RoleInfoDatas.Count - 1)
                    nowIndex = 0;
                //改变显示的模型
                ChangeRole();
            });

            btnStart.onClick.AddListener(() =>
            {
                if (DataManager.Instance.PlayerData.buyRoles.Contains(NowRole.id) || NowRole.lockMoney == 0)
                {
                    UIManager.Instance.HidePanel<SelectPanel>();
                    //进入选择场景，此时需要一个全局的管理器记录当前选择的角色，因此我们需要在DataManager里加一个数据类用来记录当前角色
                    DataManager.Instance.NowRoleInfoData = NowRole;
                    UIManager.Instance.ShowPanel<SceneSelectPanel>();
                }
            });

            btnBack.onClick.AddListener(() =>
            {
                Camera.main.GetComponent<CameraAnimator>().RightTurn(() =>
                {
                    UIManager.Instance.ShowPanel<StartPanel>();
                });
                UIManager.Instance.HidePanel<SelectPanel>();
            });

            btnUnLock.onClick.AddListener(() =>
            {
                if (DataManager.Instance.PlayerData.gold >= NowRole.lockMoney &&
                    !DataManager.Instance.PlayerData.buyRoles.Contains(NowRole.id))
                {
                    DataManager.Instance.PlayerData.gold -= NowRole.lockMoney;
                    DataManager.Instance.PlayerData.buyRoles.Add(NowRole.id);

                    UpdateBtn();
                    hasGold.text = "$:" + DataManager.Instance.PlayerData.gold;
                    DataManager.Instance.SavePlayerData();

                    //显示提示购买成功
                    UIManager.Instance.ShowPanel<TipPanel>().ChangeValue("购买成功");
                }
                else
                {
                    //提示面板显示金钱不足
                    UIManager.Instance.ShowPanel<TipPanel>().ChangeValue("金钱不足");
                }
            });

            ChangeRole();
        }

        /// <summary>
        /// 根据当前ID，更新UI,显示不同的角色
        /// </summary>
        public void ChangeRole()
        {
            if (heroObj != null)
                Destroy(heroObj);
            NowRole = DataManager.Instance.RoleInfoDatas[nowIndex];
            roleName.text = NowRole.tips;
            hasGold.text = "$:" + DataManager.Instance.PlayerData.gold;
            tmpUnLock.text = NowRole.lockMoney.ToString();
            heroObj = GameObject.Instantiate(Resources.Load(NowRole.res), heroPos.position, heroPos.rotation);

            UpdateBtn();
        }

        /// <summary>
        /// 根据条件判断是否显示解锁按钮
        /// </summary>
        public void UpdateBtn()
        {
            //如果该角色需要解锁却没有解锁，就显示解锁按钮
            if (NowRole.lockMoney > 0 && DataManager.Instance.PlayerData.buyRoles.Contains(NowRole.id) == false)
            {
                btnUnLock.gameObject.SetActive(true);
            }

            //如果该角色不需要解锁或者已经解锁了，就隐藏解锁按钮
            if (NowRole.lockMoney == 0 || DataManager.Instance.PlayerData.buyRoles.Contains(NowRole.id))
            {
                btnUnLock.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// 重写隐藏自己的方法，在隐藏的时候销毁当前的角色模型
        /// </summary>
        /// <param name="callback"></param>
        public override void HideMe(UnityAction callback)
        {
            base.HideMe(callback);
            if (heroObj != null)
                Destroy(heroObj);
        }
    }
}
