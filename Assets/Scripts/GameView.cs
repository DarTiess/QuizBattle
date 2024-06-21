using DefaultNamespace;
using Infrastructure.Level.EventsBus;
using Infrastructure.Level.EventsBus.Signals;
using UI.UIPanels;
using UnityEngine;
using Zenject;

public class GameView : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private EnterPanel enterPanel;
    [SerializeField] private StartPanel startPanel;
    [SerializeField] private GameplayPanel gameplayPanel;
    [SerializeField] private WinPanel winPanel;  
    [SerializeField] private LoosePanel loosePanel;
    private IEventBus _eventBus;
    private EnemySettings _enemySettings;
    private QuestionsSettings _questSettingz;
    private IQuizHandler _quizHandler;
    private PlayerSettings _playerSettings;

    [Inject]
    public void Construct(IEventBus eventBus, IQuizHandler quizHandler, 
                          EnemySettings enemySettings, PlayerSettings playerSettings,
                          QuestionsSettings questionsSettings)
    {
        _eventBus = eventBus;
        _quizHandler = quizHandler;
        _enemySettings = enemySettings;
        _playerSettings = playerSettings;
        _questSettingz = questionsSettings;
    }

    private void Start()
    {
        _eventBus.Subscribe<LevelStart>(OnEnterGame);
        _eventBus.Subscribe<LevelWin>(OnLevelWin);
        _eventBus.Subscribe<LevelLost>(OnLevelLost);
        _eventBus.Subscribe<BotChoose>(OnBotMakeChoose);
        _eventBus.Subscribe<FinishChoosing>(ChooseCompleted);

         _enemySettings.Current();

         enterPanel.ClickedPanel += OnStartGame;
        enterPanel.InputName += OnInputName;
        
        startPanel.ClickedPanel += OnPlayGame;
        gameplayPanel.ClickedPanel += OnPauseGame;
        gameplayPanel.PlayerMakeAnswer += OnPlayerAnswer;
        gameplayPanel.RestartBot += OnRestartBot;
        gameplayPanel.CompleteQuiz += OnCompleteQuiz;
        loosePanel.ClickedPanel += RestartGame;
        winPanel.ClickedPanel += LoadNextLevel;
        EnterGame();
    }

    private void OnCompleteQuiz()
    {
        _quizHandler.CompleteQuiz();
    }

    private void OnDisable()
    {
        _eventBus.Unsubscribe<LevelStart>(OnEnterGame);
        _eventBus.Unsubscribe<LevelWin>(OnLevelWin);
        _eventBus.Unsubscribe<LevelLost>(OnLevelLost);
        _eventBus.Unsubscribe<BotChoose>(OnBotMakeChoose);
        _eventBus.Unsubscribe<FinishChoosing>(ChooseCompleted);

        enterPanel.ClickedPanel -= OnStartGame;
        startPanel.ClickedPanel -= OnPlayGame;
        loosePanel.ClickedPanel -= RestartGame;
        gameplayPanel.ClickedPanel -= OnPauseGame;
        gameplayPanel.PlayerMakeAnswer -= OnPlayerAnswer;
       
        winPanel.ClickedPanel -= LoadNextLevel;
    }

    private void OnRestartBot()
    {
        _quizHandler.StartBot(gameplayPanel.QuestIndex);
    }

    private void OnLevelWin(LevelWin obj)
    {
        Debug.Log("Level Win"); 
        HideAllPanels();
        winPanel.Show();  
    }

    private void OnLevelLost(LevelLost obj)
    {
        Debug.Log("Level Lost");  
        HideAllPanels();
        loosePanel.Show();
    }

    private void OnBotMakeChoose(BotChoose obj)
    {
        gameplayPanel.BotChoose(obj.Choose);
    }

    private void ChooseCompleted(FinishChoosing obj)
    {
        gameplayPanel.DisplayRightAnswer();
    }

    private void OnStartGame() { }

    private void EnterGame()      
    {   
        HideAllPanels();
        enterPanel.Show();
    }

    private void OnPlayerAnswer(bool isRight, int coins)
    {
      
        _quizHandler.PlayerAnswer(isRight, coins);
    }

    private void OnInputName(string name)
    {
       _playerSettings.Name = name;
       
       enterPanel.InputName -= OnInputName;
        startPanel.Initialize(_enemySettings, _playerSettings);
        gameplayPanel.Initialize(_playerSettings, _enemySettings);
       
        HideAllPanels();
       startPanel.Show();
    }

    private void OnEnterGame(LevelStart obj)
    {
        EnterGame();
    }

    private void OnPauseGame()
    {
        _eventBus.Invoke(new PauseGame());
    }

    private void OnPlayGame()
    { 
        _eventBus.Invoke(new PlayGame());
        HideAllPanels();
        gameplayPanel.InitializeQuiz(_questSettingz.Questions);
        gameplayPanel.Show();
        _quizHandler.StartBot(gameplayPanel.QuestIndex);
    }
    private void LoadNextLevel()
    {
        _eventBus.Invoke(new NextLevel());
    }

    private void RestartGame()
    {
        _eventBus.Invoke(new RestartGame());
    }

    private void HideAllPanels()
    {
        enterPanel.Hide();
        startPanel.Hide();
        gameplayPanel.Hide();
        loosePanel.Hide();
        winPanel.Hide();
    }
}
