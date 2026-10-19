using TMPro;
using UnityEngine.UI;

/// <summary>
/// 提示面板
/// </summary>
public class TipPanel : BasePanel
{
    //提示文字
    public TextMeshProUGUI tipText;
    //确定按钮
    public Button btnSure;

    /// <summary>
    /// 绑定各组件的回调
    /// </summary>
    public override void Init()
    {
        btnSure.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<TipPanel>();
        });
    }
    
    /// <summary>
    /// 更改提示文字
    /// </summary>
    /// <param name="text">提示文字</param>
    public void ChangeValue(string text)
    {
        tipText.text = text;
    }
}
