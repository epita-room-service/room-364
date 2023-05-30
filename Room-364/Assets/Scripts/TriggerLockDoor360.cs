
using Photon.Pun;
using UnityEngine;

public class TriggerLockDoor360 : MonoBehaviourPun
{
    [SerializeField] private GameObject porte;
    
    private bool locked;

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            if (!locked)
            {
                photonView.RPC("CmdLock",RpcTarget.All);
            }
        }
    }

    [PunRPC]
    private void CmdLock()
    {
        locked = true;
        porte.GetComponent<DoorController>().LockDoor();
    }
}
