
using UnityEngine;
/// <summary>
/// Rotation Objet
/// </summary>
public class MovementObject3 : MonoBehaviour
{
    public float rotationSpeed = 200f; // Vitesse de rotation

    private void Update()
    {
        // Calcul de la rotation en fonction de la vitesse et du temps
        float rotationAmount = rotationSpeed * Time.deltaTime;

        // Appliquer la rotation autour de l'axe Y
        transform.Rotate(0f, rotationAmount, 0f);
    }
}
