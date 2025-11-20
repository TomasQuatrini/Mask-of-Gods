using Fusion;
using UnityEngine;

public class NetPlayerMovement : NetworkBehaviour, IMovement
{
    private PlayerContext _cxt;
    private NetworkCharacterController _characterController;
    private PlayerStaminaSM _playerStaminaSM;
    private MovementSettings _movementSettings;

    [Header("Internos")]
    private int _jumpCount;
    private float _netMultiplyerValue = 1.5f;
    public override void Spawned()
    {
        _cxt = GetComponentInParent<PlayerContext>();
        _characterController = GetComponentInParent<NetworkCharacterController>();
        _playerStaminaSM = _cxt.Stamina;
        _movementSettings = _cxt.MovementSettings;
        _jumpCount = _movementSettings.maxJumpCount;

    }

    public override void FixedUpdateNetwork()
    {
        if (!Object.HasStateAuthority) { return; }
        if (GetInput<PlayerNetworkInput>(out var inputPlayer))
        {
            inputPlayer.Move.Normalize();
            _characterController.Move(inputPlayer.Move * Runner.DeltaTime);
            bool isGrounded = _characterController.Grounded;
            if (isGrounded)
            {
                CountJumpsReset();
            }
            if (inputPlayer.Jump && _jumpCount > 0)
            {
                _jumpCount--;
                _characterController.Jump(true, _movementSettings.jumpForce * _netMultiplyerValue);
            }
        }
    }
    private void CountJumpsReset()
    {
        _jumpCount = _movementSettings.maxJumpCount;
    }

    //public override void Despawn()
}
