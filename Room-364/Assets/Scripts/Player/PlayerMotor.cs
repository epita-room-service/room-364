using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    private Vector3 velocity;
    private Vector3 rotation;
    private float cameraRotationX = 0f;
    private float currentCameraRotationX = 0f;
    
    private Rigidbody rb;
    [SerializeField] private Camera cam;

    [SerializeField] private float cameraRotationLimitUp = 70f;
    [SerializeField] private float cameraRotationLimitDown = 85f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }
    
    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }
    
    public void RotateCamera(float _cameraRotationX)
    {
        cameraRotationX = _cameraRotationX;
    }

    private void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }

    private void PerformMovement()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
    }

    private void PerformRotation()
    {
        // on calcule la rotation de la camera
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        // le moins c'est pour inverser la cam
        currentCameraRotationX -= cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimitUp, cameraRotationLimitDown);

        // on applique la rotation
        cam.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }
}