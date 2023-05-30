
using Photon.Pun;
using UnityEngine;

public class ElevatorLeftController : MonoBehaviourPun // NetworkBehaviour
{
    private Animator elevatorLeftAnim;
    private void Awake()
    {
        elevatorLeftAnim = gameObject.GetComponent<Animator>();
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
        elevatorLeftAnim.Play("ElevatorLeftOpen", 0, 0.0f);
    }
    [PunRPC]
    private void CmdPlayCloseAnimation()
    {
        elevatorLeftAnim.Play("ElevatorLeftClose", 0, 0.0f);
    }
}
