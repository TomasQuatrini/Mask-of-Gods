using System.Collections;
using TMPro;
using UnityEngine;

public class Potion : MonoBehaviour
{
    [SerializeField] UI_PickUpText _PickUpText;    
    [SerializeField] private string _text;    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _PickUpText.Show(_text);
            Destroy(gameObject);
        }
    }
}
