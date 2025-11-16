using UnityEngine;

[CreateAssetMenu(fileName = "MovementSettings", menuName = "ScriptableObjects/MovementSettings", order = 1)]
public class MovementSettings : ScriptableObject
{
    public float walkSpeed;
    public float runSpeed;
    public float jumpForce;
    public int maxJumpCount;
    public float turnSpeed;
}
