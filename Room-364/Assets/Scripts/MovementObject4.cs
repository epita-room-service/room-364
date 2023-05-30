
using UnityEngine;
 /// <summary>
 /// Montée et descente
 /// </summary>
public class MovementObject4 : MonoBehaviour
{
    public float verticalSpeed = 1f; // Vitesse de déplacement vertical
    public float verticalAmplitude = 1f; // Amplitude verticale du mouvement
    public float verticalOffset = 1f; // Décalage vertical initial

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        // Calcul de la position verticale en fonction du temps
        float verticalPosition = startPosition.y + Mathf.Sin(Time.time * verticalSpeed) * verticalAmplitude + verticalOffset;

        // Mise à jour de la position de l'objet
        transform.position = new Vector3(transform.position.x, verticalPosition, transform.position.z);
    }
}
