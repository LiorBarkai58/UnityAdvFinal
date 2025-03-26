using System.Collections;
using UnityEngine;


public class EnemyMovement : MonoBehaviour {

    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int AOEAttack = Animator.StringToHash("AOEAttack");
    private static readonly int PunchAttack = Animator.StringToHash("PunchAttack");


    [SerializeField] private MovementData movementData;
    [SerializeField] private Rigidbody Rb;
    [SerializeField] private PlayerTransform playerTransform;
    [SerializeField] private Animator animator;

    [SerializeField] private float aoeAttackRange = 20;
    [SerializeField] private float aoeAttackCooldown = 5;

    [SerializeField] private float punchAttackRange = 5;
    [SerializeField] private float punchAttackCooldown = 2;

    private bool isAttacking;
    private float lastPunchAttackTime;
    private float lastAOEAttackTime;
    private int currentAttackType;


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
        float currentTime = Time.time;
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.PlayersTransform.position);

        if (distanceToPlayer <= punchAttackRange && currentTime - lastPunchAttackTime >= punchAttackCooldown)
        {
            Attack(PunchAttack, ref lastPunchAttackTime);
        }
        else if (distanceToPlayer <= aoeAttackRange && currentTime - lastAOEAttackTime >= aoeAttackCooldown)
        {
            Attack(AOEAttack, ref lastAOEAttackTime);
        }
    }

    private void Attack(int attackType, ref float lastAttackTime)
    {
        isAttacking = true;
        currentAttackType = attackType;
        lastAttackTime = Time.time;
        animator.SetTrigger(attackType);
    }

    public void OnAttackEnd() //animation event
    {
        isAttacking = false;
        animator.ResetTrigger(currentAttackType);
    }


}