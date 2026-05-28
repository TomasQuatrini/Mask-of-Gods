using UnityEngine;

[CreateAssetMenu(fileName = "KeysMove", menuName = "ScriptableObjects/Keys/KeysMove", order = 1)]

[System.Serializable]
public class Keys : ScriptableObject
{
    [Header("Movement")]
    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;
    public KeyCode run;    
    public KeyCode jump;

    [Header("Combat")]
    public KeyCode attack;
    public KeyCode defense;
    public KeyCode takedamage;
    public KeyCode switchWeapon;

    [Header("Interaction")]
    public KeyCode pickup;
    public KeyCode consumeH;
    public KeyCode consumeS;    
    public KeyCode interact;
}
