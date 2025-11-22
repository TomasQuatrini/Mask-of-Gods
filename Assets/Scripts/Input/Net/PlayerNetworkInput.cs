using Fusion;
using UnityEngine;

public struct PlayerNetworkInput : INetworkInput
{    
    public Vector3 Move;

    public bool IsRun;
    public bool IsJump;
    public bool IsPickup;
    public bool TakeDamaged;
    public bool ConsumeStamina;
    public bool ConsumeHealth;
}
