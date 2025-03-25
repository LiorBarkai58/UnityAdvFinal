using UnityEngine;


[CreateAssetMenu(menuName = "Player/PlayerTransform")]
public class PlayerTransform : ScriptableObject {
    public Transform PlayersTransform { get; set; } 
}