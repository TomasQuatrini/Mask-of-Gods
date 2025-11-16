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
        _isRunning = _playerMovement.GetRunning();
        Move();
    }
    private void Move()
    {
        //var pos = MoveAction.ReadValue<Vector2>();
        _movement = _playerMovement.GetDirection();
        _movement.Normalize();
        if(_movement != Vector3.zero)
        {
            Vector3 desiredForward = Vector3.RotateTowards(_rb.transform.forward, _movement, _settings.turnSpeed * Time.deltaTime, 0f);
            _rotation = Quaternion.LookRotation(desiredForward);

            _rb.MoveRotation(_rotation);
            _rb.MovePosition(_rb.position + CurrentSpeed() * Time.deltaTime * _movement );
        }
        
    }
    private float CurrentSpeed()
    {
        return _isRunning ? _settings.runSpeed : _settings.walkSpeed;
    }

}
