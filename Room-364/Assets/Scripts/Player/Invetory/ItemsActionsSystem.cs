
using Photon.Pun;
using Unity.Mathematics;
using UnityEngine;

public class ItemsActionsSystem : MonoBehaviour
{
    [SerializeField] private Equipment equipment;
    [SerializeField] private SceneInteractions sceneInteractions;
    [SerializeField] private Inventory inventory;
    
    public GameObject actionPanel;
    
    [SerializeField] private Transform dropPoint;
    
    [SerializeField] private GameObject useItemButton;
    [SerializeField] private GameObject equipItemButton;
    [SerializeField] private GameObject dropItemButton;

    [HideInInspector] public ItemData itemCurrentlySelected;
    
    private DoorController _doorController;
    private LocalDoorController _localDoorController;
    private LockerController _lockerDoorController;
    private ElecController _elecController;
    private CardReaderController _cardReaderController;
    
    public void OpenActionPanel(ItemData item, Vector3 slotPosition)
    {
        itemCurrentlySelected = item;

        if (item == null)
        {
            actionPanel.SetActive(false);
            return;
        }
        
        switch (item.itemType)
        {
            case ItemType.Consumable:
                useItemButton.SetActive(true);
                equipItemButton.SetActive(false);
                break;
            case ItemType.NotConsumable:
                useItemButton.SetActive(true);
                equipItemButton.SetActive(false);
                break;
            case ItemType.Equipment:
                useItemButton.SetActive(false);
                equipItemButton.SetActive(true);
                break;
        }

        actionPanel.transform.position = slotPosition;
        actionPanel.SetActive(true);
    }

    public void CloseActionPanel()
    {
        actionPanel.SetActive(false);
        itemCurrentlySelected = null;
    }

    public void UseActionButton()
    {
        RaycastHit hit;
        // Ouverture de porte grace a clé
        if (Physics.Raycast(sceneInteractions.transform.position, sceneInteractions.transform.forward, out hit, sceneInteractions.interactRange, sceneInteractions.layerDoorMask))
        {
            if (hit.transform.CompareTag("Door"))
            {
                _doorController = hit.transform.GetComponent<DoorController>();
                if (_doorController.doorLock)
                {
                    if (itemCurrentlySelected == _doorController.Key)
                    {
                        _doorController.Unlock();
                        sceneInteractions.doorLockRedondance = false;
                        sceneInteractions.openText.SetActive(true);
                        sceneInteractions.lockDoorsText.SetActive(false);
                        inventory.RemoveItem(itemCurrentlySelected);
                    }
                    if (itemCurrentlySelected == _doorController.newKey)
                    {
                        _doorController.Unlock();
                        sceneInteractions.doorLockRedondance = false;
                        sceneInteractions.lockDoorsText.SetActive(false);
                        inventory.RemoveItem(itemCurrentlySelected);
                        _doorController.DestroyDoor();
                    }
                }
            }
        }
        // Utiliser le fusible sur le dijoncteur pour ouvrir l'ascenseur
        /*if (expr)
        {
            
        }*/
        // Utiliser la hache pour détruire la porte 360
        /*if (expr)
        {
            Destroy
        }*/
        // Utiliser la carte pour changer de scène
        /*if (expr)
        {
            
        }*/
        if (Physics.Raycast(sceneInteractions.transform.position, sceneInteractions.transform.forward, out hit, sceneInteractions.interactRange, sceneInteractions.layerLocalDoorMask))
        {
            if (hit.transform.CompareTag("LocalDoor"))
            {
                _localDoorController = hit.transform.GetComponent<LocalDoorController>();
                if (_localDoorController.doorLock)
                {
                    if (itemCurrentlySelected == _localDoorController.Key)
                    {
                        _localDoorController.Unlock();
                        sceneInteractions.localDoorLockRedondance = false;
                        sceneInteractions.openText.SetActive(true);
                        sceneInteractions.lockDoorsText.SetActive(false);
                    }
                }
            }
        }
        if (Physics.Raycast(sceneInteractions.transform.position, sceneInteractions.transform.forward, out hit, sceneInteractions.interactRange, sceneInteractions.layerLockerMask))
        {
            if (hit.transform.CompareTag("Locker"))
            {
                _lockerDoorController = hit.transform.GetComponent<LockerController>();
                if (_lockerDoorController.doorLock)
                {
                    if (itemCurrentlySelected == _lockerDoorController.Key)
                    {
                        _lockerDoorController.Unlock();
                        sceneInteractions.lockerLockRedondance = false;
                        sceneInteractions.openText.SetActive(true);
                        sceneInteractions.lockDoorsText.SetActive(false);
                        inventory.RemoveItem(itemCurrentlySelected);
                    }
                }
            }
        }
        if (Physics.Raycast(sceneInteractions.transform.position, sceneInteractions.transform.forward, out hit, sceneInteractions.interactRange, sceneInteractions.layerElecMask))
        {
            if (hit.transform.CompareTag("Elec"))
            {
                _elecController = hit.transform.GetComponent<ElecController>();
                if (!_elecController.activate)
                {
                    if (itemCurrentlySelected == _elecController.Key)
                    {
                        _elecController.activate = true;
                        sceneInteractions.needFuzeText.SetActive(false);
                        _elecController.elevatorLeftController.PlayOpenAnimation();
                        _elecController.elevatorRightController.PlayOpenAnimation();
                        _elecController.elevatorRightController.PlaySound();
                        inventory.RemoveItem(itemCurrentlySelected);
                    }
                }
            }
        }
        if (Physics.Raycast(sceneInteractions.transform.position, sceneInteractions.transform.forward, out hit, sceneInteractions.interactRange, sceneInteractions.layerCardReaderMask))
        {
            if (hit.transform.CompareTag("CardReader"))
            {
                _cardReaderController = hit.transform.GetComponent<CardReaderController>();
                if (!_cardReaderController.CardActive)
                {
                    if (itemCurrentlySelected == _cardReaderController.Card)
                    {
                        _cardReaderController.CardActive = true;
                        sceneInteractions.needCardText.SetActive(false);
                        StartCoroutine(sceneInteractions.Wait());
                    }
                }
            }
        }
        CloseActionPanel();
    }
    
    public void DropActionButton()
    {
        PhotonNetwork.Instantiate(itemCurrentlySelected.name,dropPoint.position,quaternion.identity);
        inventory.RemoveItem(itemCurrentlySelected);
        inventory.RefreshContent();
        CloseActionPanel();
    }

    public void EquipActionButton()
    {
        equipment.EquipAction();
    }
}
