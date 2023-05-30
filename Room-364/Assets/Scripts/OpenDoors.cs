
using System.Collections;
using UnityEngine;


public class OpenDoors : MonoBehaviour
{
    public ElevatorLeftController elevatorLeftController;
    public ElevatorRightController elevatorRightController;

    private bool open;

    public AudioClip sound;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!open)
        {
            if (other.CompareTag("Player"))
            {
                open = true;
                StartCoroutine(WaitAndDoSomething(other));
            }
        }
    }
    
    private IEnumerator WaitAndDoSomething(Collider other)
    {
        yield return new WaitForSeconds(3f);
        PlayerSetup playerSetup = other.gameObject.GetComponent<PlayerSetup>();
        elevatorLeftController.PlayOpenAnimation();
        elevatorRightController.PlayOpenAnimation();
        elevatorRightController.PlaySound();
        //playerSetup.OpenElevator(elevatorLeftController,elevatorRightController);
        //playerSetup.PlayElevatorSound(elevatorRightController,sound);
    }
}
