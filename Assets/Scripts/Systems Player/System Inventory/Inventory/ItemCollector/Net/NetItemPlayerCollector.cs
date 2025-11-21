using UnityEngine;
using Fusion;


namespace System.Inventory
{
    public class NetPlayerItemCollector : NetworkBehaviour
    {
        private PlayerInventoryComponent _inventoryComponent;
        private PlayerContext _ctx;
        public override void Spawned()
        {
            _ctx = GetComponentInParent<PlayerContext>();
            _inventoryComponent = _ctx.Inventory;
            Debug.Log($"[Collector] Spawned en {Object.name}" + $"StateAuth={Object.HasStateAuthority} InputAuth= {Object.HasInputAuthority}");
        }

        private void OnTriggerEnter(Collider other)
        {            
            if (!HasInputAuthority) return;            
            if (_inventoryComponent == null) return;            
            if (!other.TryGetComponent<NetItemPickup>(out var pickup)) return;
            var no = pickup.GetComponent<NetworkObject>();
            if (no == null) return;
            Debug.Log($"[Collector] Detecte pickup {pickup.name}. " + $"InputAuth= {Object.HasInputAuthority}, StateAuth= {Object.HasStateAuthority}");
            Rpc_RequestPickup(no);
        }

        [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
        public void Rpc_RequestPickup(NetworkObject pickableObject, RpcInfo info = default)
        {
            if (!HasStateAuthority) return;
            if (pickableObject == null) return;
            if (!pickableObject.TryGetComponent<NetItemPickup>(out var itemPickUp)) return;            
            if (_ctx == null) _ctx = GetComponentInParent<PlayerContext>();            
            if (_inventoryComponent == null && _ctx != null) _inventoryComponent = _ctx.Inventory;            
            var itemData = itemPickUp.GetItemData();
            var quantity = itemPickUp.GetQuantity();
            if (itemData == null) return;            
            bool added = _inventoryComponent.AddItem(itemData, quantity);
            Debug.Log(added
                ? $"[Collector] Se agregaron {quantity} de {itemData.Name} al inventario"
                : $"[Collector] No se pudo agregar {itemData.Name} al inventario");
            if (!added) return;
            itemPickUp.OnPicked();
        }

        [Rpc(RpcSources.StateAuthority, RpcTargets.InputAuthority)]
        public void Rpc_ConfirmPickup(int itemId, int quantity, RpcInfo info = default)
        {
            if (!HasInputAuthority) return;
            if (_inventoryComponent == null)
            {
                if (_ctx == null) _ctx = GetComponentInParent<PlayerContext>();
                _inventoryComponent = _ctx.Inventory;
                if (_inventoryComponent == null) return;
            }
            //var itemData = ItemDatabase.Instance.GetItemById(itemId);
            Debug.Log($"[Collector] Confirmacion de pickup: {quantity} de ");
        }
    }
}