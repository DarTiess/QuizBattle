using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UI.UIPanels
{
    public class WinPanel: PanelBase
    {

        [SerializeField] private TextMeshProUGUI name;
        [SerializeField] private TextMeshProUGUI coins;
        public override event Action ClickedPanel;
        protected override void OnClickedPanel()
        {
            ClickedPanel?.Invoke();
        }

        public void Initialize(IPersonSettings playerSettings)
        {
            name.text = playerSettings.Name;
            coins.text = playerSettings.Coins.ToString();

            coins.transform.DOShakeRotation(0.5f, 90f);
        }
    }
}