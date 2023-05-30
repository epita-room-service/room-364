
using System.Collections;
using Photon.Pun;
using UnityEngine;

public class InteractionsBehaviour : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    
    public void DoPickup(Item item)
    {
        // verification de full
        if (inventory.IsFull())
        {
            Debug.Log("Inventory full : can't pick up: " + item.name);
            return;
        }
        
        // Ajouter l'objet
        inventory.AddItem(item.itemData);

        if (!item.gameObject.GetComponent<PhotonView>().IsMine)
        {
            item.gameObject.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer.ActorNumber);
        }

        StartCoroutine(Wait(item));
    }

    private IEnumerator Wait(Item item)
    {
        yield return new WaitForSeconds(0.11f);
        PhotonNetwork.Destroy(item.gameObject);
    }
}
