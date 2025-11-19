using Fusion;
using UnityEngine;

public class PlayerCameraBinderNet : NetworkBehaviour
{
    private PlayerContext _ctx;

    public override void Spawned()
    {
        _ctx = GetComponentInParent<PlayerContext>();
        if (!Object.HasInputAuthority)
            return;
        if (_ctx == null)
        {
            Debug.LogError("PlayerCameraBinder no encontró PlayerContext en los padres!");
            enabled = false;
            return;
        }
        if (CameraFollow.Instance == null)
        {
            Debug.LogError("Camera no está inicializado!");
            enabled = false;
            return;
        }
        CameraFollow.Instance.SetTarget(_ctx.transform);
    }
}
