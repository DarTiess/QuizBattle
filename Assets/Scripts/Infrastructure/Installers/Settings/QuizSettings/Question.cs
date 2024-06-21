using System;
using UnityEngine;

[System.Serializable]
public struct Question
{
    [SerializeField] private string quest;
    [SerializeField] private Answer[] answers;
    [SerializeField] private int coins;
    public string Quest=>quest;
    public Answer[] Answers=>answers;
    public int Coins => coins;

}