using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI面板管理器
/// </summary>
public class UIManager
{
    private static UIManager _instance;
    
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new UIManager();
            }
            return _instance;
        }
    }
    
    //Dictionary存储所有的面板
    private Dictionary<string,BasePanel> _panelDic = new Dictionary<string, BasePanel>();
    //Canvas父对象
    private Transform _canvasTrans;
    
    /// <summary>
    /// 实例化Canvas并设置DontDestroyOnLoad
    /// </summary>
    public UIManager()
    {
        GameObject canvas = Object.Instantiate(Resources.Load<GameObject>("UI/Canvas"));
        _canvasTrans = canvas.transform;
        Object.DontDestroyOnLoad(canvas);
    }
    
    /// <summary>
    /// 显示面板，先从字典缓存中拿，如果没有就实例化
    /// </summary>
    /// <typeparam name="T">继承基础面板的面板</typeparam>
    /// <returns></returns>
    public T ShowPanel<T>() where T : BasePanel
    {
        string panelName = typeof(T).Name;
        if (_panelDic.ContainsKey(panelName))
            return _panelDic[panelName] as T;
        
        GameObject panelObj = Object.Instantiate(Resources.Load<GameObject>("UI/" + panelName));
        T panel = panelObj.GetComponent<T>();
        panel.ShowMe();
        panel.transform.SetParent(_canvasTrans,false);
        _panelDic.Add(panelName, panel);
        
        return panel;
    }
    
    /// <summary>
    /// 隐藏面板
    /// </summary>
    /// <param name="isFade">是否等待渐变</param>
    /// <typeparam name="T">继承基础面板的面板</typeparam>
    public void HidePanel<T>(bool isFade =true) where T : BasePanel
    {
        string panelName = typeof(T).Name;
        if(_panelDic.ContainsKey(panelName))
        {
            BasePanel panel = _panelDic[panelName];
            if (isFade)
            {
                panel.HideMe(() =>
                {
                    Object.Destroy(panel.gameObject);
                    _panelDic.Remove(panelName);
                });
            }
            else
            {
                Object.Destroy(panel.gameObject);    
                _panelDic.Remove(panelName);
            }
        }   
    }
    
    /// <summary>
    /// 获取面板
    /// </summary>
    /// <typeparam name="T">继承基础面板的面板</typeparam>
    /// <returns></returns>
    public T GetPanel<T>() where T : BasePanel
    {
        string panelName = typeof(T).Name;
        if(_panelDic.ContainsKey(panelName))
            return _panelDic[panelName] as T;
        return null;
    }
}
