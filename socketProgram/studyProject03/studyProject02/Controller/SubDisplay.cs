﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace studyProject02
{
    public partial class SubDisplay : UserControl
    {
        // 画面切り替えイベント
        public event Action<string> OnSwitchDisplayAction;

        public SubDisplay()
        {
            InitializeComponent();
            MyInitializeComponent();
        }

        private void returnButtonOnClick(object sender, EventArgs e)
        {
            // 画面遷移（切替）
            OnSwitchDisplayAction(Constants.DISPLAY_USERCONTROL);
        }

        private void OnTcpConnectionDispTransition(object sender, EventArgs e)
        {
            // 画面遷移 (切替)
            OnSwitchDisplayAction(Constants.DISPLAY_TCPCONNECTIONDISP);
        }

        private void MyInitializeComponent()
        {
            this.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
        }
    }
}