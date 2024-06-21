using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "QuestionsSettingsInstaller", menuName = "Installers/QuestionsSettingsInstaller")]
public class QuestionsSettingsInstaller : ScriptableObjectInstaller<QuestionsSettingsInstaller>
{
    public QuestionsSettings QuestionsSettings;
    public override void InstallBindings()
    {
        Container.BindInstance(QuestionsSettings);
    }
}