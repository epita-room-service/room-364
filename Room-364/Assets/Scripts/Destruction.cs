
using System.Collections;
using UnityEngine;

public class Destruction : MonoBehaviour
{
    public float delay = 5f;
    void DestructionObject ()
    {
        Rigidbody[] objerct = GetComponentsInChildren<Rigidbody>();
        foreach (var i in objerct)
        {
            i.isKinematic = false;
        }
    }

    private void OnMouseDown()
    {
        if (isDoorOpen == false)
        {
            DestructionObject();
            Invoke(nameof(Disappear), delay);
        }
    }

    private void Disappear()
    {
        Destroy(gameObject);
    }
    public float closeDelay = 5f;
    public bool isDoorOpen = false;

    private Coroutine closeCoroutine;

    public void OpenDoor()
    {
        // Ouvrir la porte
        isDoorOpen = true;

        // Annuler la coroutine de fermeture si elle est en cours
        if (closeCoroutine != null)
        {
            StopCoroutine(closeCoroutine);
        }

        // Lancer la coroutine de fermeture avec un délai de 5 secondes
        closeCoroutine = StartCoroutine(CloseDoorAfterDelay());
    }

    private IEnumerator CloseDoorAfterDelay()
    {
        yield return new WaitForSeconds(closeDelay);

        // Fermer la porte
        isDoorOpen = false;

        closeCoroutine = null;
    }
}
