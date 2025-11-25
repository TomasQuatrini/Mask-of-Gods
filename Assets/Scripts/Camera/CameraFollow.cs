using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow Instance { get; private set; }

    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset = new Vector3(0f, 3f, -6f);
    [SerializeField] private float _smoothTime = 0.15f;
    [SerializeField] private float _maxSnapDistance = 20f;

    [Header("Rotación cámara")]
    [SerializeField] private float _sensX = 2f;
    [SerializeField] private float _sensY = 2f;
    [SerializeField] private float _minPitch = -30f;
    [SerializeField] private float _maxPitch = 60f;

    private float _yaw;    // giro horizontal alrededor del target
    private float _pitch;  // giro vertical
    private Vector3 _velocity;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        if (InputPlayer.Instance != null)
            InputPlayer.Instance.OnDelta += OnLookInput;
    }

    private void LateUpdate()
    {
        if (_target == null) return;

        // Creamos una rotación a partir de yaw/pitch
        Quaternion rot = Quaternion.Euler(_pitch, _yaw, 0f);

        // Aplicamos esa rotación al offset para orbitar alrededor del target
        Vector3 desiredPosition = _target.position + rot * _offset;

        float dist = Vector3.Distance(transform.position, desiredPosition);

        if (dist > _maxSnapDistance)
        {
            transform.position = desiredPosition;
        }
        else
        {
            transform.position = Vector3.SmoothDamp(
                transform.position,
                desiredPosition,
                ref _velocity,
                _smoothTime
            );
        }

        // Mirar siempre al target
        transform.LookAt(_target.position);
    }

    public void SetTarget(Transform target, bool snapInstant = true)
    {
        _target = target;

        if (_target == null) return;

        if (snapInstant)
        {
            // Inicializamos yaw/pitch en base al offset actual
            Vector3 dir = (transform.position - _target.position).normalized;
            float planarMag = new Vector2(dir.x, dir.z).magnitude;

            _pitch = Mathf.Atan2(dir.y, planarMag) * Mathf.Rad2Deg;
            _yaw = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;

            transform.position = _target.position + dir * _offset.magnitude;
        }
    }

    public void OnLookInput(Vector2 delta)
    {
        // Acumulamos, no reemplazamos
        _yaw += delta.x * _sensX;
        _pitch -= delta.y * _sensY;  // invertido para feeling habitual

        _pitch = Mathf.Clamp(_pitch, _minPitch, _maxPitch);
    }

    private void OnDestroy()
    {
        if (InputPlayer.Instance != null)
            InputPlayer.Instance.OnDelta -= OnLookInput;
    }
}