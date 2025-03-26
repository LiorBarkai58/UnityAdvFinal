using UnityEngine;


public class EnemyMovement : MonoBehaviour {

    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int AOEAttack = Animator.StringToHash("AOEAttack");
    private static readonly int PunchAttack = Animator.StringToHash("PunchAttack");


    [SerializeField] private MovementData movementData;
    [SerializeField] private Rigidbody Rb;
    [SerializeField] private PlayerTransform playerTransform;
    [SerializeField] private Animator animator;

    [SerializeField] private float aoeAttackRange = 30;
    [SerializeField] private float punchAttackRange = 5;
    [SerializeField] private float attackCooldown = 5;

    private bool isAttacking;
    private int attackType;

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
            Rb.MovePosition(nextPosition);
        }

        Vector3 lookDirection = new Vector3(direction.x, 0, direction.z);
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);

        HandleAttack();

        if (animator)
        {
            animator.SetFloat(Speed, Rb.linearVelocity.magnitude);
        }
    }

    private void HandleAttack()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.PlayersTransform.position);

        if (distanceToPlayer <= punchAttackRange)
        {
            attackType = PunchAttack;
        }
        else if (distanceToPlayer <= aoeAttackRange)
        {
            attackType = AOEAttack;
        }
        else
        {
            return;
        }
        Attack();
    }

    private void Attack()
    {
        isAttacking = true;
        animator.SetTrigger(attackType);
    }

    public void OnAttackEnd() //animation event
    {
        isAttacking = false;
        animator.ResetTrigger(attackType);
    }


}