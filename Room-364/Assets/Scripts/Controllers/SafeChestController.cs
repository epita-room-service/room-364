
using Photon.Pun;
using UnityEngine;

public class SafeChestController : MonoBehaviourPun
{
    private Animator safeChestAnim;

    [HideInInspector] public bool safeChestOpen = false;

    [SerializeField] private AudioClip openSound;
    [SerializeField] private AudioClip closeSound;

    private void Awake()
    {
        safeChestAnim = gameObject.GetComponent<Animator>();
    }

    public void PlayAnimation()
    {
        if (!safeChestOpen) photonView.RPC("CmdPlayOpenAnimation",RpcTarget.All);
    }

    [PunRPC]
    public void CmdPlayOpenAnimation()
    {
        safeChestAnim.Play("SafeChestOpen", 0, 0.0f);
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(openSound);
        safeChestOpen = true;
    }
}
