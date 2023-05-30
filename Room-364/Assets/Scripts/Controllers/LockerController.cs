using Photon.Pun;
using UnityEngine;

public class LockerController : MonoBehaviourPun // NetworkBehaviour
{
    
    private Animator lockerDoorAnim;

    [HideInInspector] public bool lockerDoorOpen = false;
    
    public ItemData Key;

    public bool doorLock;

    [SerializeField] private AudioClip openSound;
    [SerializeField] private AudioClip closeSound;
    
    private void Awake()
    {
        lockerDoorAnim = gameObject.GetComponent<Animator>();
    }

    public void PlayAnimation()
    {
        if (!lockerDoorOpen) photonView.RPC("PlayOpenAnimation",RpcTarget.All);
    }

    [PunRPC]
    private void PlayOpenAnimation()
    {
        lockerDoorAnim.Play("DoorLockerOpen", 0, 0.0f);
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(openSound);
        lockerDoorOpen = true;
    }
    
    public void Unlock()
    {
        photonView.RPC("CmdUnlock",RpcTarget.All);
    }

    [PunRPC]
    private void CmdUnlock()
    {
        doorLock = false;
    }
}