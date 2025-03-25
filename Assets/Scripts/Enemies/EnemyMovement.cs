using UnityEngine;


public class EnemyMovement : MonoBehaviour {

    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int AOEAttack = Animator.StringToHash("AOEAttack");


    [SerializeField] private MovementData movementData;
    [SerializeField] private Rigidbody RB;
    [SerializeField] private PlayerTransform playerTransform;
    [SerializeField] private Animator animator;

    [SerializeField] private float attackRange = 30;

    private bool isAttacking;

    [Header("Debug")]
    [SerializeField] private bool DisableMovement = false;
    private void FixedUpdate()
    {
        if(DisableMovement) return;
        Vector3 direction = (playerTransform.PlayersTransform.position - transform.position).normalized;
        direction.y = 0;

        Vector3 nextPosition = transform.position + direction * movementData.Speed * Time.fixedDeltaTime;
        if(!isAttacking )
        {
            RB.MovePosition(nextPosition);
        }

        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.PlayersTransform.position);

        if(distanceToPlayer <= attackRange)
        {
            Attack();
        }

        Vector3 lookDirection = new Vector3(direction.x, 0, direction.z);
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);

        if (animator)
        {
            animator.SetFloat(Speed, RB.linearVelocity.magnitude);
        }
    }

    private void Attack()
    {
        isAttacking = true;
        animator.SetTrigger(AOEAttack);
    }

    public void OnAttackEnd()
    {
        isAttacking = false;
        animator.ResetTrigger(AOEAttack);
    }
}