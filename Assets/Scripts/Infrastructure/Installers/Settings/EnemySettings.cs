using System;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public class EnemySettings
{
    [SerializeField] private TextAsset enemyNamesFile;

    [SerializeField] private string[] names;
    public string[] Names=>names = enemyNamesFile 
                                       ? enemyNamesFile.text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries) 
                                       : null;

    public string CurrentEnemy
    { 
        get
        {
        int rnd = Random.Range(0, Names.Length);
        Debug.Log(Names[rnd]);
        return Names[rnd];
        }
        
    }
}