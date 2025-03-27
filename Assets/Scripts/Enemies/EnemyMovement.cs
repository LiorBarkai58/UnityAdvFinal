using UnityEngine;


public class EnemyMovement : MonoBehaviour {

    private static readonly int Speed = Animator.StringToHash("Speed");

    [SerializeField] private MovementData movementData;
    [SerializeField] private Rigidbody Rb;
    [SerializeField] private PlayerTransform playerTransform;
    [SerializeField] private Animator animator;

    [Header("Debug")]
    [SerializeField] private bool DisableMovement = false;

   public bool isAttacking = true;

    private void FixedUpdate()
    {
        if(DisableMovement) return;
        Vector3 direction = (playerTransform.PlayersTransform.position - transform.position).normalized;
        direction.y = 0;

        Vector3 nextPosition = transform.position + direction * movementData.Speed * Time.fixedDeltaTime;
        if(!isAttacking )
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

}