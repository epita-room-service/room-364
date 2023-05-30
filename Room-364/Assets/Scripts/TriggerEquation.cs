using UnityEngine;

public class TriggerEquation : MonoBehaviour
{
    public Material equationSimple;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Renderer renderer = GetComponent<Renderer>();

            // Change the material to the new material
            renderer.material = equationSimple;
        }
    }
}