using System;
using Unity.Services.Core;
using Unity.Services.Authentication;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
public class AuthInitializer : MonoBehaviour
{

    [SerializeField] private Button signInAnonymousButton;
    async void Start()
    {
        signInAnonymousButton.interactable = false;
        await UnityServices.InitializeAsync();
        signInAnonymousButton.interactable = true;
        signInAnonymousButton.onClick.AddListener(SignIn);
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
            Debug.LogError(e);
            throw;
        }

        Debug.Log("Player ID: " + AuthenticationService.Instance.PlayerId);
        await Task.Delay(4000);
        Debug.Log("Sign in successful");
    }


}
