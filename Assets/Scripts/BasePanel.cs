
using UnityEngine;
using UnityEngine.Events;

public abstract class BasePanel : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    private float _alphaSpeed = 0.1f;
    public bool _isShow = false;
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

    public abstract void Init();

    public virtual void ShowMe()
    {
        _canvasGroup.alpha = 0;
        _isShow = true;
    }

    public virtual void HideMe(UnityAction callback)
    {
        _canvasGroup.alpha = 1;
        _isShow = false;
        _hideCallback = callback;
    }

    protected virtual void Update()
    {
        if (_isShow && _canvasGroup.alpha < 1)
        {
            _canvasGroup.alpha += _alphaSpeed * Time.deltaTime;
            if(_canvasGroup.alpha > 1)
                _canvasGroup.alpha = 1;
        }
        else if (!_isShow && _canvasGroup.alpha > 0)
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
