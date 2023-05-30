
using UnityEngine;
using Photon.Pun;

public class LocalDoorController : MonoBehaviourPun // NetworkBehaviour
{
    
    private Animator localDoorAnim;

    [HideInInspector] public bool localDoorOpen = false;
    
    public ItemData Key;

    [HideInInspector] public bool doorLock = true;

    [SerializeField] private AudioClip openSound;
    [SerializeField] private AudioClip closeSound;
    
    private void Awake()
    {
        localDoorAnim = gameObject.GetComponent<Animator>();
    }
    
    public void PlayAnimation()
    {
        if (localDoorOpen) photonView.RPC("CmdPlayCloseAnimation",RpcTarget.All);
        else photonView.RPC("CmdPlayOpenAnimation",RpcTarget.All);
    }

    [PunRPC]
    public void CmdPlayCloseAnimation()
    {
        localDoorAnim.Play("LocalDoorClose", 0, 0.0f);
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(closeSound);
        localDoorOpen = false;
    }

    [PunRPC]
    private void CmdPlayOpenAnimation()
    {
        localDoorAnim.Play("LocalDoorOpen", 0, 0.0f);
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(openSound);
        localDoorOpen = true;
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
