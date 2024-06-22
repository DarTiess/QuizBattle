using System;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.UIPanels
{
    public class GameplayPanel: PanelBase
    {
        [SerializeField] private TextMeshProUGUI playerName;
        [FormerlySerializedAs("playerCoins")]
        [SerializeField] private Text playerCoinsTxt;
        [SerializeField] private TextMeshProUGUI enemyName;
        [FormerlySerializedAs("enemyCoins")]
        [SerializeField] private Text enemyCoinsTxt;
        [Header("Quiz")]
        [SerializeField] private TextMeshProUGUI questionTxt;
        [SerializeField] private AnswerView[] answers;
       
        private Question[] _quest;
        private int _index = 0;
        private IPersonSettings _playerSettings;
        private IPersonSettings _enemySettings;
        private int _playerCoins;
        private int _enemyCoins;

        public event Action CompleteQuiz;
        public event Action RestartBot;
        public event Action<bool, int> PlayerMakeAnswer;
        public override event Action ClickedPanel;
        public int QuestIndex => _index;

        protected override void OnClickedPanel()
        {
            ClickedPanel?.Invoke();
        }

        public void Initialize( IPersonSettings playerSettings, IPersonSettings enemySettings)
        {
            _playerSettings = playerSettings;
            _enemySettings = enemySettings;
            
            SetPlayer(playerName, playerCoinsTxt,_playerCoins ,_playerSettings);
            SetPlayer(enemyName,enemyCoinsTxt,_enemyCoins, _enemySettings);
        }

        public void InitializeQuiz(QuestionsList questSettingz)
        {
            _quest = questSettingz.Questions;
            SetQuestions();
        }

        public void BotChoose(int choose)
        {
            answers[choose].BotChoose();
        }

        public async void DisplayRightAnswer()
        {
            DisplayAnswer();
           
            CountCoins(playerCoinsTxt,_playerCoins,  _playerSettings);
            CountCoins(enemyCoinsTxt, _enemyCoins, _enemySettings);
                
            _index++;
            await Task.Delay(1000);
            if (_index >= _quest.Length)
            {
                _playerCoins = 0;
                _enemyCoins = 0;
                CompleteQuiz?.Invoke();
            }
            else
            {
                SetQuestions();
                RestartBot?.Invoke();
            }
        }

        private void SetPlayer(TextMeshProUGUI name, Text coins, int coinsAmount, IPersonSettings settings)
        {
            name.text = settings.Name;
            name.color =settings.Color;
            coinsAmount = settings.Coins;
            coins.text = coinsAmount.ToString();
            coins.color = settings.Color;
        }

        private void SetQuestions()
        {
            questionTxt.transform.DOShakeScale(0.5f, new Vector3(1.5f, 1.5f, 1.5f));
            questionTxt.text = _quest[_index].Quest;
            for (int i = 0; i < answers.Length; i++)
            {
                answers[i].Initialize(_quest[_index].Answers[i], _playerSettings, _enemySettings);
                answers[i].MakeAnswer += PlayerAnswer;
            }
        }

        private void PlayerAnswer(bool isRighnt)
        {
            foreach (AnswerView answer in answers)
            {
                answer.SwitchEnable(false);
                answer.MakeAnswer -= PlayerAnswer;
            }
            PlayerMakeAnswer?.Invoke(isRighnt, _quest[_index].Coins);
        }

        private void DisplayAnswer()
        {
            for (int i = 0; i < _quest[_index].Answers.Length; i++)
            {
                if (_quest[_index].Answers[i].RightVariant)
                {
                    answers[i].DisplayRight();
                }
            }
        }

        private void CountCoins(Text coinsTxt,int coins, IPersonSettings settings)
        {
            Debug.Log(settings.Name+ " : "+settings.Coins);
            if (coins < settings.Coins)
            {
                coinsTxt.transform.DOShakeRotation(0.5f, 90f);
            }
            coinsTxt.DOText(settings.Coins.ToString(), 0.5f)
                    .OnComplete(() =>
                    {
                        coins = settings.Coins;
                    });
        }
    }
}