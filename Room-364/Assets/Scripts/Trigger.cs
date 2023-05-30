using UnityEngine;
using UnityEngine.SceneManagement;

public class Trigger : MonoBehaviour {
    public string sceneName;

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            SceneManager.LoadScene(sceneName);
        }
    }
}