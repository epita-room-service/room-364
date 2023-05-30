
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    [Header("Controls Assignment")]
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode sneakKey = KeyCode.C;
    
    [Header("Speed References")]
    [SerializeField] private float speed = 2f;
    [SerializeField] private float sneakSpeed = 1f;
    [SerializeField] private float sprintSpeed = 3f;

    [Header("Sensitivity References")]
    [SerializeField] private float mouseSensitivityX = 3f;
    [SerializeField] private float mouseSensitivityY = 3f;
    
    private PlayerMotor motor;

    [Header("Camera References")]
    [SerializeField] private Camera cam;
    
    [SerializeField] private float playerHeightSneak = -0.5f;
    private float cameraHeight;
    private bool sneak;
    
    [Header("Inventory References")]
    [SerializeField] private Inventory inventory;
    [SerializeField] private SceneInteractions sceneInteractions;

    private void Start()
    {
        cameraHeight = cam.transform.position.y;
        motor = GetComponent<PlayerMotor>();
    }

    private void Update()
    {
        // sneak

        if (Input.GetKeyDown(sneakKey))
        {
            sneak = true;
            var camTransform = cam.transform;
            Vector3 camPosition = camTransform.position;
            camTransform.position = new Vector3(camPosition.x, cameraHeight + playerHeightSneak, camPosition.z);
        }

        if (Input.GetKeyUp(sneakKey))
        {
            sneak = false;
            var camTransform = cam.transform;
            Vector3 camPosition = camTransform.position;
            camTransform.position = new Vector3(camPosition.x, cameraHeight, camPosition.z);
        }
    }

    private void FixedUpdate()
    {
        // calcule de la velocité
        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");

        var transform1 = transform;
        Vector3 moveHorizontal = transform1.right * xMov;
        Vector3 moveVertical = transform1.forward * zMov;

        Vector3 velocity = (moveHorizontal + moveVertical).normalized * speed;

        // on verifie si le joueur veut courrir
        if (Input.GetKey(sprintKey) && !sneak)
        {
            velocity = (moveHorizontal + moveVertical).normalized * sprintSpeed;
        }

        if (sneak)
        {
            velocity = (moveHorizontal + moveVertical).normalized * sneakSpeed;
        }

        if (sceneInteractions.keyCodePanelIsOpen)
        {
            velocity = new Vector3(0, 0, 0);
        }
        
        motor.Move(velocity);

        Vector3 rotation;
        float cameraRotationX;

        if (!CanMoveCam())
        {
            // on calcule la rotation du joueur en un vecteur3
            float yRot = Input.GetAxisRaw("Mouse X");

            rotation = new Vector3(0, yRot, 0) * mouseSensitivityX;

            // on calcule la rotation de la camera en un vecteur3
            float xRot = Input.GetAxisRaw("Mouse Y");

            cameraRotationX = xRot * mouseSensitivityY;
            
        }
        else
        {
            rotation = new Vector3(0, 0, 0);
            cameraRotationX = 0;
        }
        motor.Rotate(rotation);
        motor.RotateCamera(cameraRotationX);
    }

    private bool CanMoveCam()
    {
        return inventory.isOpen || sceneInteractions.keyCodePanelIsOpen;
    }
}