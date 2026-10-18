using UnityEngine;
using UnityEngine.AI;

public class MonsterObject : MonoBehaviour
{
    private Animator _animator;
    private NavMeshAgent _navMeshAgent;
    
    private MonsterInfoData _info;
    
    private int _hp;
    public bool isDead;
    
    //上一次攻击的间隔
    private float _frontTime;

    private Collider[] _colliders;
    
    void Awake()
    {
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }
    
    //初始化属性
    public void InitMonster(MonsterInfoData monsterInfoData)
    {
        _info = monsterInfoData;
        _animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(_info.animator);
        _hp = monsterInfoData.hp;
        _navMeshAgent.speed = _navMeshAgent.acceleration=_info.moveSpeed;
        _navMeshAgent.angularSpeed = _info.roundSpeed;
        GameLevelManager.Instance.MonsterObjects.Add(this);
    }
    
    public void Wound(int dmg)
    {
        if (isDead)
            return;
        _hp -= dmg;
        _animator.SetTrigger("Wound");
        if (_hp <= 0)
        {
            _hp = 0;
            isDead=true;
            Dead();
        }
        else
        {
            //播放音效
            DataManager.Instance.PlaySound("Wound");
        }
    }
    
    private void Dead()
    {
        isDead=true;
        _navMeshAgent.isStopped=true;
        _animator.SetBool("Dead",true);
        
        //播放音效
        DataManager.Instance.PlaySound("dead");
        
        //加钱
        DataManager.Instance.PlayerData.gold += 50;
        UIManager.Instance.GetPanel<GamePanel>().UpdateGold(DataManager.Instance.PlayerData.gold);
    }
    
    //攻击——范围检测
    public void AtkEvent()
    {
        Physics.OverlapSphereNonAlloc(this.transform.position + this.transform.forward + this.transform.up, 1, _colliders,1 << LayerMask.NameToLayer("MainTower"));
        foreach (Collider col in _colliders)
        {
            if(col.gameObject == MainTowerObject.Instance.gameObject)
            {
                MainTowerObject.Instance.Wound(_info.atk);
                DataManager.Instance.PlaySound("Eat");
            }
        }
    }

    public void DeadEvent()
    {
        GameLevelManager.Instance.MonsterNum--;
        Destroy(this.gameObject);
        GameLevelManager.Instance.MonsterObjects.Remove(this);
        if (GameLevelManager.Instance.CheckOver())
        {
            //游戏胜利
            GameOverPanel panel=UIManager.Instance.ShowPanel<GameOverPanel>();
            panel.InitInfo(DataManager.Instance.NowRoleInfoData.money,true);
        }
    }
    
    //移动——寻路组件
    public void BornOver()
    {
        _navMeshAgent.SetDestination(MainTowerObject.Instance.transform.position);
        _animator.SetBool("Run", true);
    }

    void Update()
    {
        if (isDead)
            return;
        //根据速度 来决定动画播放什么
        _animator.SetBool("Run", _navMeshAgent.velocity != Vector3.zero);
        //检测和目标点达到移动条件时 就攻击
        if( Vector3.Distance(this.transform.position, MainTowerObject.Instance.transform.position ) < 5 &&
            Time.time - _frontTime >= _info.atkOffset)
        {
            //记录这次攻击时的时间
            _frontTime = Time.time;
            _animator.SetTrigger("Atk");
        }
    }
}
