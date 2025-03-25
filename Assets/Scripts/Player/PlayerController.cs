using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Character References")]
    [SerializeField] private Rigidbody Rb;

    [SerializeField] private CinemachineCamera followCamera;

    [SerializeField] private Transform Visuals;

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

        // Ignore vertical influence
        CameraForward.y = 0;
        CameraRight.y = 0;

        // Normalize vectors
        CameraForward.Normalize();
        CameraRight.Normalize();

        // Calculate movement direction
        Vector3 movement = (_InputDirection.x * CameraRight + _InputDirection.z * CameraForward) * movementSpeed;
        Rb.linearVelocity = movement;

        if (movement.sqrMagnitude > 0.01f) 
        {
            Vector3 lookDirection = new Vector3(movement.x, 0, movement.z);
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            Visuals.rotation = Quaternion.Slerp(Visuals.rotation, targetRotation, Time.deltaTime * 10f);
        }
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
