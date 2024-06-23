using UnityEngine;

namespace Infrastructure.Installers.Settings
{
    [System.Serializable]
    public class PlayerSettings : IPersonSettings
    {
        [SerializeField] private Color color;
        [SerializeField] private int maxTimeToAnswer;

        private string _name;
        private int _coins=0;
        [SerializeField] private int _speed;

        public Color Color => color;
        public string Letter => _name.Substring(0, 1);
        public string Name { get => _name; set=> _name = value;  }
        public int Coins { get => _coins; set=> _coins = value; }
        public int Speed { get=>_speed; set => _speed = value; }
        public int MaxTimeToAnswer => maxTimeToAnswer;
    }
}