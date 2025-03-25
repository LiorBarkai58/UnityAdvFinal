using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Character References")]
    [SerializeField] private Rigidbody Rb;

    [SerializeField] private CinemachineCamera followCamera;

    [Header("Character Data")]

    [SerializeField] private float movementSpeed = 5;
    private Vector3 _InputDirection;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 CameraForward = followCamera.transform.forward;
        Vector3 CameraRight = followCamera.transform.right;

        // Remove vertical influence
        CameraForward.y = 0;
        CameraRight.y = 0;

        // Normalize to prevent slow diagonal movement
        CameraForward.Normalize();
        CameraRight.Normalize();

        // Corrected movement direction (z instead of y)
        Rb.linearVelocity = (_InputDirection.x * CameraRight + _InputDirection.z * CameraForward) * movementSpeed;
    }

    public void OnMove(InputAction.CallbackContext context){
        if(context.performed){
            Vector2 inputValue = context.ReadValue<Vector2>();
            _InputDirection = new Vector3(inputValue.x,0, inputValue.y);
        }
        else if(context.canceled){
            Vector2 inputValue = context.ReadValue<Vector2>();
            _InputDirection = new Vector3(inputValue.x,0, inputValue.y);
        }
    }
}
