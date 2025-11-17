using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow Instance { get; private set; }
    private Transform _target;
    [SerializeField] private float _smoothSpeed = 5f;
    private Vector3 _offset;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void FixedUpdate()
    {
        if (_target == null) return;
        Vector3 desiredPosition = _target.position + _offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed * Time.deltaTime);
    }
    
    public void SetTarget(Transform target)
    {
        _target = target;
        _offset = transform.position - _target.position;
    }
}