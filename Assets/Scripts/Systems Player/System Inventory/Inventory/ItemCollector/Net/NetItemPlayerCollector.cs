using UnityEngine;
using Fusion;

namespace System.Inventory
{
    public class NetPlayerItemCollector : NetworkBehaviour
    {
        [Header("Pickup Settings")]
        [SerializeField] private float _pickupRadius = 1.5f;
        [SerializeField] private LayerMask _pickupMask;

        [Header("Data")]
        [SerializeField] private ItemDataBase _itemDataBasePotions;

        private PlayerInventoryComponent _inventoryComponent;
        private PlayerContext _ctx;



        public override void Spawned()
        {
            _ctx = GetComponentInParent<PlayerContext>();
            _inventoryComponent = _ctx != null ? _ctx.Inventory : null;     

            Debug.Log($"[Collector] Spawned en {Object.name} " +
                      $"StateAuth={Object.HasStateAuthority} InputAuth={Object.HasInputAuthority}");
        }

        public override void FixedUpdateNetwork()
        {
            if (!HasInputAuthority)
            { Debug.Log("[Collector] No tiene Input Authority, saliendo de FixedUpdateNetwork"); return; }

            if (!GetInput(out PlayerNetworkInput input)) { Debug.LogWarning("[Collector] No se pudo obtener el input"); return; }
            Debug.Log("[Collector] FixedUpdateNetwork - Input obtenido");
            if (!input.Pickup) return;
            Debug.Log("[Collector] Intentando recoger ítem...");

            var center = transform.position;
            var hits = Physics.OverlapSphere(center, _pickupRadius, _pickupMask);

            foreach (var hit in hits)
            {
                if (!hit.TryGetComponent<NetItemPickup>(out var pickup))
                    continue;

                Rpc_RequestPickup(pickup.Object);
                break;
            }
        }

        [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
        private void Rpc_RequestPickup(NetworkObject pickupObj, RpcInfo info = default)
        {
            Debug.Log("[Collector] Rpc_RequestPickup llamado en server");
            if (!HasStateAuthority)
                return;

            if (pickupObj == null)
            {
                Debug.LogWarning("[Collector] Rpc_RequestPickup: pickupObj es null");
                return;
            }

            if (!pickupObj.TryGetComponent<NetItemPickup>(out var netItemPickup))
            {
                Debug.LogWarning("[Collector] Rpc_RequestPickup: el NetworkObject no tiene NetItemPickup");
                return;
            }

            if (_inventoryComponent == null)
            {
                if (_ctx == null)
                    _ctx = GetComponentInParent<PlayerContext>();

                _inventoryComponent = _ctx != null ? _ctx.Inventory : null;
                if (_inventoryComponent == null)
                {
                    Debug.LogWarning("[Collector] InventoryComponent es null en Rpc_RequestPickup (server)");
                    return;
                }
            }

            var itemData = netItemPickup.GetItemData();
            var quantity = netItemPickup.GetQuantity();
            if (itemData == null)
            {
                Debug.LogWarning("[Collector] NetItemPickup sin ItemData en server");
                return;
            }
            Rpc_ConfirmPickup(itemData.Id, quantity);
            if (Runner != null)
                Runner.Despawn(pickupObj);
            else
                Debug.LogWarning("[Collector] Runner es null, no se pudo despawn el pickup");
        }

        [Rpc(RpcSources.StateAuthority, RpcTargets.InputAuthority)]
        private void Rpc_ConfirmPickup(string itemId, int quantity, RpcInfo info = default)
        {
            if (!HasInputAuthority)
                return;
            Debug.Log("[Collector] Rpc_ConfirmPickup llamado en client");
            if (_inventoryComponent == null)
            {
                if (_ctx == null)
                    _ctx = GetComponentInParent<PlayerContext>();

                _inventoryComponent = _ctx?.Inventory;
                if (_inventoryComponent == null)
                {
                    Debug.LogWarning("[Collector] InventoryComponent null en Rpc_ConfirmPickup (client)");
                    return;
                }
            }

            var itemData = _itemDataBasePotions.GetItemById(itemId);
            if (itemData == null)
            {
                Debug.LogWarning($"[Collector] No encontré ItemData para id {itemId} en client");
                return;
            }

            bool added = _inventoryComponent.AddItem(itemData, quantity);

            Debug.Log(added
                ? $"[Collector] CONFIRM pickup: se agregan {quantity} de {itemData.Name} al inventario LOCAL"
                : $"[Collector] CONFIRM pickup: NO se pudo agregar {itemData.Name} en client");
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _pickupRadius);
        }
    }
}