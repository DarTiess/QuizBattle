using UnityEngine;
using Zenject;

namespace Infrastructure.Installers.Settings
{
    [CreateAssetMenu(fileName = "EnemySettingsInstaller", menuName = "Installers/EnemySettingsInstaller")]
    public class QuizSettingsInstaller : ScriptableObjectInstaller<QuizSettingsInstaller>
    {
        public EnemySettings EnemySettings;
        public PlayerSettings PlayerSettings;
        public override void InstallBindings()
        {
            Container.BindInstance(EnemySettings);
            Container.BindInstance(PlayerSettings);
        }
    }
}