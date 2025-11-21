using Fusion;
using Unity.VisualScripting;
using UnityEngine;

namespace System.Inventory
{
    public class ItemSpawner : NetworkBehaviour
    {
        [SerializeField] private NetworkPrefabRef _potionPrefab; // Arrastra aquí tu prefab de la poción
        [SerializeField] private Transform[] _spawnPoints;       // Puntos donde quieres que aparezcan
       
        public override void Spawned()
        {
            Debug.Log("[Spawner] Spawned llamado.");
            // Solo el servidor (StateAuthority) puede crear objetos de red
            if (HasStateAuthority)
            {
                SpawnItems();
            }
        }

        public void SpawnItems()
        {
            Debug.Log("[Spawner] SpawnItems llamado.");
            foreach (var spawnPoint in _spawnPoints)
            {
                var potionInstance = Runner.Spawn(_potionPrefab, spawnPoint.position, Quaternion.identity);
                Debug.Log($"[Spawner] Poción spawnada en {spawnPoint.position}");
            }
        }

    }
}