using SSWC.Common.UI;
using SSWC.Data;
using SSWC.Data.Config;
using SSWC.Features.GameScene.UI;
using UnityEngine;
using UnityEngine.AI;

namespace SSWC.Features.GameScene.Gameplay
{
    /// <summary>
    /// 怪物物体
    /// </summary>
    public class MonsterObject : MonoBehaviour
    {
        //动画状态机组件
        private Animator _animator;

        //导航寻路组件
        private NavMeshAgent _navMeshAgent;

        //怪物信息
        private MonsterInfoData _info;

        //怪物的血量
        private int _hp;

        //判断怪物是否死亡
        public bool isDead;

        //上一次攻击的间隔
        private float _frontTime;

        //怪物攻击，检测到的物体
        private Collider[] _colliders;

        void Awake()
        {
            _animator = GetComponent<Animator>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _colliders = new Collider[10];
        }

        //初始化属性
        public void InitMonster(MonsterInfoData monsterInfoData)
        {
            _info = monsterInfoData;
            _animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(_info.animator);
            _hp = monsterInfoData.hp;
            _navMeshAgent.speed = _navMeshAgent.acceleration = _info.moveSpeed;
            _navMeshAgent.angularSpeed = _info.roundSpeed;
            GameLevelManager.Instance.MonsterObjects.Add(this);
        }

        /// <summary>
        /// 怪物受伤
        /// </summary>
        /// <param name="dmg">伤害</param>
        public void Wound(int dmg)
        {
            if (isDead)
                return;
            _hp -= dmg;
            _animator.SetTrigger("Wound");
            if (_hp <= 0)
            {
                _hp = 0;
                isDead = true;
                Dead();
            }
            else
            {
                //播放音效
                DataManager.Instance.PlaySound("Wound");
            }
        }

        /// <summary>
        /// 怪物死亡
        /// </summary>
        private void Dead()
        {
            isDead = true;
            _navMeshAgent.isStopped = true;
            _animator.SetBool("Dead", true);

            //播放音效
            DataManager.Instance.PlaySound("dead");

            //加钱
            DataManager.Instance.PlayerData.gold += 50;
            UIManager.Instance.GetPanel<GamePanel>().UpdateGold(DataManager.Instance.PlayerData.gold);
        }

        /// <summary>
        /// 动画事件——怪物攻击
        /// </summary>
        public void AtkEvent()
        {
            int count = Physics.OverlapSphereNonAlloc(
                this.transform.position + this.transform.forward + this.transform.up, 1, _colliders,
                1 << LayerMask.NameToLayer("MainTower"));

            for (int i = 0; i < count; i++)
            {
                if (_colliders[i].gameObject == MainTowerObject.Instance.gameObject)
                {
                    MainTowerObject.Instance.Wound(_info.atk);
                    DataManager.Instance.PlaySound("Eat");
                }
            }
        }

        /// <summary>
        /// 动画事件——怪物死亡
        /// </summary>
        public void DeadEvent()
        {
            GameLevelManager.Instance.MonsterNum--;
            Destroy(this.gameObject);
            GameLevelManager.Instance.MonsterObjects.Remove(this);
            if (GameLevelManager.Instance.CheckOver())
            {
                //游戏胜利
                GameOverPanel panel = UIManager.Instance.ShowPanel<GameOverPanel>();
                panel.InitInfo(DataManager.Instance.NowRoleInfoData.money, true);
            }
        }

        /// <summary>
        ///动画事件——怪物出生后移动
        /// </summary>
        public void BornOver()
        {
            _navMeshAgent.SetDestination(MainTowerObject.Instance.transform.position);
            _animator.SetBool("Run", true);
        }

        /// <summary>
        /// 热路径内检测移动和动画
        /// </summary>
        void Update()
        {
            if (isDead)
                return;
            //根据速度 来决定动画播放什么
            _animator.SetBool("Run", _navMeshAgent.velocity != Vector3.zero);
            //检测和目标点达到移动条件时 就攻击
            if (Vector3.Distance(this.transform.position, MainTowerObject.Instance.transform.position) < 5 &&
                Time.time - _frontTime >= _info.atkOffset)
            {
                //记录这次攻击时的时间
                _frontTime = Time.time;
                _animator.SetTrigger("Atk");
            }
        }
    }
}
