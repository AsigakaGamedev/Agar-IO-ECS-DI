using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerSystem : IEcsRunSystem
{
    private readonly EcsFilterInject<Inc<SpawnerData>> spawnersPool = default;

    private float spawnTimer;

    public void Run(IEcsSystems systems)
    {
        foreach (int i in spawnersPool.Value)
        {
            ref SpawnerData spawnerData = ref spawnersPool.Pools.Inc1.Get(i);

            spawnTimer -= Time.deltaTime;

            if (spawnTimer <= 0)
            {
                Object.Instantiate(spawnerData.FoodPrefab, GetSpawnPos(spawnerData.FieldSize), Quaternion.identity);
                Object.Instantiate(spawnerData.ActorPrefab, GetSpawnPos(spawnerData.FieldSize), Quaternion.identity);

                spawnTimer = spawnerData.SpawnDelay;
            }
        }
    }

    private Vector2 GetSpawnPos(Vector2 fieldSize)
    {
        return new Vector2(Random.Range(-fieldSize.x, fieldSize.x),
                           Random.Range(-fieldSize.y, fieldSize.y));
    }
}
