
using UnityEngine;
/// <summary>
/// Je pense qu'il faudra faire une fusion entre le script Porte360 et Destruction
/// </summary>
public class Porte360 : MonoBehaviour
{
    
    private Animator doorAnim;

    [HideInInspector] public bool doorOpen = false;// Indique si la porte est ouverte
    public float closeDelay = 3f; // Délai de fermeture de la porte en secondes
    private float timer; // Compteur de temps

    public bool doorLock;

    [SerializeField] private AudioClip openSound;
    [SerializeField] private AudioClip closeSound;
    private void Awake()
    {
        doorAnim = gameObject.GetComponent<Animator>();
    }
    
    public void PlayAnimation()
    {
        if (doorOpen)
        {
            // Ajoute du temps dans le compteur de temps
            timer += Time.deltaTime;

            // Vérifie si le délai de fermeture a été atteint
            if (timer >= closeDelay)
            {
                CloseDoor();
            }
            
            doorAnim.Play("DoorClose", 0, 0.0f);
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.PlayOneShot(closeSound);
            doorOpen = false;
        }
        else
        {
            doorAnim.Play("DoorOpen", 0, 0.0f);
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.PlayOneShot(openSound);
            doorOpen = true;
        }
    }
    private void CloseDoor()
    {
        // fermer la porte
        doorOpen = false;
    }
    
}

