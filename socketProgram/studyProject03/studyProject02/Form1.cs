using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace studyProject02
{
    public partial class Form1 : Form
    {
        TcpClient tClient = new TcpClient();

        public Form1()
        {
            InitializeComponent();
            // 初期化
            ControllerManager.init();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // delegateの紐づけ
            tClient.OnConnectedEventAction += tClient_OnConnected;
            tClient.OnDisconnectedEventAction += tClient_OnDisconnected;
            tClient.OnReceiveDataAction += tClient_OnReceiveData;

            // パネル初期表示の設定処理
            ControllerManager.controller.switchDisplay(Values.DISPLAY_USERCONTROL);
        }

        /** 接続OKイベント **/
        void tClient_OnConnected(EventArgs e)
        {
            //接続OK処理
        }
        void tClient_OnDisconnected(object sender, EventArgs e)
        {
            // 接続断処理
        }
        void tClient_(EventArgs e)
        {
            //接続OK処理
        }
        void tClient_OnReceiveData(object sender, string e)
        {

        }
    }
}
