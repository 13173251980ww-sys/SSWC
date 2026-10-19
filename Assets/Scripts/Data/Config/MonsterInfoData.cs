namespace SSWC.Data.Config
{
    /// <summary>
    /// 怪物配置数据
    /// </summary>
    public class MonsterInfoData
    {
        // 怪物 ID
        public int id;

        // 预制体资源路径
        public string res;

        // 动画控制器资源路径
        public string animator;

        // 攻击力
        public int atk;

        // 移动速度
        public float moveSpeed;

        // 转向速度
        public float roundSpeed;

        // 血量
        public int hp;

        // 攻击间隔
        public float atkOffset;
    }
}