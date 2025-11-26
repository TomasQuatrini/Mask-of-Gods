using System.Collections.Generic;
using UnityEngine;

public class HUDResourcesBarController : MonoBehaviour
{
    public static HUDResourcesBarController Instance { get; private set; }
    private Dictionary<BarType, UIBarView> _bars = new Dictionary<BarType, UIBarView>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        // Inicializar el diccionario de barras
        UIBarView[] barViews = GetComponentsInChildren<UIBarView>();
        foreach (var barView in barViews)
        {
            _bars[barView.barType] = barView;
        }
    }

    public void BindPlayer(PlayerHealth playerHealth, PlayerStaminaSM playerStaminaSM)
    {
        //Debug.Log($"[HUDController] Binding player UI with Health: {playerHealth}, Stamina: {playerStaminaSM}");
        if (playerHealth == null && playerStaminaSM == null)
        {
            Debug.LogError("[HUD] BindPlayer; Health o StaminaSM son null"); 
            return;
        }
        _bars[BarType.Health].slider.maxValue = playerHealth.HealthResource.Max;
        _bars[BarType.Health].slider.value = playerHealth.HealthResource.Current;
        _bars[BarType.Stamina].slider.maxValue = playerStaminaSM.StaminaResource.Max;
        _bars[BarType.Stamina].slider.value = playerStaminaSM.StaminaResource.Current;
        if (_bars.TryGetValue(BarType.Health, out var healthBar))
        {
            playerHealth.HealthResource.OnCurrentChanged += (currentHealth) =>
            {
                healthBar.slider.value = currentHealth;
            };
            playerHealth.HealthResource.OnMaxChanged += (maxHealth) =>
            {
                healthBar.slider.maxValue = maxHealth;
            };
        }
        if (_bars.TryGetValue(BarType.Stamina, out var staminaBar))
        {
            playerStaminaSM.StaminaResource.OnCurrentChanged += (currentStamina) =>
            {
                staminaBar.slider.value = currentStamina;
            };
            playerStaminaSM.StaminaResource.OnMaxChanged += (maxStamina) =>
            {
                staminaBar.slider.maxValue = maxStamina;
            };
        }
    }
}
