
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private ToolTipSystem toolTipSystem;
    public ItemData item;
    public Image itemVisual;
    
    [SerializeField] private ItemsActionsSystem itemsActionsSystem;
    
    public void OnPointerEnter(PointerEventData enventData)
    {
        if (item != null)
        {
            toolTipSystem.Show(item.description, item.name);
        }
    }
    public void OnPointerExit(PointerEventData enventData)
    {
        toolTipSystem.Hide();
    }

    public void ClickOnSlot()
    {
        itemsActionsSystem.OpenActionPanel(item, transform.position);
    }
}
