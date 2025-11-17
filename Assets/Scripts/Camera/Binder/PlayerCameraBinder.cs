using UnityEngine;

public class PlayerCameraBinder : MonoBehaviour
{
    private PlayerContext _ctx;

    private void Awake()
    {
        _ctx = GetComponentInParent<PlayerContext>();
    }

    private void Start()
    {
        if (_ctx == null)
        {
            Debug.LogError("PlayerCameraBinder no encontró PlayerContext en los padres!");
            enabled = false;
            return;
        }
        if (HUDController.Instance == null)
        {
            Debug.LogError("HUDController no está inicializado!");
            enabled = false;
            return;
        }
        CameraFollow.Instance.SetTarget(_ctx.transform);
    }
}
