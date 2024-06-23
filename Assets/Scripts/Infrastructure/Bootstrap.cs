using Infrastructure.EventsBus;
using Infrastructure.EventsBus.Signals;
using Infrastructure.Level;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Infrastructure
{
    public class Bootstrap: MonoBehaviour
    {
       [FormerlySerializedAs("_winEffect")]
       [SerializeField] private ParticleSystem winEffect;
       [FormerlySerializedAs("_looseEffect")]
       [SerializeField] private ParticleSystem looseEffect;
        private IEventBus _eventBus;
        private LevelLoader _levelLoader;

        [Inject]
        public void Construct(IEventBus eventBus, LevelLoader levelLoader)
        {
            _eventBus = eventBus;
            _levelLoader = levelLoader;
        }

        private void Awake()
        {
            _eventBus.Subscribe<LevelWin>(OnLevelWin);
            _eventBus.Subscribe<LevelLost>(OnLevelLost);
            
            DontDestroyOnLoad(this);
        }

        private void OnLevelLost(LevelLost obj)
        {
            looseEffect.Play();
        }

        private void OnLevelWin(LevelWin obj)
        {
            winEffect.Play();
        }
    }
}