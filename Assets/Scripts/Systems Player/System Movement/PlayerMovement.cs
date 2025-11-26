using UnityEngine;

public class PlayerMovement : MonoBehaviour, IMovement, IKnockbackeable
{
    private Rigidbody _rb;
    private PlayerContext _ctx;
    private PlayerCollisionController _playerCollisionController;
    private PlayerStaminaSM _stamina;
    private PlayerJumpCommand _jumpCommand;
    private PlayerMoveCommand _moveCommand;
    private InputPlayer _inputPlayer;


    [SerializeField] private MovementSettings _settings;

    private Vector3 _currentDirection = Vector3.zero;
    private bool _isRunning = false;
    private bool _hasStamina;

    [SerializeField] private float _knockbackDamping = 10f;
    private Vector3 _knockbackVelocity = Vector3.zero;
    private float _knockbackTime = 0f;

    private void Awake()
    {
        _ctx = GetComponentInParent<PlayerContext>();
        _rb = _ctx.Rigidbody;
        _inputPlayer = InputPlayer.Instance;
        if ( _inputPlayer == null ) Debug.LogWarning("InputPlayer instance not found in PlayerMovement");
        _playerCollisionController = _ctx.CollisionController;
        _stamina = _ctx.Stamina;
        if (_inputPlayer != null)
        {
            _inputPlayer.OnMove += HandleMove;
            _inputPlayer.OnRun += HandleRun;
            _inputPlayer.OnJump += HandleJump;            
        }
        if (_stamina != null)
        {
            _stamina.HasStamina += HasStamina;
        }

        _moveCommand = new PlayerMoveCommand(_rb, _playerCollisionController, _settings, this);
    }

    private void OnDestroy()
    {
        if (_inputPlayer != null)
        {
            _inputPlayer.OnMove -= HandleMove;
            _inputPlayer.OnRun -= HandleRun;
            _inputPlayer.OnJump -= HandleJump;            
        }
        if (_stamina != null)
        {
            _stamina.HasStamina -= HasStamina;
        }
    }

    private void FixedUpdate()
    {
        if (_moveCommand == null) return;
        _moveCommand.Tick(_knockbackVelocity);
        if (_knockbackTime > 0)
        {
            _knockbackTime -= Time.deltaTime;
            _knockbackVelocity = Vector3.Lerp(_knockbackVelocity, Vector3.zero, _knockbackDamping * Time.fixedDeltaTime);
        }
        else
        {
            _knockbackVelocity = Vector3.zero;
        }
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
    }

    private void HandleRun(bool running)
    {
        _isRunning = running;
    }

    private void HandleJump()
    {
        if(_jumpCommand is null) 
        {
            _jumpCommand = new PlayerJumpCommand(_rb, _playerCollisionController, _settings);
        }
        _jumpCommand.Execute();
    }

    private void HasStamina(bool has)
    {
        _hasStamina = has;
    }

    public void ApplyKnockback(Vector3 force, float duration)
    {
        _knockbackVelocity += force;
        _knockbackTime = Mathf.Max(_knockbackTime, duration);
    }
}