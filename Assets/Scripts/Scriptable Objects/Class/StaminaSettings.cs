using UnityEngine;

[CreateAssetMenu(fileName = "StaminaSettings", menuName = "ScriptableObjects/Game/Stats/Stamina Settings", order = 0)]
public class StaminaSettings : ScriptableObject
{
    public float baseMax = 100f;          // tamaño base de la barra
    public float spendRunning = 10f;      // consumo por segundo corriendo
    public float regenIdle = 5f;        // regen por segundo en idle
    public float regenExhausted = 3f;     // regen por segundo estando agotado
    [Range(0f, 100f)] public float exhaustedRecoverPercent = 20f; // % del Max para salir de Exhausted
}
