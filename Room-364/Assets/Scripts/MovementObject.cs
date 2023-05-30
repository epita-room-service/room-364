
using UnityEngine;
/// <summary>
/// Tourne autour d'un périmètre
/// </summary>
public class MovementObject : MonoBehaviour
{
    public Transform centerPoint; // Point central autour duquel l'objet se déplace
    public float radius = 0.001f; // Rayon du mouvement
    public float speed = 1f; // Vitesse de rotation

    private float angle = 0f;

    private void Update()
    {
        // Calcul de la nouvelle position en utilisant les fonctions trigonométriques
        float x = centerPoint.position.x + Mathf.Cos(angle) * radius;
        float z = centerPoint.position.z + Mathf.Sin(angle) * radius;

        // Mise à jour de la position de l'objet
        transform.position = new Vector3(x, transform.position.y, z);

        // Incrémentation de l'angle en fonction de la vitesse
        angle += speed * Time.deltaTime;

        // Vérification de l'angle pour éviter les valeurs trop grandes
        if (angle >= 360f)
        {
            angle -= 360f;
        }
    }
}
