
using Photon.Pun;
using UnityEngine;

public class DrawerController : MonoBehaviourPun // NetworkBehaviour
{
    private Animator drawerAnim;

    [HideInInspector] public bool drawerOpen = false;
    private void Awake()
    {
        drawerAnim = gameObject.GetComponent<Animator>();
    }

    public void PlayAnimation()
    {
        if (drawerOpen) photonView.RPC("CmdPlayCloseAnimation",RpcTarget.All);
        else photonView.RPC("CmdPlayOpenAnimation",RpcTarget.All);
    }

    [PunRPC]
    private void CmdPlayOpenAnimation()
    {
        drawerAnim.Play("DrawerOpen", 0, 0.0f);
        drawerOpen = true;
    }

    [PunRPC]
    private void CmdPlayCloseAnimation()
    {
        drawerAnim.Play("DrawerClose", 0, 0.0f);
        drawerOpen = false;
    }
}
