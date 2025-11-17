using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    public static HUDController Instance { get; private set; }
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
        if(playerHealth == null && playerStaminaSM == null) return;
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
            playerStaminaSM.StaminaResource.OnCurrentChanged += (maxStamina) =>
            {
                staminaBar.slider.maxValue = maxStamina;
            };
        }
    }
}
