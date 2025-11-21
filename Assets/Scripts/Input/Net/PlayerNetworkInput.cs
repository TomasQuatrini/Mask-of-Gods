using Fusion;
using UnityEngine;

public struct PlayerNetworkInput : INetworkInput
{   
    public const byte RUN = 1;
    public const byte JUMP = 2;
    public const byte PICKUP = 3;

    public NetworkButtons buttons;
    public Vector3 Move;

    public bool IsRun;
    public bool IsJump;
    public bool IsPickup;
}
