using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _smoothSpeed = 5f;
    private Vector3 _offset;

    private void Awake()
    {
        if (_target == null)
        {
            Debug.LogError("CameraFollow no tiene asignado un target!");
            enabled = false;
            return;
        }
        _offset = transform.position - _target.position;
    }

    private void FixedUpdate()
    {
        if (_target == null) return;
        Vector3 desiredPosition = _target.position + _offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed * Time.deltaTime);
    }   
}