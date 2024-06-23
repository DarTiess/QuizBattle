using System;
using DG.Tweening;
using Infrastructure.Installers.Settings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure.UIPanels
{
    public class WinPanel: PanelBase
    {

        [SerializeField] private TextMeshProUGUI name;
        [SerializeField] private TextMeshProUGUI coins;
        [SerializeField] private TextMeshProUGUI speed;
        [SerializeField] private Text complete;
        public override event Action ClickedPanel;
        protected override void OnClickedPanel()
        {
            ClickedPanel?.Invoke();
        }

        public void Initialize(float animationDuration,float rotateAngle, IPersonSettings playerSettings)
        {
            name.text = playerSettings.Name;
            coins.text = playerSettings.Coins.ToString();
            speed.text = playerSettings.Speed.ToString();
            

            coins.transform.DOShakeRotation(animationDuration, rotateAngle);

            int comp = playerSettings.Speed + playerSettings.Coins;
            complete.DOText(comp.ToString(),animationDuration);
        }
    }
}