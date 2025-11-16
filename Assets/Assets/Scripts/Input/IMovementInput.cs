using UnityEngine;
public interface IMovementInput
{
    Vector3 MoveInput { get; }
    bool IsRunning { get; }
}
