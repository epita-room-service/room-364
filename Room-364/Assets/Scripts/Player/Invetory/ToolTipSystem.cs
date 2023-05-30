
using UnityEngine;

public class ToolTipSystem : MonoBehaviour
{
    [SerializeField] private ToolTip toolTip;

    public void Show(string content, string header = "")
    {
        toolTip.SetText(content,header);
        toolTip.gameObject.SetActive(true);
    }
    public void Hide()
    {
        toolTip.gameObject.SetActive(false);
    }
}
