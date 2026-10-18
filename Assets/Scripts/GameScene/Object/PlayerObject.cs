using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    //初始化属性
    private int _atk;
    public int money;

    private float _rotateSpeed=250;
    
    private Animator _animator;

    public Transform gunPoint;

    private Collider[] _colliders;

    private RaycastHit[] _hits;
    
    private void Start()
    {
        _animator = this.GetComponent<Animator>();
    }

    public void InitPlayer(int atk,int money)
    {
        _atk = atk;
        this.money = money;
        UpdateMoney();
    }
    
    //控制玩家移动，动画
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
    
    //攻击范围检测
    public void KnifeEvent()
    {
        DataManager.Instance.PlaySound("Knife");
        Physics.OverlapSphereNonAlloc(this.transform.position +this.transform.forward+this.transform.up,1,_colliders,1<<LayerMask.NameToLayer("Monster"));

        if (_colliders.Length == 0)
        {
            return;
        }
        
        for (var i = 0; i < _colliders.Length; i++)
        {
            if (_colliders[i].gameObject.layer == LayerMask.NameToLayer("Monster"))
            {
                _colliders[i].gameObject.GetComponent<MonsterObject>().Wound(_atk);
            }
        }
    }
    
    public void ShootEvent()
    {
        DataManager.Instance.PlaySound("Gun");
        Physics.RaycastNonAlloc(gunPoint.position, this.transform.forward,_hits, 1000, 1 << LayerMask.NameToLayer("Monster"));
        
        if(_hits.Length ==0)
            return;
        
        for (var i = 0; i < _hits.Length; i++)
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
    
    //钱变化的逻辑
    public void UpdateMoney()
    {
        UIManager.Instance.GetPanel<GamePanel>().UpdateGold(money);
    }
}
