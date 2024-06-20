using System;
using Infrastructure.Level.EventsBus;
using Infrastructure.Level.EventsBus.Signals;
using UI.UIPanels;
using UnityEngine;
using Zenject;

public class Game : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private EnterPanel enterPanel;
    [SerializeField] private StartPanel startPanel;
    [SerializeField] private GameplayPanel gameplayPanel;
    [SerializeField] private WinPanel winPanel;  
    [SerializeField] private LoosePanel loosePanel;
    private IEventBus _eventBus;
    private EnemySettings _enemySettings;
    private string _playerName;
    private string _enemyName;
    private QuestionsSettings _questSettingz;

    [Inject]
    public void Construct(IEventBus eventBus, EnemySettings enemySettings,
                          QuestionsSettings questionsSettings)
    {
        _eventBus = eventBus;
        _enemySettings = enemySettings;
        _questSettingz = questionsSettings;
    }

    private void Start()
    {
        _eventBus.Subscribe<LevelStart>(OnEnterGame);
        _eventBus.Subscribe<LevelWin>(OnLevelWin);
        _eventBus.Subscribe<LevelLost>(OnLevelLost);

        _enemyName = _enemySettings.CurrentEnemy;
        
        startPanel.InitializeEnemy(_enemyName);
       

        enterPanel.ClickedPanel += OnStartGame;
        enterPanel.InputName += OnInputName;
        startPanel.ClickedPanel += OnPlayGame;
        gameplayPanel.ClickedPanel += OnPauseGame;
        loosePanel.ClickedPanel += RestartGame;
        winPanel.ClickedPanel += LoadNextLevel;
        EnterGame();
    }

    private void OnInputName(string name)
    {
        _playerName = name;
        startPanel.InitializePlayer(_playerName);
        gameplayPanel.Initialize(_playerName, _enemyName);
    }

    private void OnEnterGame(LevelStart obj)
    {
        EnterGame();
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

    private void OnStartGame()
    {
        HideAllPanels();
        startPanel.Show();
    }

    private void EnterGame()      
    {   
        HideAllPanels();
        enterPanel.Show();
    }

    private void OnDisable()
    {
        _eventBus.Unsubscribe<LevelStart>(OnEnterGame);
        _eventBus.Unsubscribe<LevelWin>(OnLevelWin);
        _eventBus.Unsubscribe<LevelLost>(OnLevelLost);

        enterPanel.ClickedPanel -= OnStartGame;
        startPanel.ClickedPanel -= OnPlayGame;
        loosePanel.ClickedPanel -= RestartGame;
        gameplayPanel.ClickedPanel -= OnPauseGame;
        winPanel.ClickedPanel -= LoadNextLevel;
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
