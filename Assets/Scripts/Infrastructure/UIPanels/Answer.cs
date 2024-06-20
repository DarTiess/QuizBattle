using TMPro;
using UnityEngine;

public class Answer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;


    public void Initialize(string answer)
    {
        text.text = answer;
    }
}