using UnityEngine;

[CreateAssetMenu(menuName = "Character/MovementData")]
public class MovementData : ScriptableObject {
    [SerializeField] private float speed;

    [SerializeField] private float jumpForce;



    public float Speed => speed;

    public float JumpForce => jumpForce;

}