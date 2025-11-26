using UnityEngine;

public class PlayerJumpCommand : ICommand
{
    private MovementSettings _settings;
    private PlayerCollisionController _playerCollisionController;
    private Rigidbody _rb;
    private int _jumpCount = 0;

    public PlayerJumpCommand(Rigidbody rb, PlayerCollisionController playerCollisionController,
        MovementSettings settings)
    {
        _rb = rb;
        _settings = settings;
        _playerCollisionController = playerCollisionController;
        _playerCollisionController.PlayerCollidedWithGround += CountJumpsReset;
        CountJumpsReset();
    }
    //CountJumpsReset();
    public void Execute()
    {
        HandleJump();
    }
    private void HandleJump()
    {
        if (_jumpCount <= 0) return;

        _jumpCount--;
        _rb.AddForce(Vector3.up * _settings.jumpForce, ForceMode.Impulse);
        //Debug.Log($"Jump");
    }

    private void CountJumpsReset()
    {
        _jumpCount = _settings.maxJumpCount;
    }
    public void OnDestroy() //cual seria?
    {
        _playerCollisionController.PlayerCollidedWithGround -= CountJumpsReset;
    }
}
