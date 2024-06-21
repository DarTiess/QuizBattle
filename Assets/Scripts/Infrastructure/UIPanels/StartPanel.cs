using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.UIPanels
{
    public class StartPanel : PanelBase
    {
        [SerializeField] private TextMeshProUGUI playerName;
        [SerializeField] private Text enemyName;
        private string _name;
        public override event Action ClickedPanel;
        protected override void OnClickedPanel()
        {
           ClickedPanel?.Invoke();
        }

        public void Initialize(IPersonSettings enemySettings, IPersonSettings playerSettings)
        {
            playerName.text = playerSettings.Name;
            playerName.color =playerSettings.Color;
            _name = enemySettings.Name;
            enemyName.color = enemySettings.Color;
        }

        private void OnEnable()
        {
            HideButton();
            ChooseEnemyName();
        }

        private void ChooseEnemyName()
        {
            enemyName.DOText(_name, 1.5f, true, ScrambleMode.All)
                     .OnComplete(() =>
                     {
                         ShowButton();
                     });
        }
    }
}