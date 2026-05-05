using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_Message : MonoBehaviour
{
    [SerializeField] private TMP_Text messageText;
    private Button closeWindowButton;
    [SerializeField] private GameObject root;

    public static UI_Message Instance { get; private set; }
    void Awake()
    {             
        if (root == null) 
        {
            Debug.LogError("Root GameObject is not assigned in the inspector.");
            return;
        }
        closeWindowButton = GetComponentInChildren<Button>();
        closeWindowButton.onClick.AddListener(CloseWindow);
    }

    private void Start()
    {
        CloseWindow();
    }

    private void CloseWindow()
    {
        root.SetActive(false);
    }

    public void ShowMessage(string message)
    {        
        messageText.color = Color.black;
        messageText.text = message;
        root.SetActive(true);
    }

    public void ShowErrorMessage(string message)
    {
        messageText.color = Color.red;
        messageText.text = message;
        root.SetActive(true);
    }
}
