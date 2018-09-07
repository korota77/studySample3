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
        // シングルトン保存用
        private static UserControl1 _sharedInstace;

        TcpClient tClient = new TcpClient();

        public static UserControl1 sharedIncetance
        {
            get
            {
                return _sharedInstace;
            }
        }

        /// <summary>
        /// コンストラクタ(シングルトン化のため隠蔽)
        /// </summary>
        private UserControl1()
        {
            InitializeComponent();
            tClient.OnConnectedEventAction += tClient_OnConnected;
            tClient.OnDisconnectedEventAction += tClient_OnDisconnected;
            tClient.OnReceiveDataAction += tClient_OnReceiveData;
            tClient.OnErrorEventAction += tClient_OnError;
        }

        /// <summary>
        /// 静的コンストラクタ
        /// </summary>
        static UserControl1()
        {
            _sharedInstace = new UserControl1();
        }

        /// <summary>
        /// サブ画面遷移用メソッド
        /// </summary>
        /// <param name="sender">発生オブジェクト</param>
        /// <param name="e">イベント情報</param>
        private void SubDisplayButtonClick(object sender, EventArgs e)
        {
            // 自身を無効化
            this.Visible = false;
            // サブ画面有効化
            SubDisplay.sharedIncetance.Visible = true;
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
                this.Invoke(new Action<Exception>(this.tClient_OnError), ex);
            }
            else
            {
                MessageBox.Show(ex.Message);
            }
            
            
        }
    }
}
