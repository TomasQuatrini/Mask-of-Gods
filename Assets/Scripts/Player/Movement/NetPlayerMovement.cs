using Fusion;
using UnityEngine;

public class NetPlayerMovement : NetworkBehaviour, IMovement
{
    private PlayerContext _cxt;
    private NetworkCharacterController _characterController;
    private PlayerStaminaSM _playerStaminaSM;

    public override void Spawned()
    {
        _cxt = GetComponentInParent<PlayerContext>();
        _characterController = GetComponentInParent<NetworkCharacterController>();
        _playerStaminaSM = _cxt.Stamina;
    }

    public override void FixedUpdateNetwork()
    {
        if (!Object.HasStateAuthority) { return; }
        if (GetInput<PlayerNetworkInput>(out var inputPlayer))
        {
           inputPlayer.Move.Normalize();
           _characterController.Move(inputPlayer.Move * Runner.DeltaTime);
        }
    }
}
