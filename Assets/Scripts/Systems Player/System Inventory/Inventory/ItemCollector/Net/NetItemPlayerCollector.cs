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
        }

        public override void FixedUpdateNetwork()
        {
            if (!HasInputAuthority) return; 
            if (!GetInput(out PlayerNetworkInput input)) return; 
            if (!input.IsPickup) return;
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
            if (!HasStateAuthority) return;
            if (pickupObj == null) return;
            if (!pickupObj.TryGetComponent<NetItemPickup>(out var netItemPickup)) return;
            if (_inventoryComponent == null)
                if (_ctx == null) _ctx = GetComponentInParent<PlayerContext>();                
                _inventoryComponent = _ctx != null ? _ctx.Inventory : null;
                if (_inventoryComponent == null) return;
            var itemData = netItemPickup.GetItemData();
            var quantity = netItemPickup.GetQuantity();
            if (itemData == null) return;
            Rpc_ConfirmPickup(itemData.Id, quantity);
            if (Runner != null)
                Runner.Despawn(pickupObj);
            else
                Debug.LogWarning("[Collector] Runner is null, Don't despawn");
        }

        [Rpc(RpcSources.StateAuthority, RpcTargets.InputAuthority)]
        private void Rpc_ConfirmPickup(string itemId, int quantity, RpcInfo info = default)
        {
            if (!HasInputAuthority)
                return;
            if (_inventoryComponent == null)
            {
                if (_ctx == null)
                    _ctx = GetComponentInParent<PlayerContext>();
                _inventoryComponent = _ctx?.Inventory;
                if (_inventoryComponent == null) return;                
            }
            var itemData = _itemDataBasePotions.GetItemById(itemId);
            if (itemData == null) return;
            bool added = _inventoryComponent.AddItem(itemData, quantity);            
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _pickupRadius);
        }
    }
}