using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    //public Player player;

    // Diccionario de bindings (acción ? fábrica de comando)
    private Dictionary<string, System.Func<ICommand>> commandBindings;

    public InputAction moveAction;
    public InputAction jumpAction;
    public InputAction attackAction;

    private void Awake()
    {
        // Inicializar el diccionario
        commandBindings = new Dictionary<string, System.Func<ICommand>>
        {
            //{ "Jump",   () => new JumpCommand(player) },
            //{ "Attack", () => new AttackCommand(player) }
            // Movimiento lo manejamos distinto porque es continuo
        };
    }

    private void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();
        attackAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
        attackAction.Disable();
    }

    void Update()
    {
        // Movimiento continuo
        Vector2 move = moveAction.ReadValue<Vector2>();
        //ICommand moveCmd = new MoveCommand(player, move);
        

        // Saltar
        if (jumpAction.WasPerformedThisFrame())
            commandBindings["Jump"]().Execute();

        // Atacar
        if (attackAction.WasPerformedThisFrame())
            commandBindings["Attack"]().Execute();
    }
}