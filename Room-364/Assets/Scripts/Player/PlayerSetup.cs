
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class PlayerSetup : MonoBehaviourPun // NetworkBehaviour
{
    [SerializeField] private Behaviour[] componentsToDisable;
    [SerializeField] private GameObject[] partToDisable;
    [SerializeField] private Collider collider;

    private Camera mainCamera;

    private void Start()
    {
        if (photonView != null && !photonView.IsMine)
        {
            // desactivation des script qui ne sont pas ce de mon joueur
            int length = componentsToDisable.Length;
            for (int i = 0; i < length; i++)
            {
                componentsToDisable[i].enabled = false;
            }
            length = partToDisable.Length;
            for (int i = 0; i < length; i++)
            {
                partToDisable[i].SetActive(false);
            }

            collider.enabled = false;
        }
        mainCamera = Camera.main;
        if (mainCamera != null)
        {
            mainCamera.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        if (mainCamera != null)
        {
            mainCamera.gameObject.SetActive(true);
        }
    }

    public void End()
    {
        photonView.RPC("CmdEnd",RpcTarget.All);
    }

    [PunRPC]
    private void CmdEnd()
    {
        SceneManager.LoadScene("Menu 1");
    }
    
    public void Stage1()
    {
        photonView.RPC("CmdStage1",RpcTarget.All);
    }

    [PunRPC]
    private void CmdStage1()
    {
        SceneManager.LoadScene("Etage-1");
    }
}
