using System;
using System.Threading.Tasks;
using Infrastructure.EventsBus;
using Infrastructure.EventsBus.Signals;
using Infrastructure.Installers.Settings;
using Infrastructure.Installers.Settings.QuizSettings;
using Random = UnityEngine.Random;

public class QuizHandler : IQuizHandler, IDisposable
{
    private readonly IEventBus _eventBus;
    private int _playerCoins;
    private readonly QuestionsSettings _quizSettings;
    private  PlayerSettings _playerSettings;
    private  EnemySettings _enemySettings;
    private Question _quest;
    private bool _isPlayerChoosed;
    private bool _isEnemyChoosed;
    private bool _second;

    public QuizHandler(IEventBus eventBus, QuestionsSettings questionsSettings, PlayerSettings playerSettings, EnemySettings enemySettings)
    {
        _eventBus = eventBus;
            
        _eventBus.Subscribe<RestartGame>(ClearAll);
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
        if (!_second)
        {
            _playerSettings.Speed++;
            _second = true;
        }
        else
        {
            _playerSettings.Speed--;
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
        var plComplete = _playerSettings.Coins + _playerSettings.Speed;
        var enComplete = _enemySettings.Coins + _enemySettings.Speed;
        if (plComplete > enComplete)
        {
            _eventBus.Invoke(new LevelWin());
        }
        else
        {
            _eventBus.Invoke(new LevelLost());
        }
    }

    public void Dispose()
    {
        Clear();
    }

    private async void BotChoose(int rndChoose)
    {
        await Task.Delay(Random.Range(1,_enemySettings.MaxTimeToAnswer));
        _eventBus.Invoke(new BotChoose(rndChoose));
        _isEnemyChoosed = true;
            
           
        if (_quest.Answers[rndChoose].RightVariant)
        {
            _enemySettings.Coins += _quest.Coins;
        } 
        if (!_second)
        {
            _enemySettings.Speed++;
            _second = true;
        }
        else
        {
            _enemySettings.Speed--;
        }
        MakeChoose();
    }

    private async void MakeChoose()
    {
        if (_isPlayerChoosed && _isEnemyChoosed)
        {
            _isPlayerChoosed = false;
            _isEnemyChoosed = false;
            _second = false;
            await Task.Delay(500);
          
            _eventBus.Invoke(new FinishChoosing());
               
        }
    }

    private void ClearAll(RestartGame obj)
    {
        Clear();
    }

    private void Clear()
    {
        _playerSettings.Coins = 0;
        _playerSettings.Speed = 0;
        _playerCoins = 0;
        _enemySettings.Coins = 0;
        _enemySettings.Speed = 0;
        _second = false;
    }
}