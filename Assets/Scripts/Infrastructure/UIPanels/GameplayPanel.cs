using System;
using System.Threading.Tasks;
using DG.Tweening;
using Infrastructure.Installers.Settings;
using Infrastructure.Installers.Settings.QuizSettings;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Infrastructure.UIPanels
{
    public class GameplayPanel: PanelBase
    {
        [SerializeField] private TextMeshProUGUI clockLogo;
        [SerializeField] private Image clocks;
        [SerializeField] private TextMeshProUGUI speedLogo;
        [SerializeField] private TextMeshProUGUI playerName;
        [Header("Players")]
        [FormerlySerializedAs("playerCoins")]
        [SerializeField] private Text playerCoinsTxt;
        [SerializeField] private Text playerSpeedTxt;
        [SerializeField] private TextMeshProUGUI enemyName;
        [FormerlySerializedAs("enemyCoins")]
        [SerializeField] private Text enemyCoinsTxt;
        [SerializeField] private Text enemySpeedTxt;
        [Header("Quiz")]
        [SerializeField] private TextMeshProUGUI questionTxt;
        [SerializeField] private AnswerView[] answers;
        public int QuestIndex => _index;

        private Question[] _quest;
        private int _index = 0;
        private IPersonSettings _playerSettings;
        private IPersonSettings _enemySettings;
        private int _playerCoins;
        private int _enemyCoins;
        private bool _timeIsOut;
        private bool _playerAnswer;
        private float _startSpeedLogoYPosition;
        private bool _botAnswer;
        private float _animationDuration;
        private float _animationScale;
        private float _rotateAngle;

        public event Action CompleteQuiz;
        public event Action RestartBot;
        public event Action<bool, int> PlayerMakeAnswer;
        public override event Action ClickedPanel;

        protected override void OnClickedPanel()
        {
            ClickedPanel?.Invoke();
        }

        public void Initialize(float animationDuration, float animationScale,float rotateAngle,
                               IPersonSettings playerSettings, IPersonSettings enemySettings)
        {
            _playerSettings = playerSettings;
            _enemySettings = enemySettings;
            _animationDuration = animationDuration;
            _animationScale = animationScale;
            _rotateAngle = rotateAngle;
            SetPlayer(playerName, playerCoinsTxt,_playerCoins, playerSpeedTxt,_playerSettings);
            SetPlayer(enemyName,enemyCoinsTxt,_enemyCoins, enemySpeedTxt,_enemySettings);
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
           
            CountCoins(playerCoinsTxt, playerSpeedTxt, _playerSettings,ref _playerCoins);
            CountCoins(enemyCoinsTxt,enemySpeedTxt, _enemySettings, ref _enemyCoins);
                
            await IsContinue();
        }

        private async Task IsContinue()
        {
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

        private void SetQuestions()
        {
            _playerAnswer = false;
            SetTimer();

            questionTxt.transform.DOShakeScale(_animationDuration, _animationScale);
            questionTxt.text = _quest[_index].Quest;
          
            for (int i = 0; i < answers.Length; i++)
            {
                answers[i].Initialize(_quest[_index].Answers[i], _animationDuration, _playerSettings, _enemySettings);
                answers[i].MakeAnswer += PlayerAnswer;
            }
        }

        private void SetTimer()
        {
            speedLogo.gameObject.SetActive(false);
            _startSpeedLogoYPosition = speedLogo.gameObject.transform.position.y;
            _timeIsOut = false;

            clockLogo.transform.DOKill();
            speedLogo.transform.DOKill();
            clockLogo.transform.DOScale(_animationScale,_animationDuration).SetLoops(-1, LoopType.Yoyo);
            clocks.fillAmount = 0;
            clocks.DOFillAmount(1, _playerSettings.MaxTimeToAnswer)
                  .OnComplete(() =>
                  {
                      _timeIsOut = true;
                      if (!_playerAnswer)
                      {
                          
                          speedLogo.gameObject.SetActive(true);
                          speedLogo.transform.DOMoveY(playerName.transform.position.y, _animationDuration)
                                   .OnComplete(() =>
                                   {
                                       speedLogo.gameObject.SetActive(false);
                                       speedLogo.gameObject.transform.position 
                                           = new Vector3(speedLogo.transform.position.x,
                                                         _startSpeedLogoYPosition,
                                                         speedLogo.transform.transform.position.z);
                                   });
                      }
                  });
        }

        private void SetPlayer(TextMeshProUGUI name, Text coins, int coinsAmount, Text speed, IPersonSettings settings)
        {
            name.text = settings.Name;
            name.color =settings.Color;
            coinsAmount = settings.Coins;
            coins.text = coinsAmount.ToString();
            coins.color = settings.Color;
            speed.text = settings.Speed.ToString();
            speed.color = settings.Color;
        }

        private void PlayerAnswer(bool isRighnt)
        {
            _playerAnswer = true;
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

        private void CountCoins(Text coinsTxt,Text speed, IPersonSettings settings,ref int coins)
        {
            Debug.Log(settings.Name+ " : "+settings.Coins+" coins:"+coins);
            if (coins < settings.Coins)
            {
                coinsTxt.transform.DOShakeRotation(_animationDuration, _rotateAngle);
            }

            
            speed.DOText(settings.Speed.ToString(), _animationDuration)
                 .OnStart(() =>
                 {
                     speed.transform.DOShakeScale(_animationDuration,_animationScale);
                 }); 
            
          
            coins = settings.Coins;
            coinsTxt.DOText(settings.Coins.ToString(),_animationDuration);
        }
    }
}