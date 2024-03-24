using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ECSGameInit : MonoBehaviour
{
    private EcsWorld world;
    private EcsSystems systems;

    public Action<EcsWorld> onInit;

    private void Start()
    {
        world = new EcsWorld();
        systems = new EcsSystems(world);

        systems
            .Add(new SpawnerSystem())
            .Inject()
            .Init();

        onInit?.Invoke(world);
    }

    private void Update()
    {
        systems?.Run();
    }

    private void OnDestroy()
    {
        if (systems != null)
        {
            systems.Destroy();
            systems = null;
        }

        if (world != null)
        {
            world.Destroy();
            world = null;
        }
    }
}
