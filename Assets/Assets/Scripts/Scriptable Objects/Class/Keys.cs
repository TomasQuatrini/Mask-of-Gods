using UnityEngine;

[CreateAssetMenu(fileName = "KeysMove", menuName = "ScriptableObjects/Keys/KeysMove", order = 1)]
public class KeysMove : ScriptableObject
{
    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;
    public KeyCode run;    
    public KeyCode jump;
    public KeyCode attack;
    public KeyCode specialAttack1;
}
