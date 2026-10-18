using TMPro;
using UnityEngine.UI;

public class TipPanel : BasePanel
{
    public TextMeshProUGUI tipText;
    public Button btnSure;

    public override void Init()
    {
        btnSure.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<TipPanel>();
        });
    }

    public void ChangeValue(string text)
    {
        tipText.text = text;
    }
}
