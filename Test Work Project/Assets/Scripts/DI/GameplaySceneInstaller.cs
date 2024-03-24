using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller
{
    [SerializeField] private ECSGameInit ecsGamePrefab;

    public override void InstallBindings()
    {
        var ecsInstance = Container.InstantiatePrefabForComponent<ECSGameInit>(ecsGamePrefab, transform);
        Container.Bind<ECSGameInit>().FromInstance(ecsInstance).AsSingle();
    }
}
