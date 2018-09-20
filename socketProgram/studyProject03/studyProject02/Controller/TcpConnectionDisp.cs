using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using studyProject02.Model.TCPModel;

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

        private void button1_Click(object sender, EventArgs e)
        {
            TcpAsyncServer server = new TcpAsyncServer("50001");
            server.serverStert();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OnSwitchDisplayAction?.Invoke(Constants.DISPLAY_USERCONTROL);
        }
    }
}
