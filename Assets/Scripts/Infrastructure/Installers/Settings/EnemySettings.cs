using System;
using ModestTree;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public class EnemySettings: IPersonSettings
{
    [SerializeField] private TextAsset enemyNamesFile;
    [SerializeField] private Color color;

    [SerializeField] private string[] names;
    private string _currentEnemy;
    private int _coins=0;
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

    public void Current()
    {
        int rnd = Random.Range(0, Names.Length);
        _coins = 0;
        _currentEnemy= Names[rnd];
    }
}