using SSWC.Common.UI;
using SSWC.Features.BeginScene.UI;
using UnityEngine;

namespace SSWC.Features.BeginScene
{
    /// <summary>
    /// 主入口，用于初始化
    /// </summary>
    public class Main : MonoBehaviour
    {
        /// <summary>
        /// 游戏开始时显示开始面板
        /// </summary>
        void Start()
        {
            UIManager.Instance.ShowPanel<StartPanel>();
        }
    }
}
