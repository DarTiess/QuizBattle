using UnityEngine;

[System.Serializable]
public class QuestionsList
{
    [SerializeField] private Question[] questions;

    public Question[] Questions=>questions;

}