using UnityEngine;

public class HUD_PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenuPanel;
    private InputPlayer _input;


    private void Start()
    {
        _input = InputPlayer.Instance;
        if (_input == null)
        {
            Debug.LogError("HUD_PauseMenuController: InputPlayer instance not found.");
            return;
        }
    }
}
