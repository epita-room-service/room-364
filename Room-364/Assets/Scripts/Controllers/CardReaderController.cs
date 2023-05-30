
using UnityEngine;

public class CardReaderController : MonoBehaviour
{
    public ElevatorRightController elevatorRightController;
    public ElevatorLeftController elevatorLeftController;
    
    public ItemData Card;

    [HideInInspector] public bool locked = true;

    [HideInInspector] public bool CardActive = false;
}
