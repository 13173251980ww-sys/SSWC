using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    //初始化属性
    private int atk;
    public int money;

    private float rotateSpeed=250;
    
    private Animator _animator;

    public Transform _gunPoint;
    
    private void Start()
    {
        _animator = this.GetComponent<Animator>();
    }

    public void InitPlayer(int atk,int money)
    {
        this.atk = atk;
        this.money = money;
        UpdateMoney();
    }
    
    //控制玩家移动，动画
    void Update()
    {
        _animator.SetFloat("HSpeed", Input.GetAxis("Horizontal"));
        _animator.SetFloat("VSpeed", Input.GetAxis("Vertical"));
        
        //旋转
        this.transform.Rotate(Vector3.up,rotateSpeed*Time.deltaTime*Input.GetAxis("Mouse X"));
        
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
        Collider[] colliders = Physics.OverlapSphere(this.transform.position +this.transform.forward+this.transform.up,1,1<<LayerMask.NameToLayer("Monster"));
        for (var i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.layer == LayerMask.NameToLayer("Monster"))
            {
                colliders[i].gameObject.GetComponent<MonsterObject>().Wound(atk);
            }
        }
    }
    
    public void ShootEvent()
    {
        DataManager.Instance.PlaySound("Gun");
        RaycastHit[] hits = Physics.RaycastAll(_gunPoint.position, this.transform.forward, 1000, 1 << LayerMask.NameToLayer("Monster"));
        for (var i = 0; i < hits.Length; i++)
        {
            MonsterObject monster = hits[i].collider.gameObject.GetComponent<MonsterObject>();
            if(monster != null)
            {
                GameObject effObj = Instantiate(Resources.Load<GameObject>(DataManager.Instance.NowRoleInfoData.hitEff));
                effObj.transform.position = hits[i].point;
                effObj.transform.rotation = Quaternion.LookRotation(hits[i].normal);
                monster.Wound(atk);
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
