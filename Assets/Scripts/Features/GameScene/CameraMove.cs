using UnityEngine;

namespace SSWC.Features.GameScene
{
    /// <summary>
    /// 摄像机跟随
    /// </summary>
    public class CameraMove : MonoBehaviour
    {
        //摄像机要跟随的对象
        private Transform _target;

        //摄像机偏移
        public Vector3 offset;

        //摄像机要看向的目标的身体高度
        public float bodyHeight;

        //摄像机移动的速度
        public float moveSpeed;

        //摄像机旋转的度数
        public float rotateSPeed;

        //摄像机的位置
        private Vector3 _targetPos;

        //摄像机的角度
        private Quaternion _targetRot;

        /// <summary>
        /// 在热路径里进行实时摄像机跟随
        /// </summary>
        void Update()
        {
            if (_target is null)
                return;
            //向后偏移Z坐标
            _targetPos = _target.position + _target.forward * offset.z;
            //向上偏移Y坐标
            _targetPos += Vector3.up * offset.y;
            //向右偏移X坐标
            _targetPos += _target.right * offset.x;

            this.transform.position = Vector3.Lerp(this.transform.position, _targetPos, moveSpeed * Time.deltaTime);

            _targetRot = Quaternion.LookRotation(_target.position + Vector3.up * bodyHeight - this.transform.position);
            this.transform.rotation =
                Quaternion.Slerp(this.transform.rotation, _targetRot, rotateSPeed * Time.deltaTime);
        }

        //设置摄像机要跟随的对象
        public void SetTarget(Transform playerTarget)
        {
            _target = playerTarget;
        }
    }
}
