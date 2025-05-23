using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int IsJumping = Animator.StringToHash("IsJumping");
    private static readonly int IsFalling = Animator.StringToHash("IsFalling");

    [Header("Character References")]
    [SerializeField] private Rigidbody Rb;

    [SerializeField] private CinemachineCamera followCamera;

    [SerializeField] private Transform Visuals;

    [SerializeField] private Animator animator;

    [Header("Character Data")]

    [SerializeField] private MovementData movementData;

    [SerializeField] private PlayerTransform playerTransform;
    private Vector3 _InputDirection;

    private float ySpeed;
    private float timeOfGround;

    private bool isJumping;
    private bool isGrounded;
    private bool isFalling;

    public event UnityAction OnPlayerPause;

    void Awake()
    {
        playerTransform.PlayersTransform = transform;
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    

    void FixedUpdate()
    {
        HandleMovement();
        HandleJump();  
    }

    private void OnEnable()
    {
        SaveGameManager.OnSave += SavePlayerState;
        SaveGameManager.OnLoad += LoadPlayerState;
    }

    private void OnDisable()
    {
        SaveGameManager.OnSave -= SavePlayerState;
        SaveGameManager.OnLoad -= LoadPlayerState;
    }

    private void SavePlayerState(SerializedSaveGame saveData)
    {
        saveData.playerPositionX = transform.position.x;
        saveData.playerPositionY = transform.position.y;
        saveData.playerPositionZ = transform.position.z;

        saveData.playerRotationX = transform.rotation.eulerAngles.x;
        saveData.playerRotationY = transform.rotation.eulerAngles.y;
        saveData.playerRotationZ = transform.rotation.eulerAngles.z;
    }

    private void LoadPlayerState(SerializedSaveGame saveData)
    {
        if(new Vector3(saveData.playerPositionX, saveData.playerPositionY, saveData.playerPositionZ) != Vector3.zero){
            transform.position = new Vector3(saveData.playerPositionX, saveData.playerPositionY + 3, saveData.playerPositionZ);
            transform.eulerAngles = new Vector3(saveData.playerRotationX, saveData.playerRotationY, saveData.playerRotationZ);
        }
        
    }

    private bool GroundCheck()
    {
        RaycastHit hit;
        return Physics.Raycast(transform.position, Vector3.down, out hit, 0.5f);
    }

    private void Jump()
    {
        if (isGrounded)
        {
            isJumping = true;
            isFalling = false; // Reset falling state
            Rb.linearVelocity = new Vector3(Rb.linearVelocity.x, movementData.JumpForce, Rb.linearVelocity.z);
            isGrounded = false; // Ensure the player is considered off the ground
        }
    }
    private void HandleJump(){

        ySpeed = Rb.linearVelocity.y;

        if (GroundCheck())
        {
            if (!isJumping) // Reset only if not jumping
            {
                isGrounded = true;
                isFalling = false;
                timeOfGround = 0;
            }

        }
        else
        {
            isGrounded= false;
            timeOfGround += Time.fixedDeltaTime;
            if(isJumping && ySpeed < 0 || timeOfGround > 0.5)
            {
                isFalling = true;
                isJumping = false;
            }
        }
        if (animator)
        {
            animator.SetBool(IsJumping, isJumping);
            animator.SetBool(IsFalling, isFalling);
        }
    }

    private void HandleMovement(){
        Vector3 CameraForward = followCamera.transform.forward;
        Vector3 CameraRight = followCamera.transform.right;

        CameraForward.y = 0;
        CameraRight.y = 0;

        CameraForward.Normalize();
        CameraRight.Normalize();

        Vector3 movement = (_InputDirection.x * CameraRight + _InputDirection.z * CameraForward) * movementData.Speed;
        movement.y = Rb.linearVelocity.y;
        Rb.linearVelocity = movement;

        if (animator)
            animator.SetFloat(Speed, Rb.linearVelocity.magnitude);



        if (movement.sqrMagnitude > 0.01f) 
        {
            Vector3 lookDirection = new Vector3(movement.x, 0, movement.z);
            if(lookDirection != Vector3.zero){
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                Visuals.rotation = Quaternion.Slerp(Visuals.rotation, targetRotation, Time.deltaTime * 10f);    
            }
            
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
    public void OnJump(InputAction.CallbackContext context){
        if(context.started){
            Jump();    
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("pause pressed");
            OnPlayerPause?.Invoke();
        }
    }

    
}
