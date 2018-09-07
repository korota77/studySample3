using System;
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
        public SubDisplay()
        {
            InitializeComponent();
        }

        private void returnButtonOnClick(object sender, EventArgs e)
        {
            // 画面遷移（切替）
            ControllerManager.controller.switchDisplay(Values.DISPLAY_USERCONTROL);
        }
    }
}
