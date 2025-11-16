using System.Collections;
using TMPro;
using UnityEngine;

public class UI_PickUpText : MonoBehaviour
{
    private TMPro.TextMeshProUGUI _textMeshPro;
    public float seconds = 2f;

    private void Awake()
    {
        _textMeshPro = GetComponentInChildren<TMPro.TextMeshProUGUI>();
        if( _textMeshPro != null )
        _textMeshPro.gameObject.SetActive(false);
    }
    
    public void Show(string msg)
    {
        if (_textMeshPro == null) return;
        StopAllCoroutines();
        _textMeshPro.text = msg;
        _textMeshPro.gameObject.SetActive(true);
        StartCoroutine(HideLater());
    }

    private IEnumerator HideLater()
    {
        yield return new WaitForSeconds(seconds);
        if (_textMeshPro)
            _textMeshPro.gameObject.SetActive(false);
    }
}
