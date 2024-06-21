using UnityEngine;

[System.Serializable]
public struct QuestionsList
{
    [SerializeField] private Question[] questions;

    public Question[] Questions=>questions;

}