
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using UnityEngine;

public class GameManager : MonoBehaviourPun
{
    public GameObject playerPrefab;
    public Transform spawnpoint1;
    public Transform spawnpoint2;
    
    void Start()
    {
        Transform sp = PhotonNetwork.LocalPlayer.GetPlayerNumber() == 0 ? spawnpoint1 : spawnpoint2;

        PhotonNetwork.AutomaticallySyncScene = true;

        Quaternion rotation = Quaternion.Euler(0f, 270f, 0f);
        PhotonNetwork.Instantiate(playerPrefab.name, sp.position, rotation, 0);
    }
}
