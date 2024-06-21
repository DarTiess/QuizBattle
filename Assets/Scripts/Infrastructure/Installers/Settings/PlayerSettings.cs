using UnityEngine;

[System.Serializable]
public class PlayerSettings : IPersonSettings
{
    [SerializeField] private Color color;
    [SerializeField]private string _name;
    private int _coins=0;

    public Color Color => color;
    public string Letter => _name.Substring(0, 1);
    public string Name
    {
        get => _name;
        set
        {
            _coins = 0;
            _name = value;
        }
    }
    public int Coins { get => _coins; set=> _coins = value; }
}