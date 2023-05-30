
using TMPro;
using UnityEngine;

// je te laisse l'implémenter Maël merci d'avance
public class Keypad : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TextMeshProUGUI ans;
    private string answer = "1986";
    public void Number(int nb)
    {
        ans.text += nb.ToString();
    }

    public void Execute()
    {
        if (ans.text == answer)
        {
            ans.text= "CORRECT";
            //playerSetup.AnimSafeChest(this);
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
        
    }
    
    public GameObject interfaceObject; // L'interface à ouvrir

    private bool isInterfaceOpen = false; // Indique si l'interface est ouverte ou fermée

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OpenInterface();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CloseInterface();
        }
    }

    private void OpenInterface()
    {
        if (!isInterfaceOpen)
        {
            interfaceObject.SetActive(true);
            isInterfaceOpen = true;
        }
    }

    private void CloseInterface()
    {
        if (isInterfaceOpen)
        {
            interfaceObject.SetActive(false);
            isInterfaceOpen = false;
        }
    }
    
    //-------------------------------------------------------------------------//
    
    [SerializeField] private PlayerSetup playerSetup;
    
    private Animator safeChestAnim;

    [HideInInspector] public bool safeChestOpen = false;

    [HideInInspector] public bool locked = true;

    [SerializeField] private AudioClip openSound;
    [SerializeField] private AudioClip closeSound;
    
    private void Awake()
    {
        safeChestAnim = gameObject.GetComponent<Animator>();
    }

    public void PlayAnimation()
    {
        if (!safeChestOpen)
        {
            safeChestAnim.Play("DoorLockerOpen", 0, 0.0f);
            //AudioSource audioSource = GetComponent<AudioSource>();
            //audioSource.PlayOneShot(openSound);
            safeChestOpen = true;
        }
    }
}
