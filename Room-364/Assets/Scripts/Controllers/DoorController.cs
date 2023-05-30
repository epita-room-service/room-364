
using System.Collections;
using UnityEngine;
using Photon.Pun;

public class DoorController : MonoBehaviourPun // NetworkBehaviour
{
    private Animator doorAnim;

    [HideInInspector] public bool doorOpen = false;

    public ItemData Key;
    public ItemData newKey;

    public bool doorLock;

    [SerializeField] private AudioClip openSound;
    [SerializeField] private AudioClip closeSound;
    [SerializeField] private AudioClip ClakSound;
    [SerializeField] private AudioClip breakSound;
    
    [HideInInspector] public bool forceLock;
    
    private void Awake()
    {
        doorAnim = gameObject.GetComponent<Animator>();
    }

    public void PlayAnimation()
    {
        if (forceLock) photonView.RPC("CmdPlayClakAnimation",RpcTarget.All);
        else
        {
            if (doorOpen) photonView.RPC("CmdPlayCloseAnimation",RpcTarget.All);
            else photonView.RPC("CmdPlayOpenAnimation",RpcTarget.All);
        }
    }

    [PunRPC]
    private void CmdPlayClakAnimation()
    {
        doorAnim.Play("ClakDoor", 0, 0.0f);
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(ClakSound);
        doorOpen = false;
    }

    [PunRPC]
    private void CmdPlayCloseAnimation()
    {
        doorAnim.Play("DoorClose", 0, 0.0f);
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(closeSound);
        doorOpen = false;
    }
    
    [PunRPC]
    private void CmdPlayOpenAnimation()
    {
        doorAnim.Play("DoorOpen", 0, 0.0f);
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(openSound);
        doorOpen = true;
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

    public void DestroyDoor()
    {
        photonView.RPC("CmdDestroyDoor",RpcTarget.All);
    }

    [PunRPC]
    private void CmdDestroyDoor()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(breakSound);
        photonView.TransferOwnership(null);
        StartCoroutine(DestroyDoorCR());
    }
    
    private IEnumerator DestroyDoorCR()
    {
        yield return new WaitForSeconds(0.5f);
        PhotonNetwork.Destroy(gameObject);
    }

    public void LockDoor()
    {
        photonView.RPC("CmdLockDoor",RpcTarget.All);
    }

    [PunRPC]
    private void CmdLockDoor()
    {
        forceLock = true;
        doorLock = true;
        Key = newKey;
        PlayAnimation();
    }
}
