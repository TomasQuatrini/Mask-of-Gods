using UnityEngine;

public class CommandBus : MonoBehaviour
{
    public static CommandInvoker Invoker { get; private set; }
    private void Awake() => Invoker = GetComponent<CommandInvoker>();
}

