using UnityEngine;

[CreateAssetMenu(fileName = "MaskSetting", menuName = "Scriptable Objects/MaskSetting")]
public class MaskSetting : ScriptableObject
{
    public float damage;
    public float duration;
    public float cooldown;
}
