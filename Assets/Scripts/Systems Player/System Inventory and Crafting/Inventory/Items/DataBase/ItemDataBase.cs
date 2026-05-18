using UnityEngine;
using System.Collections.Generic;
using System.Inventory;

[CreateAssetMenu(fileName = "ItemDataBase", menuName = "Inventory/ItemDataBase", order = 1)]
public class ItemDataBase : ScriptableObject
{
    [SerializeField] private List<ItemData> _items;
    private Dictionary<string, ItemData> _itemDictionary;

    private void OnEnable()
    {
        BuildMap();
    }

    private void BuildMap()
    {
        _itemDictionary = new Dictionary<string, ItemData>();
        foreach (var item in _items)
        {
            if (item == null) continue;
            if (_itemDictionary.ContainsKey(item.Id))
            {
                Debug.LogWarning($"Duplicate item ID detected: {item.Id}. Skipping item {item.Name}.");
                continue;
            }
            _itemDictionary.Add(item.Id, item);
        }
    }

    public ItemData GetItemById(string id)
    {
        if (_itemDictionary == null || _itemDictionary.Count == 0)
        {
            BuildMap();
        }
        _itemDictionary.TryGetValue(id, out var item);
        return item;
    }
}
