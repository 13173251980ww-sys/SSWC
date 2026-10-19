using UnityEngine;

/// <summary>
/// 玩家物体
/// </summary>
public class PlayerObject : MonoBehaviour
{
    //玩家攻击力
    private int _atk;
    //玩家金钱
    public int money;
    //玩家旋转速度
    private float _rotateSpeed=250;
    //动画状态机组件
    private Animator _animator;
    //玩家的枪口位置
    public Transform gunPoint;
    //被刀攻击范围检测到的碰撞体们
    private Collider[] _colliders;
    //被枪攻击射线检测到的碰撞信息们
    private RaycastHit[] _hits;

    void Awake()
    {
        _colliders = new Collider[10];
        _hits = new RaycastHit[10];
    }
    
    private void Start()
    {
        _animator = this.GetComponent<Animator>();
    }
    
    /// <summary>
    /// 初始化玩家数据
    /// </summary>
    /// <param name="atk">攻击力</param>
    /// <param name="money">金钱</param>
    public void InitPlayer(int atk,int money)
    {
        _atk = atk;
        this.money = money;
        UpdateMoney();
    }
    
    /// <summary>
    /// 热路径控制玩家的移动，攻击动画
    /// </summary>
    void Update()
    {
        _animator.SetFloat("HSpeed", Input.GetAxis("Horizontal"));
        _animator.SetFloat("VSpeed", Input.GetAxis("Vertical"));
        
        //旋转
        this.transform.Rotate(Vector3.up,_rotateSpeed*Time.deltaTime*Input.GetAxis("Mouse X"));
        
        //蹲起
        if(Input.GetKeyDown(KeyCode.LeftShift))
            _animator.SetLayerWeight(1, 1);
        if(Input.GetKeyUp(KeyCode.LeftShift))
            _animator.SetLayerWeight(1, 0);
        
        //翻滚
        if(Input.GetKeyDown(KeyCode.R))
            _animator.SetTrigger("Roll");
        
        //开火
        if (Input.GetKeyUp(KeyCode.Mouse0))
            _animator.SetTrigger("Fire");
    }
    
    /// <summary>
    /// 动画事件-近战攻击范围检测
    /// </summary>
    public void KnifeEvent()
    {
        DataManager.Instance.PlaySound("Knife");
        int count =Physics.OverlapSphereNonAlloc(this.transform.position +this.transform.forward+this.transform.up,1,_colliders,1<<LayerMask.NameToLayer("Monster"));
        
        for (var i = 0; i < count; i++)
        {
            if (_colliders[i].gameObject.layer == LayerMask.NameToLayer("Monster"))
            {
                _colliders[i].gameObject.GetComponent<MonsterObject>().Wound(_atk);
            }
        }
    }
    
    /// <summary>
    /// 动画事件-远程攻击射线检测
    /// </summary>
    public void ShootEvent()
    {
        DataManager.Instance.PlaySound("Gun");
        int count=Physics.RaycastNonAlloc(gunPoint.position, this.transform.forward,_hits, 1000, 1 << LayerMask.NameToLayer("Monster"));
        
        for (var i = 0; i < count; i++)
        {
            MonsterObject monster = _hits[i].collider.gameObject.GetComponent<MonsterObject>();
            if(monster != null)
            {
                GameObject effObj = Instantiate(Resources.Load<GameObject>(DataManager.Instance.NowRoleInfoData.hitEff));
                effObj.transform.position = _hits[i].point;
                effObj.transform.rotation = Quaternion.LookRotation(_hits[i].normal);
                monster.Wound(_atk);
                break;
            }
        }
    }
    
    /// <summary>
    /// 更新玩家金钱显示
    /// </summary>
    public void UpdateMoney()
    {
        UIManager.Instance.GetPanel<GamePanel>().UpdateGold(money);
    }
}
