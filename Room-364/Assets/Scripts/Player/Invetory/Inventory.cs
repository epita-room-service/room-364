
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Equipment equipment;

    [SerializeField] private ItemsActionsSystem itemsActionsSystem;
    
    [SerializeField] private ToolTipSystem toolTipSystem;
    
    [Header("Controls Assignment")]
    [SerializeField] private KeyCode inventoryKey = KeyCode.I;
    
    [SerializeField] private KeyCode flashLightKey = KeyCode.F;
    
    [Header("Inventory References")]
    [SerializeField] private List<ItemData> content = new List<ItemData>();

    [SerializeField] private GameObject inventoryPanel;

    [SerializeField] private Transform inventorySlotParent;

    private const int InventorySize = 24;

    [SerializeField] private GameObject flashLightSpot;
    [SerializeField] private GameObject uvFlashLightSpot;

    [SerializeField] private GameObject sounds;
    [SerializeField] private AudioClip flashlightSound;

    public Sprite emptySlotVisual;

    public bool isOpen = false;

    public void Start()
    {
        CloseInventory();
        RefreshContent();
    }

    public void Update()
    {
        if (Input.GetKeyDown(inventoryKey))
        {
            if (isOpen)
            {
                CloseInventory();
            }
            else
            {
                OpenInventory();
            }
        }
        if (Input.GetKeyDown(flashLightKey))
        {
            AudioSource audioSource = sounds.GetComponent<AudioSource>();
            audioSource.PlayOneShot(flashlightSound);
            flashLightSpot.SetActive(!flashLightSpot.activeSelf);
            uvFlashLightSpot.SetActive(!uvFlashLightSpot.activeSelf);
        }
    }

    private void OpenInventory()
    {
        isOpen = true;
        inventoryPanel.SetActive(true);
    }

    public void CloseInventory()
    {
        isOpen = false;
        inventoryPanel.SetActive(false);
        itemsActionsSystem.actionPanel.SetActive(false);
        toolTipSystem.Hide();
    }
    
    public void AddItem(ItemData item)
    {
        content.Add(item);
        RefreshContent();
    }
    public void RemoveItem(ItemData item)
    {
        content.Remove(item);
        RefreshContent();
    }

    public void RefreshContent()
    {
        // on vide l'inventaire
        int length = inventorySlotParent.childCount;
        for (int i = 0; i < length; i++)
        {
            Slot currentSlot = inventorySlotParent.GetChild(i).GetComponent<Slot>();
            currentSlot.item = null;
            currentSlot.itemVisual.sprite = emptySlotVisual;
        }
        
        // on le remplis du contenu
        length = content.Count;
        for (int i = 0; i < length; i++)
        {
            Slot currentSlot = inventorySlotParent.GetChild(i).GetComponent<Slot>();
            currentSlot.item = content[i];
            currentSlot.itemVisual.sprite = content[i].visual;
        }
        
        equipment.UpdateEquipmentsDesequipButtons();
    }

    public bool IsFull()
    {
        return InventorySize == content.Count;
    }
}
