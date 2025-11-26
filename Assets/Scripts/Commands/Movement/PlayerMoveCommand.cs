using UnityEngine;

public class PlayerMoveCommand : ICommand
{
    private MovementSettings _settings;
    private PlayerCollisionController _playerCollisionController;
    private Rigidbody _rb;
    private PlayerMovement _playerMovement;

    private Vector3 _currentDirection = Vector3.zero;
    private Vector3 _movement;
    private Quaternion _rotation = Quaternion.identity;
    private bool _isRunning;

    public PlayerMoveCommand(Rigidbody rb, PlayerCollisionController playerCollisionController,
        MovementSettings settings, PlayerMovement playerMovement)
    {
        _rb = rb;
        _settings = settings;
        _playerCollisionController = playerCollisionController;
        _playerMovement = playerMovement;
    }
    public void Execute()
    {
        Tick(Vector3.zero);
    }

    public void Tick(Vector3 knockback)
    {
        _isRunning = _playerMovement.GetRunning();
        Move(knockback);
    }
    private void Move(Vector3 knockback)
    {        
        _movement = _playerMovement.GetDirection();
        _movement.Normalize();

        Vector3 inputVelocity = Vector3.zero;
        if (_movement != Vector3.zero )
        {
            inputVelocity = _movement * CurrentSpeed();
            Vector3 desiredForward = Vector3.RotateTowards(_rb.transform.forward, _movement, _settings.turnSpeed * Time.fixedDeltaTime, 0f);
            _rotation = Quaternion.LookRotation(desiredForward);
            _rb.MoveRotation(_rotation);
        }
        Vector3 totalVelocity = inputVelocity + knockback;
        if (totalVelocity == Vector3.zero) return;
        _rb.MovePosition(_rb.position + totalVelocity * Time.fixedDeltaTime);
    }
    private float CurrentSpeed()
    {
        return _isRunning ? _settings.runSpeed : _settings.walkSpeed;
    }

}
