
using System.Collections;
using UnityEngine;
using TMPro;

public class SceneInteractions : MonoBehaviour
{
    public PlayerSetup playerSetup;
    
    [Header("Controls Assignment")]
    [SerializeField] private KeyCode useKey = KeyCode.E;
    
    [Header("Items References")]
    [SerializeField] private float pickupRange = 1.1f;

    public float interactRange = 1.5f;

    public InteractionsBehaviour playerPickupBehaviour;
    
    [Header("Layers")]
    [SerializeField] private LayerMask layerItemMask;
    public LayerMask layerDoorMask;
    public LayerMask layerLocalDoorMask;
    [SerializeField] private LayerMask layerDrawerMask;
    public LayerMask layerLockerMask;
    public LayerMask layerElecMask;
    [SerializeField] private LayerMask layerSafeChestMask;
    [SerializeField] private LayerMask layerKeyCodeMask;
    public LayerMask layerCardReaderMask;

    [Header("Texts")]
    [SerializeField] private GameObject pickupText;

    public GameObject openText;
    public GameObject lockDoorsText;
    [SerializeField] private GameObject closeText;

    public GameObject activeElecText;
    public GameObject needFuzeText;

    public GameObject interactText;
    public GameObject needCardText;

    private DoorController _doorController;
    private LocalDoorController _localDoorController;
    private DrawerController _drawerController;
    private LockerController _lockerDoorController;
    private ElecController _elecController;
    private SafeChestController _safeChestController;
    private KeyCodeController _keyCodeController;
    private CardReaderController _cardReaderController;

    [HideInInspector] public bool doorLockRedondance;
    [HideInInspector] public bool localDoorLockRedondance;
    [HideInInspector] public bool lockerLockRedondance;

    [Header("Safe Chest")]
    public GameObject keyCodePanel;

    [HideInInspector] public bool keyCodePanelIsOpen = false;
    
    [SerializeField] private TextMeshProUGUI ans;

    private KeyPad keyPad;
    
    readonly string answerSafeChest = "1986";
    readonly string answerElevator = "230580";

    public string nextLevel;
    public string sceneLevel1;

    void Update()
    {
        RaycastHit hit;

        // Rammasage d'objets
        if (Physics.Raycast(transform.position, transform.forward, out hit, pickupRange, layerItemMask))
        {
            if (hit.transform.CompareTag("Item"))
            {
                pickupText.SetActive(true);
                
                if (Input.GetKeyDown(useKey))
                {
                    playerPickupBehaviour.DoPickup(hit.transform.gameObject.GetComponent<Item>());
                }
            }
        }

        // Ouverture / fermeture des portes
        else if (Physics.Raycast(transform.position, transform.forward, out hit, interactRange, layerDoorMask))
        {
            if (hit.transform.CompareTag("Door"))
            {
                _doorController = hit.transform.GetComponent<DoorController>();

                if (!_doorController.doorOpen && !doorLockRedondance) openText.SetActive(true);
                else if (doorLockRedondance) lockDoorsText.SetActive(true);
                else closeText.SetActive(true);
                
                if (Input.GetKeyDown(useKey))
                {
                    if (_doorController.doorLock)
                    {
                        openText.SetActive(false);
                        lockDoorsText.SetActive(true);
                        doorLockRedondance = true;
                    }
                    else
                    {
                        doorLockRedondance = false;
                        lockDoorsText.SetActive(false);
                        openText.SetActive(false);
                        closeText.SetActive(false);
                        //playerSetup.CmdDoor(_doorController);
                        //playerSetup.AnimDoor(_doorController);
                        _doorController.PlayAnimation();
                    }
                }
            }
        }
        else if (Physics.Raycast(transform.position, transform.forward, out hit, interactRange, layerLocalDoorMask))
        {
            if (hit.transform.CompareTag("LocalDoor"))
            {
                _localDoorController = hit.transform.GetComponent<LocalDoorController>();
                
                if (!_localDoorController.localDoorOpen && !localDoorLockRedondance) openText.SetActive(true);
                else if (localDoorLockRedondance) lockDoorsText.SetActive(true);
                else closeText.SetActive(true);
                
                if (Input.GetKeyDown(useKey))
                {
                    if (_localDoorController.doorLock)
                    {
                        openText.SetActive(false);
                        lockDoorsText.SetActive(true);
                        localDoorLockRedondance = true;
                    }
                    else
                    {
                        localDoorLockRedondance = false;
                        lockDoorsText.SetActive(false);
                        openText.SetActive(false);
                        closeText.SetActive(false);
                        //playerSetup.CmdLocalDoor(_localDoorController);
                        //playerSetup.AnimLocalDoor(_localDoorController);
                        _localDoorController.PlayAnimation();
                    }
                }
            }
        }
        else if (Physics.Raycast(transform.position, transform.forward, out hit, interactRange, layerDrawerMask))
        {
            if (hit.transform.CompareTag("Drawer"))
            {
                _drawerController = hit.transform.GetComponent<DrawerController>();
                if (!_drawerController.drawerOpen) openText.SetActive(true);
                else closeText.SetActive(true);
                
                if (Input.GetKeyDown(useKey))
                {
                    openText.SetActive(false);
                    closeText.SetActive(false);
                    //playerSetup.CmdDrawer(_drawerController);
                    //playerSetup.AnimDrawer(_drawerController);
                    _drawerController.PlayAnimation();
                }
            }
        }
        else if (Physics.Raycast(transform.position, transform.forward, out hit, interactRange, layerLockerMask))
        {
            if (hit.transform.CompareTag("Locker"))
            {
                _lockerDoorController = hit.transform.GetComponent<LockerController>();

                if (!_lockerDoorController.lockerDoorOpen && !lockerLockRedondance) openText.SetActive(true);
                else if (lockerLockRedondance) lockDoorsText.SetActive(true);
                
                if (Input.GetKeyDown(useKey))
                {
                    if (_lockerDoorController.doorLock)
                    {
                        openText.SetActive(false);
                        lockDoorsText.SetActive(true);
                        lockerLockRedondance = true;
                    }
                    else
                    {
                        lockerLockRedondance = false;
                        lockDoorsText.SetActive(false);
                        openText.SetActive(false);
                        closeText.SetActive(false);
                        //playerSetup.CmdDoor(_doorController);
                        _lockerDoorController.PlayAnimation();
                    }
                }
            }
        }
        else if (Physics.Raycast(transform.position, transform.forward, out hit, interactRange, layerElecMask))
        {
            if (hit.transform.CompareTag("Elec"))
            {
                _elecController = hit.transform.GetComponent<ElecController>();
                if (_elecController.activate) activeElecText.SetActive(true);
                else needFuzeText.SetActive(true);
            }
        }
        else if (Physics.Raycast(transform.position, transform.forward, out hit, interactRange, layerSafeChestMask))
        {
            if (hit.transform.CompareTag("SafeChest"))
            {
                _safeChestController = hit.transform.GetComponent<SafeChestController>();
                if (!_safeChestController.safeChestOpen) interactText.SetActive(true);
                
                if (Input.GetKeyDown(useKey))
                {
                    keyPad = KeyPad.SafeChest;
                    Clear();
                    keyCodePanelIsOpen = true;
                    keyCodePanel.SetActive(true);
                }
            }
        }
        else if (Physics.Raycast(transform.position, transform.forward, out hit, interactRange, layerKeyCodeMask))
        {
            if (hit.transform.CompareTag("KeyCode"))
            {
                _keyCodeController = hit.transform.GetComponent<KeyCodeController>();
                if (!_keyCodeController.keyCodeOpenOpen) interactText.SetActive(true);
                
                if (Input.GetKeyDown(useKey))
                {
                    keyPad = KeyPad.Elevator1;
                    Clear();
                    keyCodePanelIsOpen = true;
                    keyCodePanel.SetActive(true);
                }
            }
        }
        else if (Physics.Raycast(transform.position, transform.forward, out hit, interactRange, layerCardReaderMask))
        {
            if (hit.transform.CompareTag("CardReader"))
            {
                keyPad = KeyPad.Elevator2;
                _cardReaderController = hit.transform.GetComponent<CardReaderController>();
                if (!_cardReaderController.CardActive) needCardText.SetActive(true);
            }
        }
        else
        {
            localDoorLockRedondance = false;
            doorLockRedondance = false;
            lockerLockRedondance = false;
            openText.SetActive(false);
            closeText.SetActive(false);
            lockDoorsText.SetActive(false);
            pickupText.SetActive(false);
            needFuzeText.SetActive(false);
            activeElecText.SetActive(false);
            interactText.SetActive(false);
            needCardText.SetActive(false);
        }
    }
    
    public void Number(int nb)
    {
        if (ans.text == "ERROR") Clear();
        ans.text += nb.ToString();
        int length = 0;
        switch (keyPad)
        {
            case KeyPad.SafeChest:
                length = 4;
                break;
            case KeyPad.Elevator1:
                length = 6;
                break;
            case KeyPad.Elevator2:
                length = 6;
                break;
        }
        if (ans.text.Length >= length) Execute();
    }

    public void Execute()
    {
        string code = "0000";
        switch (keyPad)
        {
            case KeyPad.SafeChest:
                code = answerSafeChest;
                break;
            case KeyPad.Elevator1:
                code = answerElevator;
                break;
        }
        if (ans.text == code)
        {
            ans.text= "CORRECT";
            StartCoroutine(Wait());
        }
        else
        {
            ans.text = "ERROR";
        }
    }

    public void Clear()
    {
        ans.text = "";
    }
    
    public void Exit()
    {
        keyCodePanelIsOpen = false;
        keyCodePanel.SetActive(false);
    }

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.3f);
        switch (keyPad)
        {
            case KeyPad.Elevator1:
                _keyCodeController.elevatorRightController.PlayCloseAnimation();
                _keyCodeController.elevatorLeftController.PlayCloseAnimation();
                StartCoroutine(WaitLoadEndScene());
                break;
            case KeyPad.SafeChest:
                _safeChestController.PlayAnimation();
                break;
            case KeyPad.Elevator2:
                _cardReaderController.elevatorRightController.PlayCloseAnimation();
                _cardReaderController.elevatorLeftController.PlayCloseAnimation();
                StartCoroutine(WaitLoadScene());
                break;
        }
        Exit();
    }

    private IEnumerator WaitLoadEndScene()
    {
        yield return new WaitForSeconds(3f);
        playerSetup.End();
    }
    
    private IEnumerator WaitLoadScene()
    {
        yield return new WaitForSeconds(3f);
        playerSetup.Stage1();
    }
}

public enum KeyPad
{
    SafeChest, // Safe chest
    Elevator2, // Elevator floor 2
    Elevator1  // Elevator floor 1
}