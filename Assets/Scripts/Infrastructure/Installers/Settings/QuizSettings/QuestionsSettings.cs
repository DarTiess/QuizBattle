using UnityEngine;

namespace Infrastructure.Installers.Settings.QuizSettings
{
    [System.Serializable]
    public struct QuestionsSettings
    {
        [SerializeField] private TextAsset filePath;

        [SerializeField] private QuestionsList questionsList;
        public QuestionsList Questions
        {
            get
            {
                string json= filePath.text;
                questionsList = JsonUtility.FromJson<QuestionsList>(json);
                return questionsList;
            }
        }


    }
}