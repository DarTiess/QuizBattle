using UnityEngine;

[System.Serializable]
public class Question
{
    [SerializeField] private string quest;
    [SerializeField] private string[] answers;
    [SerializeField] private int rightAnswerNumber;
    
    public string Quest=>quest;
    public string[] Answers=>answers;
    public int RightAnswerNumber=>rightAnswerNumber;
}