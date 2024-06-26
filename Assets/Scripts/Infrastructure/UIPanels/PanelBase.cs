﻿using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Infrastructure.UIPanels
{
    public abstract class PanelBase : MonoBehaviour
    {
        [FormerlySerializedAs("_button")]
        [SerializeField] protected Button button;
        public virtual event Action ClickedPanel;

        private void Start()
        {
            button.onClick.AddListener(OnClickedPanel);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        protected virtual void OnClickedPanel()
        {
          
        }

        protected void HideButton()
        {
            button.interactable = false;
        }

        protected void ShowButton()
        {
            button.interactable=true;
        }
    }
}