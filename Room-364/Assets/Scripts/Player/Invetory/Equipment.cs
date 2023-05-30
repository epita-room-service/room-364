
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Equipment : MonoBehaviour
{
    [SerializeField] private ItemsActionsSystem itemsActionsSystem;

    [SerializeField] private Inventory inventory;
    
    [SerializeField] private ToolTipSystem toolTipSystem;
    
    [Header("Equipment Panel References")]
    [SerializeField] private EquipmentLibrary equipmentLibrary;

    [SerializeField] private Image handSlotImage;

    private ItemData equipedHandItem;

    [SerializeField] private Button handSlotDesequipButton;

    private void DisablePreviousEquipedEquipment(ItemData itemToDisable)
    {
        if (itemToDisable == null) return;
        
        EquipmentLibraryItem equipmentLibraryItem = equipmentLibrary.content.First(elem => elem.itemData == itemToDisable);

        if (equipmentLibraryItem != null)
        {
            equipmentLibraryItem.itemPrefab.SetActive(false);
        }
        
        inventory.AddItem(itemToDisable);
    }
    
    public void DesequipEquipment()
    {
        if (inventory.IsFull())
        {
            Debug.Log("L'inventaire des full");
            return;
        }

        ItemData currentItem = null;

        currentItem = equipedHandItem;
        equipedHandItem = null;
        handSlotImage.sprite = inventory.emptySlotVisual;
        
        EquipmentLibraryItem equipmentLibraryItem = equipmentLibrary.content.First(elem => elem.itemData == currentItem);

        if (equipmentLibraryItem != null)
        {
            equipmentLibraryItem.itemPrefab.SetActive(false);
        }
        
        inventory.AddItem(currentItem);
        inventory.RefreshContent();
    }
    
    public void UpdateEquipmentsDesequipButtons()
    {
        handSlotDesequipButton.gameObject.SetActive(equipedHandItem);
    }
    
    public void EquipAction()
    {
        EquipmentLibraryItem equipmentLibraryItem = equipmentLibrary.content.First(elem => elem.itemData == itemsActionsSystem.itemCurrentlySelected);

        if (equipmentLibraryItem != null)
        {
            DisablePreviousEquipedEquipment(equipedHandItem);
            handSlotImage.sprite = itemsActionsSystem.itemCurrentlySelected.visual;
            equipedHandItem = itemsActionsSystem.itemCurrentlySelected;

            equipmentLibraryItem.itemPrefab.SetActive(true);
            if (itemsActionsSystem.itemCurrentlySelected.name == "Flashlight") equipmentLibraryItem.itemPrefab.transform.GetChild(1).gameObject.SetActive(false);
            if (itemsActionsSystem.itemCurrentlySelected.name == "UV flashlight") equipmentLibraryItem.itemPrefab.transform.GetChild(0).gameObject.SetActive(false);

            inventory.RemoveItem(itemsActionsSystem.itemCurrentlySelected);
            itemsActionsSystem.CloseActionPanel();
            toolTipSystem.Hide();
        }
    }
}
