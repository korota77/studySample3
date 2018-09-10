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

        TcpServer tServer;

        // 画面切り替えイベント
        public event Action<string> OnSwitchDisplayAction;

        public UserControl1()
        {
            InitializeComponent();
            MyInitializeComponent();
            tClient.OnConnectedEventAction += tClient_OnConnected;
            tClient.OnDisconnectedEventAction += tClient_OnDisconnected;
            tClient.OnReceiveDataAction += tClient_OnReceiveData;
            tClient.OnErrorEventAction += OnCommonError;


        }

        private void SubDisplayButtonClick(object sender, EventArgs e)
        {
            if (OnSwitchDisplayAction != null)
            {
                // 画面遷移（切替）
                OnSwitchDisplayAction(Constants.DISPLAY_SUBDISPLAY);
            }
            
        }

        private void StertTcpServerOnClick(object sender, EventArgs e)
        {
            try
            {
                Console.WriteLine("Main ThreadID:" + Thread.CurrentThread.ManagedThreadId);
                if (tServer == null)
                {
                    tServer = new TcpServer("60001");
                    tServer.OnServerReceiveAction += tServer_OnReceiveData;
                }
                tServer.init();
            }
            catch (Exception serverException)
            {
                OnCommonError(serverException);

            }
        }

        private void OnTcpServerDisConnect(object sender, EventArgs e)
        {
            if (tServer != null)
            {
                // serverのインスタンスを開放するのは基本このタイミングだけにする(複数のインスタンスができるとエンドポイントの取り合いになる)
                tServer.DisConnect();
                tServer = null;
            }
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
                OnCommonError(ex);
            }
            

        }

        private void SendButtonClick(object sender, EventArgs e)
        {
            try
            {
                tClient.Send(ClientSendData.Text);
            }
            catch (Exception ex)
            {

                OnCommonError(ex);
            }
        }

        private void CloseButtonClick(object sender, EventArgs e)
        {
            if (!tClient.IsClosed)
            {
                tClient.Close();
            }
        }

        private void tServer_OnReceiveData(string msg)
        {
            // データ受信イベント
            Debug.WriteLine("tServer_OnReceiveData" + " ThreadID:" + Thread.CurrentThread.ManagedThreadId);
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action<String>(this.tServer_OnReceiveData), msg);
            }
            else
            {
                serverReceiveData.Text = msg;
            }
            
        }

        private void tClient_OnDisconnected(Object sender, EventArgs e)
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

        void OnCommonError(Exception ex)
        {
            // エラー用イベント
            Debug.WriteLine("tClient_OnError" + " ThreadID:" + Thread.CurrentThread.ManagedThreadId);
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action<Exception>(this.OnCommonError), ex);
            }
            else
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void MyInitializeComponent()
        {
            this.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
        }

    }
}
