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

        public void InitializeEnemy(string enemy)
        {
            _name = enemy;
        }

        public void InitializePlayer(string player)
        {
            playerName.text = player;
        }
    }
}