using System.Threading.Tasks;
using Infrastructure.Level.EventsBus;
using Infrastructure.Level.EventsBus.Signals;
using UnityEngine;

namespace DefaultNamespace
{
    public interface IQuizHandler
    {
        void PlayerAnswer(bool isRight, int coins);
        void StartBot(int questIndex);
        void CompleteQuiz();
    }
    public class QuizHandler : IQuizHandler
    {
        private readonly IEventBus _eventBus;
        private int _playerCoins;
        private readonly QuestionsSettings _quizSettings;
        private  PlayerSettings _playerSettings;
        private  EnemySettings _enemySettings;
        private Question _quest;
        private bool _isPlayerChoosed;
        private bool _isEnemyChoosed;

        public QuizHandler(IEventBus eventBus, QuestionsSettings questionsSettings, PlayerSettings playerSettings, EnemySettings enemySettings)
        {
            _eventBus = eventBus;
            _quizSettings = questionsSettings;
            
            _playerSettings = playerSettings;
            _enemySettings = enemySettings;
        }

        public void PlayerAnswer(bool isRight, int coins)
        {
            if (isRight)
            {
                _playerCoins+=coins;
                _playerSettings.Coins = _playerCoins;
            }

            _isPlayerChoosed = true;
            MakeChoose();
        }

        public void StartBot(int questIndex)
        {
            _quest = _quizSettings.Questions.Questions[questIndex];
           int maxrnd= _quest.Answers.Length;
           int rnd = Random.Range(0, maxrnd);
           BotChoose(rnd);
        }

        public void CompleteQuiz()
        {
            if (_playerSettings.Coins > _enemySettings.Coins)
            {
                _eventBus.Invoke(new LevelWin());
            }
            else
            {
                _eventBus.Invoke(new LevelLost());
            }
        }

        private async void BotChoose(int rndChoose)
        {
            await Task.Delay(5000);
            _eventBus.Invoke(new BotChoose(rndChoose));
            _isEnemyChoosed = true;
            
           
            if (_quest.Answers[rndChoose].RightVariant)
            {
                _enemySettings.Coins += _quest.Coins;
            }
            MakeChoose();
        }

        private async void MakeChoose()
        {
            if (_isPlayerChoosed && _isEnemyChoosed)
            {
                _isPlayerChoosed = false;
                _isEnemyChoosed = false;
                await Task.Delay(500);
                
                _eventBus.Invoke(new FinishChoosing());
               
            }
        }
    }
}