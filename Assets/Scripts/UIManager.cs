
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIManager
{
    //Dictionary字典用于存储面板，显示/隐藏/得到面板，Canvas父对象
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
    
    private Dictionary<string,BasePanel> _panelDic = new Dictionary<string, BasePanel>();
    
    private Transform _canvasTrans;
    
    public UIManager()
    {
        GameObject canvas = GameObject.Instantiate(Resources.Load<GameObject>("UI/Canvas"));
        _canvasTrans = canvas.transform;
        GameObject.DontDestroyOnLoad(canvas);
    }
    
    public T ShowPanel<T>() where T : BasePanel
    {
        string panelName = typeof(T).Name;
        if (_panelDic.ContainsKey(panelName))
            return _panelDic[panelName] as T;
        
        GameObject panelObj = GameObject.Instantiate(Resources.Load<GameObject>("UI/" + panelName));
        T panel = panelObj.GetComponent<T>();
        panel.ShowMe();
        panel.transform.SetParent(_canvasTrans);
        _panelDic.Add(panelName, panel);
        
        return panel;
    }

    /// <summary>
    /// 隐藏面板
    /// </summary>
    /// <param name="isFade">是否等待渐变</param>
    /// <typeparam name="T"></typeparam>
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
                    GameObject.Destroy(panel.gameObject);
                    _panelDic.Remove(panelName);
                });
            }
            else
            {
                GameObject.Destroy(panel.gameObject);    
                _panelDic.Remove(panelName);
            }
        }   
    }

    public T GetPanel<T>() where T : BasePanel
    {
        string panelName = typeof(T).Name;
        if(_panelDic.ContainsKey(panelName))
            return _panelDic[panelName] as T;
        return null;
    }
}
