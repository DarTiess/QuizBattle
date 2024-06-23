using UnityEngine;

namespace Infrastructure.Installers.Settings.QuizSettings
{
    [System.Serializable]
    public struct QuestionsList
    {
        [SerializeField] private Question[] questions;

        public Question[] Questions=>questions;

    }
}