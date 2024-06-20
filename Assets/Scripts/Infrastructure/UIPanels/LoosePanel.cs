﻿using System;

namespace UI.UIPanels
{
    public class LoosePanel: PanelBase
    {
        public override event Action ClickedPanel;
        protected override void OnClickedPanel()
        {
            ClickedPanel?.Invoke();
        }
    }
}