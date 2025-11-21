using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow Instance { get; private set; }

    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset = new Vector3(0f, 6f, -10f);
    [SerializeField] private float _smoothTime = 0.15f;
    [SerializeField] private float _maxSnapDistance = 20f; // si se aleja más que esto, teleporta

    private Vector3 _velocity;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        // opcional, si la cámara vive entre escenas:
        // DontDestroyOnLoad(gameObject);
    }

    private void LateUpdate()
    {
        if (_target == null) return;

        Vector3 desiredPosition = _target.position + _offset;
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
    }

    public void SetTarget(Transform target, bool snapInstant = true)
    {
        _target = target;

        if (_target == null) return;

        // NO recalculamos _offset acá, lo definimos a mano en el inspector.
        if (snapInstant)
        {
            transform.position = _target.position + _offset;
        }
    }
}