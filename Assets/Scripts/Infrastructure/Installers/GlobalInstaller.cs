using Infrastructure.Level;
using Infrastructure.Level.EventsBus;
using UnityEngine;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        CreateEventBus();
        CreateLevelLoader();
    }

    private void CreateEventBus()
    {
        Container.BindInterfacesAndSelfTo<EventBus>().AsSingle();
    }
    
    private void CreateLevelLoader()
    {
        Container.BindInterfacesAndSelfTo<LevelLoader>().AsSingle();
    }
}
