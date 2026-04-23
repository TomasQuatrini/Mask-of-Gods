using UnityEngine;

[CreateAssetMenu(fileName = "KeysMove", menuName = "ScriptableObjects/Keys/KeysMove", order = 1)]
public class Keys : ScriptableObject
{
    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;
    public KeyCode run;    
    public KeyCode jump;
    public KeyCode attack;
    public KeyCode defense;
    public KeyCode pickup;
    public KeyCode takedamage;
    public KeyCode consumeH;
    public KeyCode consumeS;
    public KeyCode switchWeapon;
}
