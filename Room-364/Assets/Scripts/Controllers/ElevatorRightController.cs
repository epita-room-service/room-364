
using UnityEngine;
using Photon.Pun;

public class ElevatorRightController : MonoBehaviourPun // NetworkBehaviour
{
    private Animator elevatorRightAnim;

    public AudioClip sound;
    private void Awake()
    {
        elevatorRightAnim = gameObject.GetComponent<Animator>();
    }
    
    public void PlayOpenAnimation()
    {
        photonView.RPC("CmdPlayOpenAnimation",RpcTarget.All);
    }
    public void PlayCloseAnimation()
    {
        photonView.RPC("CmdPlayCloseAnimation",RpcTarget.All);
    }

    [PunRPC]
    private void CmdPlayOpenAnimation()
    {
        elevatorRightAnim.Play("ElevatorRightOpen", 0, 0.0f);
    }
    [PunRPC]
    private void CmdPlayCloseAnimation()
    {
        elevatorRightAnim.Play("ElevatorRightClose", 0, 0.0f);
    }

    public void PlaySound()
    {
        photonView.RPC("CmdPlaySound",RpcTarget.All);
    }
    
    [PunRPC]
    private void CmdPlaySound()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(sound);
    }
}
