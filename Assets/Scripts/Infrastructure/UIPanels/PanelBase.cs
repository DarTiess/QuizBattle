using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.UIPanels
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

        protected void HideButton()
        {
            Debug.Log("Hide btn");
            button.interactable = false;
        }

        protected void ShowButton()
        {
            button.interactable=true;
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
    }
}