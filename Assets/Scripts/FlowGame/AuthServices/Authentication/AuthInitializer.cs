using System;
using Unity.Services.Core;
using Unity.Services.Authentication;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class AuthInitializer : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button _signInAnonymousButton;
    [SerializeField] private Button _signUpAccountButton;
    [SerializeField] private Button _signInWithUsernameAndPasswordButton;
    [SerializeField] private Button _playGameButton;

    [Header("Input Fields")]
    [SerializeField] private TMP_InputField _usernameInputField;
    [SerializeField] private TMP_InputField _passwordInputField;

    [Header("Message Popup")]
    [SerializeField] private UI_Message _uiMessage;

    async void Start()
    {
        _signInAnonymousButton.interactable = false;
        await UnityServices.InitializeAsync();
        _signInAnonymousButton.interactable = true;
        _signInAnonymousButton.onClick.AddListener(SignIn);
        _playGameButton.interactable = false;

        _signUpAccountButton.onClick.AddListener(() =>
        {
            SignUpWithUsernamePasswordAsync(_usernameInputField.text, _passwordInputField.text);
        });

        _signInWithUsernameAndPasswordButton.onClick.AddListener(() =>
        {
            SignInWithUsernamePasswordAsync(_usernameInputField.text, _passwordInputField.text);
        });
        _playGameButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("PlayerTestScene");
        });
    }
    private async void SignIn()
    {
        _signInAnonymousButton.gameObject.SetActive(false);
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
        catch (Exception e)
        {
            _uiMessage.ShowErrorMessage(e.Message);
            Debug.LogError(e);
            return;
        }

        Debug.Log("Player ID: " + AuthenticationService.Instance.PlayerId);
        await Task.Delay(4000);
        _uiMessage.ShowMessage("SignIn Anonymous is successful");
        Debug.Log("Sign in successful");
        _playGameButton.interactable = true;
    }

    private async Task SignUpWithUsernamePasswordAsync(string username, string password)
    {
        try
        {
            await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(username, password);
        }
        catch (AuthenticationException ex)
        {
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message            
            _uiMessage.ShowErrorMessage(ex.Message);
            Debug.LogException(ex);
            throw;
        }
        catch (RequestFailedException ex)
        {
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            _uiMessage.ShowErrorMessage(ex.Message);
            Debug.LogException(ex);
            throw;
        }
        _uiMessage.ShowMessage("SignUp is successful.");
        _playGameButton.interactable = true;
    }

    private async Task SignInWithUsernamePasswordAsync(string username, string password)
    {
        try
        {
            await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(username, password);

        }
        catch (AuthenticationException ex)
        {
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message
            _uiMessage.ShowErrorMessage(ex.Message);
            Debug.LogException(ex);
            throw;
        }
        catch (RequestFailedException ex)
        {
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            _uiMessage.ShowErrorMessage(ex.Message);
            Debug.LogException(ex);
            throw;
        }
        _uiMessage.ShowMessage("SignIn is successful.");
        _playGameButton.interactable = true;
    }

    private void Update()
    {
        PlayButtonColor();
    }

    private void PlayButtonColor()
    {
        if (_playGameButton.interactable)
        {
            _playGameButton.GetComponent<Image>().color = Color.green;
        }
        else
        {
            _playGameButton.GetComponent<Image>().color = Color.gray;
        }
    }
}