using UnityEngine;
using UnityEngine.UI;

public enum BarType
{
    Health, Stamina, // se pueden agregar mas barras luego
}

[RequireComponent(typeof(Slider))]
public class UIBarView : MonoBehaviour
{
    public BarType barType;
    public Slider slider { get; private set; }

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }
}
