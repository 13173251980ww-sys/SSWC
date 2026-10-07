using UnityEngine;
using UnityEngine.Events;

public class CameraAnimator : MonoBehaviour
{
   private UnityAction _overCallback;
   private Animator _animator;

   private void Awake()
   {
      _animator = this.GetComponent<Animator>();
   }
   
   public void LeftTurn(UnityAction callback)
   {
      _animator.SetTrigger("Left");
      _overCallback = callback;
   }

   public void RightTurn(UnityAction callback)
   {
      _animator.SetTrigger("Right");
      _overCallback = callback;
   }

   public void PlayOver()
   {
      _overCallback?.Invoke();
      _overCallback = null;
   }
}
