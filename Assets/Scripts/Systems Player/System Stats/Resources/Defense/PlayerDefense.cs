using UnityEngine;

public class PlayerDefense : MonoBehaviour, IDefense
{
    private InputPlayer _input;    
    [SerializeField] private Material[] materials;
    private MeshRenderer _meshRenderer;

    public bool isDefending { get; private set; }


    private void Start()
    {
        _input = InputPlayer.Instance;
        _input.OnDefense += ToggleDefense;
        _meshRenderer = GetComponentInParent<MeshRenderer>();
    }


    private void ToggleDefense(bool value)
    {
        if (value)
        {
            isDefending = true;
            if (materials.Length > 0)
            {
                SetMaterial(materials[1]);
            }
        }
        else
        {
            isDefending = false;
            if (materials.Length > 0)
            { 
                SetMaterial(materials[0]);
            }
        }
    }

    private void OnDestroy()
    {
        _input.OnDefense -= ToggleDefense;
    }

    private void SetMaterial(Material material)
    {
        _meshRenderer.material = material;
    }
}
