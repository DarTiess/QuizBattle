using System;
using Infrastructure.Level;
using Infrastructure.Level.EventsBus;
using Infrastructure.Level.EventsBus.Signals;
using UnityEngine;
using Zenject;

namespace DefaultNamespace.Infrastructure
{
    public class Bootstrap: MonoBehaviour
    {
       [SerializeField] private ParticleSystem _winEffect;
       [SerializeField] private ParticleSystem _looseEffect;
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
            //_winEffect = gameObject.GetComponentInChildren<ParticleSystem>();
            
            DontDestroyOnLoad(this);
        }

        private void OnLevelLost(LevelLost obj)
        {
            _looseEffect.Play();
        }

        private void OnLevelWin(LevelWin obj)
        {
            _winEffect.Play();
        }
    }
}