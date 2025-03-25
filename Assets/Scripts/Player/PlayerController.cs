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

    [SerializeField] private MovementData movementData;

    [SerializeField] private PlayerTransform playerTransform;
    private Vector3 _InputDirection;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerTransform.PlayersTransform = transform;
    }

    void FixedUpdate()
    {
        Vector3 CameraForward = followCamera.transform.forward;
        Vector3 CameraRight = followCamera.transform.right;

        CameraForward.y = 0;
        CameraRight.y = 0;

        CameraForward.Normalize();
        CameraRight.Normalize();

        Vector3 movement = (_InputDirection.x * CameraRight + _InputDirection.z * CameraForward) * movementData.Speed;
        movement.y = Rb.linearVelocity.y;
        Rb.linearVelocity = movement;


        if (movement.sqrMagnitude > 0.01f) 
        {
            Vector3 lookDirection = new Vector3(movement.x, 0, movement.z);
            if(lookDirection != Vector3.zero){
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                Visuals.rotation = Quaternion.Slerp(Visuals.rotation, targetRotation, Time.deltaTime * 10f);    
            }
            
        }
    }

    private void Jump(){
        Rb.linearVelocity = new Vector3(Rb.linearVelocity.x, movementData.JumpForce, Rb.linearVelocity.z);
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
    public void OnJump(InputAction.CallbackContext context){
        if(context.started){
            Jump();    
        }
    }

    
}
