using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Infrastructure.Installers.Settings
{
    [System.Serializable]
    public class EnemySettings: IPersonSettings
    {
        [SerializeField] private TextAsset enemyNamesFile;
        [SerializeField] private Color color;
        [SerializeField] private int maxTimeToAnswer;
        [SerializeField] private string[] names;

        private string _currentEnemy;
        private int _coins=0;
        [SerializeField] private int _speed;
        public string[] Names
        {
            get
            {
                return names = enemyNamesFile
                                   ? enemyNamesFile.text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                                   : null;
            }
        }

        public Color Color => color;

        public string Letter => _currentEnemy.Substring(0, 1);

        public string Name => _currentEnemy;
        public int Coins { get => _coins; set => _coins = value; }
        public int Speed { get=>_speed; set=>_speed=value; }
        public int MaxTimeToAnswer => maxTimeToAnswer*1000;

        public void Current()
        {
            int rnd = Random.Range(0, Names.Length);
            _coins = 0;
            _currentEnemy= Names[rnd];
        }
    }
}