using Fusion;
using UnityEngine;

public struct PlayerNetworkInput : INetworkInput
{
    public Vector3 Move;
    public NetworkBool Run;
    public NetworkBool Jump;
    public NetworkBool Pickup;
}
