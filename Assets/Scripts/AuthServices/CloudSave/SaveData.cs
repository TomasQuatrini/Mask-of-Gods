using UnityEngine;
using TMPro;
using Unity.Services;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.Services.CloudSave;


public class SaveDataPlayer : MonoBehaviour
{
    [Header("Input Fields")]
    [SerializeField] private TMP_InputField _name;
    [SerializeField] private TMP_InputField _lastName;
    [SerializeField] private TMP_InputField _age;

    [Header("Buttons")]
    [SerializeField] private Button _saveButton;
    [SerializeField] private Button _deleteButton;

    private void Start()
    {
        _saveButton.onClick.AddListener(SaveAsyncData);
        _deleteButton.onClick.AddListener(DeleteAsyncData);
        LoadAsyncData();
    }

    private async void SaveAsyncData()
    {
        var name = _name.text;
        var lastName = _lastName.text;
        var age = _age.text;

        var playerData = new Dictionary<string, object>
        {
            { "name" , name },
            { "lastName", lastName },
            { "age", age }
        };

        await CloudSaveService.Instance.Data.Player.SaveAsync(playerData);
    }

    private async void DeleteAsyncData()
    {

    }

    private void OnDestroy()
    {
        _saveButton.onClick.RemoveListener(SaveAsyncData);
        _deleteButton.onClick.RemoveListener(DeleteAsyncData);
    }

    private async void LoadAsyncData()
    {
        var playerData = await CloudSaveService.Instance.Data.Player.LoadAllAsync();
        if (playerData.TryGetValue("name", out var name))
        {
            _name.text = name.ToString();
        }
        if (playerData.TryGetValue("lastName", out var lastName))
        {
            _lastName.text = lastName.ToString();
        }
        if (playerData.TryGetValue("age", out var age))
        {
            _age.text = age.ToString();
        }
    }
}
