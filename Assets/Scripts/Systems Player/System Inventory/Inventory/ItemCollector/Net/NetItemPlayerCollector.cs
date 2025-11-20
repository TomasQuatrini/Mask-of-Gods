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
            Debug.Log($"[Collector] OnTriggerEnter en {other.name} en {Object.name}");
            if (!Object.HasInputAuthority)
            {
                Debug.Log("[Collector] No soy InputAuthority, no puedo solicitar el pickup");
                return;
            }
            if (_inventoryComponent == null)
            {
                return;
            }
            if (!other.TryGetComponent<NetItemPickup>(out var pickup))
            {
                Debug.Log("[Collector] No es pickable");
                return;
            }
            var no = pickup.GetComponent<NetworkObject>();
            if (no == null)
            {
                Debug.Log("[Collector] pickup.Object es null, no tiene NetworkObject");
                return;
            }
            Debug.Log($"[Collector] Detecte pickup {pickup.name}. " + $"InputAuth= {Object.HasInputAuthority}, StateAuth= {Object.HasStateAuthority}");
            Rpc_RequestPickup(no);
        }

        [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
        public void Rpc_RequestPickup(NetworkObject pickableObject, RpcInfo info = default)
        {
            Debug.Log($"[Collector] Rpc_RequestPickup en {Object.name} " + $"SoyStateAuth={Object.HasStateAuthority}");
            if (!Object.HasStateAuthority)
            {
                Debug.Log("[Collector] No soy StateAuthority, no puedo procesar el pickup");
                return;
            }
            if (pickableObject == null)
            { 
                Debug.Log("[Collector] pickableObject es null");
                return; 
            }
            if (!pickableObject.TryGetComponent<NetItemPickup>(out var itemPickUp))
            {
                Debug.Log("[Collector] El objeto no tiene componente NetItemPickup");
                return;
            }
            if (_ctx == null)
            {
                _ctx = GetComponentInParent<PlayerContext>();
            }
            if (_inventoryComponent == null && _ctx != null)
            {
                Debug.Log("[Collector] InventoryComponent es null, intentando obtenerlo del contexto");
                _inventoryComponent = _ctx.Inventory;
            }
            var itemData = itemPickUp.GetItemData();
            var quantity = itemPickUp.GetQuantity();
            if (itemData == null)
            {
                Debug.Log("[Collector] itemData es null");
                return;
            }
            bool added = _inventoryComponent.AddItem(itemData, quantity);
            Debug.Log(added
                ? $"[Collector] Se agregaron {quantity} de {itemData.Name} al inventario"
                : $"[Collector] No se pudo agregar {itemData.Name} al inventario");
            if (!added) return;
            itemPickUp.OnPicked();
        }
    }
}