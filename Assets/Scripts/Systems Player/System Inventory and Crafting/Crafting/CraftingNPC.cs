using System.Collections.Generic;
using UnityEngine;

public class CraftingNPC : MonoBehaviour, IInteractable
{
    [SerializeField] private HUD_CraftingUI _craftingUI;
    [SerializeField] private Recipe[] _availableRecipes;

    public void Interact(IContext context)
    {
        if (context is PlayerContext playerContext)
        {
            _craftingUI.Open(playerContext, _availableRecipes);
        }
    }
}
