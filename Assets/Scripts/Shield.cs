using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private InputPlayer _inputPlayer;
    [SerializeField] private GameObject _gameObject;

    private void Start()
    {
        _inputPlayer.OnDefense += ToggleDefense;
    }
    private void ToggleDefense(bool isDefending)
    {
        if (isDefending)
        {
            _gameObject.SetActive(true);
        }
        else
        {
            _gameObject.SetActive(false);
        }
    }
    
    private void OnDestroy()
    {
        _inputPlayer.OnDefense -= ToggleDefense;
    }
}
