using UnityEngine;


public class EnemyMovement : MonoBehaviour {

    private static readonly int Speed = Animator.StringToHash("Speed");

    [SerializeField] private MovementData movementData;
    [SerializeField] private Rigidbody Rb;
    [SerializeField] private PlayerTransform playerTransform;
    [SerializeField] private Animator animator;

    [SerializeField] private Transform bottomPos;

    [SerializeField] private LayerMask groundLayer;

    [Header("Debug")]
    [SerializeField] private bool DisableMovement = false;

   public bool canMove = true;

   private bool isClimbing = false;

   private float climbingMultiplier = 15;

    private void FixedUpdate()
    {
        if(DisableMovement) return;
        Vector3 direction = (playerTransform.PlayersTransform.position - transform.position).normalized;
        direction.y = 0;

        Vector3 nextPosition = transform.position + direction * movementData.Speed * Time.fixedDeltaTime;
        ClimbLogic();
        if(isClimbing) {
            Rb.linearVelocity = new Vector3(Rb.linearVelocity.x, 0, Rb.linearVelocity.y);
            nextPosition += Vector3.up * climbingMultiplier * Time.fixedDeltaTime;
        }
        if(canMove)
        {
            Rb.MovePosition(nextPosition);
        }

        Vector3 lookDirection = new Vector3(direction.x, 0, direction.z);
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);

        if (animator)
        {
            animator.SetFloat(Speed, Rb.linearVelocity.magnitude);
        }
    }

    private void ClimbLogic(){
        Ray ray = new Ray(bottomPos.position, (playerTransform.PlayersTransform.position - transform.position).normalized);
        if(Physics.Raycast(ray, 0.8f, groundLayer)){
            isClimbing = true;
        }
        else isClimbing = false;
        

    }


}