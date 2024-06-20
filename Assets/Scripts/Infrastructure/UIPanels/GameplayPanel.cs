using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.UIPanels
{
    public class GameplayPanel: PanelBase
    {
        [SerializeField] private TextMeshProUGUI playerName;
        [SerializeField] private TextMeshProUGUI enemyName;
        [Header("Quiz")]
        [SerializeField] private TextMeshProUGUI questionTxt;
        [SerializeField] private Answer[] answers;
        private Question[] _quest;
        private int _index = 0;
        public override event Action ClickedPanel;

        protected override void OnClickedPanel()
        {
            ClickedPanel?.Invoke();
        }

        public void Initialize(string player, string enemy)
        {
            playerName.text = player;
            enemyName.text = enemy;
        }

        public void InitializeQuiz(QuestionsList questSettingz)
        {
            _quest = questSettingz.Questions;
            questionTxt.text = _quest[_index].Quest;
            for (int i=0;i<answers.Length;i++)
            {
                answers[i].Initialize(_quest[_index].Answers[i]);
            }

            _index++;
            if (_index >= _quest.Length)
            {
                _index = 0;
            }
        }
    }
}