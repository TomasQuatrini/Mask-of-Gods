using UnityEngine;

public class UniversalSpawn : MonoBehaviour
{
    public static UniversalSpawn Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public Vector3 Position()
    {
        return transform.position;
    }
}
