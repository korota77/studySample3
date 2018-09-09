using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Diagnostics;
using System.Threading;

namespace studyProject02
{
    public partial class UserControl1 : UserControl
    {
        TcpClient tClient = new TcpClient();

        // 画面切り替えイベント
        public event Action<string> OnSwitchDisplayAction;

        public UserControl1()
        {
            InitializeComponent();
            tClient.OnConnectedEventAction += tClient_OnConnected;
            tClient.OnDisconnectedEventAction += tClient_OnDisconnected;
            tClient.OnReceiveDataAction += tClient_OnReceiveData;
            tClient.OnErrorEventAction += tClient_OnError;
        }

        private void SubDisplayButtonClick(object sender, EventArgs e)
        {
            if (OnSwitchDisplayAction != null)
            {
                // 画面遷移（切替）
                OnSwitchDisplayAction(Constants.DISPLAY_SUBDISPLAY);
            }
            
        }

        private void Button2OnClick(object sender, EventArgs e)
        {
            MessageBox.Show("でたーー！！ダイアログ", "これがダイアログ", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
        }

        private void TestMethodSample(object sender, EventArgs e)
        {
            indexer_sample dic = new indexer_sample();
            dic["ﾊｧ"] = "( ﾟДﾟ)？";
            dic["ﾊｧﾊｧ"] = "(;´Д｀)";
            dic["ﾎﾟｶｰﾝ"] = "( ﾟдﾟ)";
            dic["ｵﾏｴﾓﾅ"] = "(´∀｀)";

            Console.Write(dic["ﾊｧﾊｧ"]);

        }

        private void ConnectButtonClick(object sender, EventArgs e)
        {
            String hostName = Dns.GetHostName();
            int port = 60001;
            try
            {
                tClient.Connect(hostName, port);
            }
            catch (Exception ex)
            {
                tClient_OnError(ex);
            }
            

        }

        private void SendButtonClick(object sender, EventArgs e)
        {
            try
            {
                tClient.Send(textBox1.Text);
            }
            catch (Exception ex)
            {

                tClient_OnError(ex);
            }
        }

        private void CloseButtonClick(object sender, EventArgs e)
        {
            if (!tClient.IsClosed)
            {
                tClient.Close();
            }
        }

        void tClient_OnDisconnected(Object sender, EventArgs e)
        {
            // 接続断イベント
            Debug.WriteLine("tClient_OnDisconnected" + " ThreadID:" + Thread.CurrentThread.ManagedThreadId);
        }

        void tClient_OnConnected(EventArgs e)
        {
            // 接続OKイベント
            Debug.WriteLine("tClient_OnConnected" + " ThreadID:" + Thread.CurrentThread.ManagedThreadId);
        }

        void tClient_OnReceiveData(Object sender, string e)
        {
            // データ受信イベント
            Debug.WriteLine("tClient_OnReceiveData" + " ThreadID:" + Thread.CurrentThread.ManagedThreadId);
        }

        void tClient_OnError(Exception ex)
        {
            // エラー用イベント
            Debug.WriteLine("tClient_OnError" + " ThreadID:" + Thread.CurrentThread.ManagedThreadId);
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action<Exception>(this.tClient_OnError), ex);
            }
            else
            {
                MessageBox.Show(ex.Message);
            }
            
            
        }
    }
}
