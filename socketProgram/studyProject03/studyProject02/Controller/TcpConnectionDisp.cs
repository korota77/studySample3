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
    public partial class TcpConnectionDisp : UserControl
    {
        // 画面遷移通知用イベント
        public event Action<String> OnSwitchDisplayAction;

        public TcpConnectionDisp()
        {
            InitializeComponent();
        }
    }
}
