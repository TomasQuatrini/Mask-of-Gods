using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private IInteractable _currentNPC;
    private InputPlayer _inputPlayer;
    private IContext _context;


    private void Start()
    {
        _inputPlayer = InputPlayer.Instance;
        _context = GetComponentInParent<IContext>();
    }

    private void Update()
    {
        if (_inputPlayer.Interact)
        {
            if (_currentNPC != null)
            {
                _currentNPC.Interact(_context);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CraftingNPC npc))
        {
            _currentNPC = other.GetComponent<CraftingNPC>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out CraftingNPC npc))
        {
            _currentNPC = null;
        }
    }
}
