using Fusion;
using UnityEngine;

public class NetPlayerInputBridge : NetworkBehaviour
{
    private PlayerContext _cxt;
    private IMovement _playerMovement;

    public override void Spawned()
    {
        _cxt = GetComponentInParent<PlayerContext>();
        _playerMovement = _cxt.Movement;
    }

    public override void FixedUpdateNetwork()
    {
        if (!Object.HasStateAuthority) return;
        if (GetInput(out PlayerNetworkInput data))
        {
            //_playerMovement.NetworkTick(data.Move, data.Run, data.Jump);
        }
    }
}
