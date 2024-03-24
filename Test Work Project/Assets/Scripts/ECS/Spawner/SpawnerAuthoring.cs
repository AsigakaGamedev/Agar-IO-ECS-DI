using Leopotam.EcsLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Voody.UniLeo.Lite;

public class SpawnerAuthoring : MonoProvider<SpawnerData>
{
    [SerializeField] private GameObject actorPrefab;
    [SerializeField] private GameObject foodPrefab;

    [Space]
    [SerializeField] private float spawnDelay = 1;

    [Space]
    [SerializeField] private Vector2 fieldSize;

    private ECSGameInit ecsGameInit;

    [Inject]
    public void Construct(ECSGameInit ecsGameInit)
    {
        this.ecsGameInit = ecsGameInit;
        this.ecsGameInit.onInit += OnECSInit;
    }

    private void OnDestroy()
    {
        if (ecsGameInit != null)
        {
            ecsGameInit.onInit -= OnECSInit;
        }
    }

    private void OnECSInit(EcsWorld world)
    {
        int entity = world.NewEntity();
        
        var spawnerPool = world.GetPool<SpawnerData>();
        spawnerPool.Add(entity);

        ref var spawnerData = ref spawnerPool.Get(entity);
        spawnerData = new SpawnerData()
        {
            ActorPrefab = actorPrefab,
            FoodPrefab = foodPrefab,

            SpawnDelay = spawnDelay,

            FieldSize = fieldSize
        };
    }
}

[System.Serializable]
public struct SpawnerData
{
    public GameObject ActorPrefab;
    public GameObject FoodPrefab;

    public float SpawnDelay;

    public Vector2 FieldSize;
}
