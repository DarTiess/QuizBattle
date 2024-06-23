using System;
using DG.Tweening;
using Infrastructure.Installers.Settings;
using Infrastructure.Installers.Settings.QuizSettings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure.UIPanels
{
    public class AnswerView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI answerText;
        [SerializeField] private TextMeshProUGUI enemyPoint;
        [SerializeField] private TextMeshProUGUI playerPoint;
        [SerializeField] private Color rightColor;
        [SerializeField] private Color startColor;
       
        private Button _button;
        private Image _backGround;
        private Answer _answer;
      
        public event Action<bool> MakeAnswer;

        private void OnEnable()
        {
            _backGround = GetComponent<Image>();
            _backGround.color = startColor;
        }
        public void Initialize(Answer answer,float animationDuration, IPersonSettings playerSettings, IPersonSettings enemySettings)
        {
            _answer = answer;
            answerText.text = answer.Description;
            _backGround = GetComponent<Image>();
            _backGround.color = startColor;
      
            _button = GetComponent<Button>();
            _button.onClick.AddListener(PlayerMakeAnswer);
            SwitchEnable(true);
      
            answerText.transform.DOScale(Vector3.zero, animationDuration).SetLoops(2,LoopType.Yoyo);
            SetPoints(playerPoint,playerSettings);
            SetPoints(enemyPoint, enemySettings);
        }

        public void SwitchEnable(bool isActive)
        {
            _button.interactable = isActive;
        }

        public void BotChoose()
        {
            ShowObject(enemyPoint);
        }

        public void DisplayRight()
        {
            _backGround.color = rightColor;
        }

        private void SetPoints(TextMeshProUGUI point,IPersonSettings personSettings)
        {
            point.color = personSettings.Color;
            point.text = personSettings.Letter;
            HideObject(point);
        }

        private void HideObject(TextMeshProUGUI textMeshProUGUI)
        {
            textMeshProUGUI.gameObject.SetActive(false);
        }

        private void PlayerMakeAnswer()
        {
            _button.onClick.RemoveListener(PlayerMakeAnswer);
            ShowObject(playerPoint);
            MakeAnswer?.Invoke(_answer.RightVariant);
        }

        private void ShowObject(TextMeshProUGUI textMeshProUGUI)
        {
            textMeshProUGUI.gameObject.SetActive(true);
        }
    }
}