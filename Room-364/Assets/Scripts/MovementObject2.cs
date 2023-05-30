
using UnityEngine;
/// <summary>
/// Mouvement de va-et-vient
/// </summary>
public class MovementObject2 : MonoBehaviour
{
    public float moveDistance = 5f; // Distance de déplacement
    public float speed = 2f; // Vitesse de déplacement

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        // Calcul de la nouvelle position en utilisant Mathf.PingPong pour effectuer le va-et-vient
        float newPositionX = startPosition.x + Mathf.PingPong(Time.time * speed, moveDistance);

        // Mise à jour de la position de l'objet
        transform.position = new Vector3(newPositionX, transform.position.y, transform.position.z);
    }
}
