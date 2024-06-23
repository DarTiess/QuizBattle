using UnityEngine;
using Zenject;

namespace Infrastructure.Installers.Settings.QuizSettings
{
    [CreateAssetMenu(fileName = "QuestionsSettingsInstaller", menuName = "Installers/QuestionsSettingsInstaller")]
    public class QuestionsSettingsInstaller : ScriptableObjectInstaller<QuestionsSettingsInstaller>
    {
        public QuestionsSettings QuestionsSettings;
        public override void InstallBindings()
        {
            Container.BindInstance(QuestionsSettings);
        }
    }
}