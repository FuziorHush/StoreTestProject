using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameSceneInstaller : MonoInstaller
{
    [SerializeField] private PlayerBase _playerBase;
    [SerializeField] private HUD _HUD;

    public override void InstallBindings()
    {
        Container.Bind<PlayerBase>().FromInstance(_playerBase).AsSingle();
        Container.Bind<IInputHandler>().FromInstance(new PlayerInputHandlerMobile()).AsSingle();
        Container.Bind<HUD>().FromInstance(_HUD).AsSingle().NonLazy();
    }
}
