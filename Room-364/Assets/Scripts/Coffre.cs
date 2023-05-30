
using UnityEngine;

public class Coffre : MonoBehaviour
{
    public Transform door;
    public float openAngle = 90f;
    public float smooth = 2f;

    private Quaternion startRotation;
    private Quaternion endRotation;
    private bool open = false;

    // Start is called before the first frame update
    void Start()
    {
        startRotation = door.rotation;
        endRotation = door.rotation * Quaternion.Euler(0, openAngle, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (open)
        {
            door.rotation = Quaternion.Lerp(door.rotation, endRotation, Time.deltaTime * smooth);
        }
        else
        {
            door.rotation = Quaternion.Lerp(door.rotation, startRotation, Time.deltaTime * smooth);
        }
    }

    public void ToggleDoor()
    {
        open = !open;
    }
    
}
