using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rb;
    private PlayerContext _ctx;
    private PlayerCollisionController _playerCollisionController;
    private PlayerStaminaSM _stamina;
    private PlayerJumpCommand _jumpCommand;
    private PlayerMoveCommand _moveCommand;
    private InputPlayer _inputPlayer;


    [SerializeField] private MovementSettings _settings; //como hacer para que no dependa del editor

    private Vector3 _currentDirection = Vector3.zero;
    private bool _isRunning = false;
    private bool _hasStamina;

    private void Awake()
    {
        _ctx = GetComponentInParent<PlayerContext>();
        _rb = _ctx.Body;
        _inputPlayer = _ctx.Input;
        _playerCollisionController = _ctx.CollisionController;
        _stamina = _ctx.Stamina;

        _inputPlayer.OnMove += HandleMove;
        _inputPlayer.OnRun += HandleRun;
        _inputPlayer.OnJump += HandleJump;
        _stamina.HasStamina += HasStamina;
    }

    private void OnDestroy()
    {
        _inputPlayer.OnMove -= HandleMove;
        _inputPlayer.OnRun -= HandleRun;
        _inputPlayer.OnJump -= HandleJump;
        _stamina.HasStamina -= HasStamina;
    }

    private void FixedUpdate()
    {
        //RbMoving();
    }
    public Vector3 GetDirection()
    {
        return _currentDirection;
    }
    public bool GetRunning()
    {
        if (_hasStamina == false)        
            return false;        
        else
            return _isRunning;       
    }

    private void HandleMove(Vector3 dir)
    {
        _currentDirection = dir;
        if (_moveCommand is null) { _moveCommand = new PlayerMoveCommand(_rb, _playerCollisionController, _settings, this); }
        _moveCommand.Execute();
    }

    private void HandleRun(bool running)
    {
        _isRunning = running;
    }

    private void HandleJump()
    {
        if(_jumpCommand is null) { _jumpCommand = new PlayerJumpCommand(_rb, _playerCollisionController, _settings); }
        _jumpCommand.Execute();
    }

    private void HasStamina(bool has)
    {
        _hasStamina = has;
    }
}
 