using UnityEngine;

public class CraftingTester : MonoBehaviour
{
    [SerializeField] private CraftingSystem _craftingSystem;
    [SerializeField] private Recipe _recipeToCraft;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            bool crafted = _craftingSystem.TryCraft(_recipeToCraft);
            if (!crafted)
            { 
                Debug.Log($"Failed to craft: {_recipeToCraft.name} - Missing ingredients or insufficient inventory space.");
            }
        }
    }
}
