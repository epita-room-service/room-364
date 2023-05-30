using UnityEngine;
using Photon.Pun;

public class MenuPause : MonoBehaviourPunCallbacks
{
    private bool isPaused = false;

    private void Update()
    {
        if (photonView.IsMine && Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    private void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            // Mettre le jeu en pause (pauser la logique du jeu, désactiver les mouvements, etc.)
            Time.timeScale = 0f;
        }
        else
        {
            // Reprendre le jeu (réactiver la logique du jeu, activer les mouvements, etc.)
            Time.timeScale = 1f;
        }

        // Synchroniser l'état de pause avec les autres joueurs
        photonView.RPC("SyncPauseState", RpcTarget.Others, isPaused);
    }

    [PunRPC]
    private void SyncPauseState(bool pauseState)
    {
        isPaused = pauseState;

        if (isPaused)
        {
            // Mettre le jeu en pause (pauser la logique du jeu, désactiver les mouvements, etc.)
            Time.timeScale = 0f;
        }
        else
        {
            // Reprendre le jeu (réactiver la logique du jeu, activer les mouvements, etc.)
            Time.timeScale = 1f;
        }
    }
}
