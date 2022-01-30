using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerSpawner : MonoBehaviour
{
    [SerializeField] PlayerMove _player;
    [SerializeField] GameObject _villagerPrefab;
    [SerializeField] Transform _spawnPoint;
    [SerializeField] int _spawnCount = 5;

    public void SpawnVillagers()
    {
        for (int i=0; i<_spawnCount; i++)
        {
            GameObject newVillager = Instantiate(_villagerPrefab, transform);
            newVillager.transform.position = _spawnPoint.position + new Vector3(0, 0, i);
            newVillager.GetComponent<VillagerAI>().GeneratePathToPoint(_player.transform.position);
        }

    }
}
