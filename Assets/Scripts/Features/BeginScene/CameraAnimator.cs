using UnityEngine;
using UnityEngine.Events;

namespace SSWC.Features.BeginScene
{
    /// <summary>
    /// 摄像机动画，有左转和右转两个行为
    /// </summary>
    public class CameraAnimator : MonoBehaviour
    {
        //动画转向结束后的回调，用于切换后的初始化
        private UnityAction _overCallback;
        //动画状态机组件
        private Animator _animator;
   
        private void Awake()
        {
            _animator = this.GetComponent<Animator>();
        }
   
        //左转
        public void LeftTurn(UnityAction callback)
        {
            _animator.SetTrigger("Left");
            _overCallback = callback;
        }
   
        //右转
        public void RightTurn(UnityAction callback)
        {
            _animator.SetTrigger("Right");
            _overCallback = callback;
        }
   
        //动画事件
        public void PlayOver()
        {
            _overCallback?.Invoke();
            _overCallback = null;
        }
    }
}
