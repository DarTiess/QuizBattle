using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "EnemySettingsInstaller", menuName = "Installers/EnemySettingsInstaller")]
public class EnemySettingsInstaller : ScriptableObjectInstaller<EnemySettingsInstaller>
{
    public EnemySettings EnemySettings;
    public override void InstallBindings()
    {
        Container.BindInstance(EnemySettings);
    }
}