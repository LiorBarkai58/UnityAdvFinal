using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody Rb;

    [SerializeField] private CinemachineCamera followCamera;
    private Vector3 _InputDirection;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 CameraForward = followCamera.transform.forward.normalized;
        Vector3 CameraRight = followCamera.transform.right.normalized;

        CameraForward.y = 0;
        CameraRight.y = 0;

        Rb.linearVelocity = _InputDirection.x * CameraRight + _InputDirection.y * CameraForward;
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
