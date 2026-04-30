using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum BarType
{
    Health, Stamina, // se pueden agregar mas barras luego
}

[RequireComponent(typeof(Slider))]
public class UIBarView : MonoBehaviour
{
    public BarType barType;
    public Slider slider { get; private set; }
    public TMP_Text text;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        text = GetComponentInChildren<TMP_Text>();
    }
}
