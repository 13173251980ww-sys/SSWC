using UnityEngine;
using UnityEngine.Events;

namespace SSWC.Common.UI
{
    /// <summary>
    /// 基础面板控件
    /// </summary>
    public abstract class BasePanel : MonoBehaviour
    {
        //控制面板整体的显隐，检测输入输出等
        private CanvasGroup _canvasGroup;
        //淡入淡出的速度
        private float _alphaSpeed = 5f;
        //面板是否显示
        public bool isShow;
        //隐藏后的回调，通常传入删除面板的回调
        private UnityAction _hideCallback;
    
        protected virtual void Awake()
        {
            if (_canvasGroup == null)
            {
                _canvasGroup = this.GetComponent<CanvasGroup>();
                if (_canvasGroup == null)
                {
                    _canvasGroup = this.gameObject.AddComponent<CanvasGroup>();
                }
            }
        }

        protected virtual void Start()
        {
            Init();
        }
    
        /// <summary>
        /// 初始化面板数据，子类必须重写
        /// </summary>
        public abstract void Init();
    
        /// <summary>
        /// 显示面板
        /// </summary>
        public virtual void ShowMe()
        {
            _canvasGroup.alpha = 0;
            isShow = true;
        }
    
        /// <summary>
        /// 隐藏面板
        /// </summary>
        /// <param name="callback">隐藏后的回调，通常传入删除面板的回调</param>
        public virtual void HideMe(UnityAction callback)
        {
            _canvasGroup.alpha = 1;
            isShow = false;
            _hideCallback = callback;
        }
    
        /// <summary>
        /// 控制淡入淡出
        /// </summary>
        protected virtual void Update()
        {
            if (isShow && _canvasGroup.alpha < 1)
            {
                _canvasGroup.alpha += _alphaSpeed * Time.deltaTime;
                if(_canvasGroup.alpha > 1)
                    _canvasGroup.alpha = 1;
            }
            else if (!isShow && _canvasGroup.alpha > 0)
            {
                _canvasGroup.alpha -= _alphaSpeed * Time.deltaTime;
                if (_canvasGroup.alpha <= 0)
                {
                    _canvasGroup.alpha = 0;
                    _hideCallback?.Invoke();
                }
            }
        }
    }
}


