using System;
using Unity.Services.Core;
using Unity.Services.Authentication;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using TMPro;
public class AuthInitializer : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button signInAnonymousButton;
    [SerializeField] private Button signUpAccountButton;
    [SerializeField] private Button signInWithUsernameAndPasswordButton;

    [Header("Input Fields")]
    [SerializeField] private TMP_InputField usernameInputField;
    [SerializeField] private TMP_InputField passwordInputField;

    [Header("Message Popup")]
    [SerializeField] private UI_Message uiMessage;

    async void Start()
    {
        signInAnonymousButton.interactable = false;
        await UnityServices.InitializeAsync();
        signInAnonymousButton.interactable = true;
        signInAnonymousButton.onClick.AddListener(SignIn);

        signUpAccountButton.onClick.AddListener(() =>
        {
            SignUpWithUsernamePasswordAsync(usernameInputField.text, passwordInputField.text);
        });

        signInWithUsernameAndPasswordButton.onClick.AddListener(() =>
        {
            SignInWithUsernamePasswordAsync(usernameInputField.text, passwordInputField.text);
        });
    }
    private async void SignIn()
    {
        signInAnonymousButton.gameObject.SetActive(false);
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
        catch (Exception e)
        {
            uiMessage.ShowErrorMessage(e.Message);
            Debug.LogError(e);
            return;
        }

        Debug.Log("Player ID: " + AuthenticationService.Instance.PlayerId);
        await Task.Delay(4000);
        uiMessage.ShowMessage("SignIn Anonymous is successful");
        Debug.Log("Sign in successful");
    }

    async Task SignUpWithUsernamePasswordAsync(string username, string password)
    {
        try
        {
            await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(username, password);
        }
        catch (AuthenticationException ex)
        {
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message            
            uiMessage.ShowErrorMessage(ex.Message);
            Debug.LogException(ex);
            throw;
        }
        catch (RequestFailedException ex)
        {
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            uiMessage.ShowErrorMessage(ex.Message);
            Debug.LogException(ex);
            throw;
        }
        uiMessage.ShowMessage("SignUp is successful.");
    }

    async Task SignInWithUsernamePasswordAsync(string username, string password)
    {
        try
        {
            await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(username, password);

        }
        catch (AuthenticationException ex)
        {
            // Compare error code to AuthenticationErrorCodes
            // Notify the player with the proper error message
            uiMessage.ShowErrorMessage(ex.Message);
            Debug.LogException(ex);
            throw;
        }
        catch (RequestFailedException ex)
        {
            // Compare error code to CommonErrorCodes
            // Notify the player with the proper error message
            uiMessage.ShowErrorMessage(ex.Message);
            Debug.LogException(ex);
            throw;
        }
        uiMessage.ShowMessage("SignIn is successful.");
    }
}