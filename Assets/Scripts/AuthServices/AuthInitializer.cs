using Unity.Services.Core;
using Unity.Services.Authentication;
using UnityEngine;
public class AuthInitializer : MonoBehaviour
{
    async void Start()
    {
        await UnityServices.InitializeAsync();
        await SignIn();
    }
    async System.Threading.Tasks.Task SignIn()
    {
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        Debug.Log("Player ID: " +
        AuthenticationService.Instance.PlayerId);
    }
}
// Inicializa Unity Game Services
// Inicia sesión anónima del jugador
// Imprime el Player ID en consola