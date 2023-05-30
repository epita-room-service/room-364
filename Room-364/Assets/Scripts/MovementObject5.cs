
using UnityEngine;
/// <summary>
/// Fait tourner dans tous les sens l'objet
/// </summary>
public class MovementObject5 : MonoBehaviour
{
    public float rotationSpeed = 100f; // Vitesse de rotation

    private void Update()
    {
        // Calcul de la rotation en fonction de la vitesse et du temps
        float rotationAmount = rotationSpeed * Time.deltaTime;

        // Appliquer la rotation autour des axes X, Y et Z
        Vector3 rotation = new Vector3(rotationAmount, rotationAmount, rotationAmount);
        Quaternion deltaRotation = Quaternion.Euler(rotation);
        transform.rotation *= deltaRotation;
    }
}
