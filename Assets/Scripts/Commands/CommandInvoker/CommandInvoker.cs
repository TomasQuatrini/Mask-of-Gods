using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// invoker que ejecuta los comandos en un punto único del frame
/// sirve para evitar problemas de concurrencia, y ademas para llevar un log de comandos si se desea
/// </summary>
public class CommandInvoker : MonoBehaviour
{
    private readonly Queue<ICommand> _q = new();

    public void Enqueue(ICommand cmd) => _q.Enqueue(cmd);

    private void Update()
    {
        while (_q.Count > 0)
            _q.Dequeue().Execute();
    }
}