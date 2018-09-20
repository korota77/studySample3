using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace studyProject02
{
    class TcpClient
    {
        /** プライベート変数 **/
        // Socket
        private Socket mySocket = null;

        // 受信データ保存用
        private MemoryStream myMs;

        // ロック用
        private readonly object syncLock = new object();

        // 送受信文字列エンコード
        private Encoding enc = Encoding.UTF8;

        /** イベント **/
        // データ受信イベント
        public event Action<object, string> OnReceiveDataAction;

        // 接続断イベント
        public event Action<object, EventArgs> OnDisconnectedEventAction;

        // 接続OKイベント
        public event Action<EventArgs> OnConnectedEventAction;

        // エラー用イベント
        public event Action<Exception> OnErrorEventAction;


        /** プロパティ **/
        /// <summary>
        /// ソケットが閉じているか
        /// </summary>
        public bool IsClosed
        {
            get { return (mySocket == null); }
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public virtual void Dispose()
        {
            //Socketを閉じる
            Close();
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TcpClient()
        {
        }
        public TcpClient(Socket sc)
        {
            mySocket = sc;
        }

        /// <summary>
        /// SocketClose
        /// </summary>
        public void Close()
        {
            Debug.WriteLine("Close" + " ThreadID:" + Thread.CurrentThread.ManagedThreadId);

            //Socketを無効
            mySocket.Shutdown(SocketShutdown.Both);
            //Socketを閉じる
            mySocket.Close();
            mySocket = null;

            //受信データStreamを閉じる
            if (myMs != null)
            {
                myMs.Close();
                myMs = null;
            }

            //接続断イベント発生
            OnDisconnectedEventAction?.Invoke(this, new EventArgs());
        }
        /// <summary>
        /// Hostに接続
        /// </summary>
        /// <param name="host">接続先ホスト</param>
        /// <param name="port">ポート</param>
        public void Connect(string host, int port)
        {
            //Socket生成
            mySocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Debug.WriteLine("Connect" + " ThreadID:" + Thread.CurrentThread.ManagedThreadId);
            IPAddress ipAddress = null;

            foreach (IPAddress address in Dns.GetHostAddresses(host))
            {
                // IPv4のみ取得
                if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    ipAddress = address;
                }
            }
            //IP作成
            IPEndPoint ipEnd = new IPEndPoint(ipAddress, port);
            //ホストに接続
            try
            {
                // Connect to the remote endpoint.
                mySocket.BeginConnect(ipEnd,
                    new AsyncCallback(ConnectCallback), mySocket);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            Debug.WriteLine("ConnectCallback" + " ThreadID:" + Thread.CurrentThread.ManagedThreadId);
            try
            {
                // Retrieve the socket from the state object.
                Socket client = (Socket)ar.AsyncState;

                // Complete the connection.
                client.EndConnect(ar);

                Console.WriteLine("Socket connected to {0}",
                    client.RemoteEndPoint.ToString());

                //接続OKイベント発生
                OnConnectedEventAction?.Invoke(new EventArgs());
                //データ受信開始
                StartReceive();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                // エラーイベント発生
                OnErrorEventAction?.Invoke(e);
            }
        }
        /// <summary>
        /// データ受信開始
        /// </summary>
        public void StartReceive()
        {
            Debug.WriteLine("StartReceive" + " ThreadID:" + Thread.CurrentThread.ManagedThreadId);

            //受信バッファ
            byte[] rcvBuff = new byte[1024];
            //受信データ初期化
            myMs = new MemoryStream();
            try
            {
                //非同期データ受信開始
                mySocket.BeginReceive(rcvBuff, 0, rcvBuff.Length, SocketFlags.None, new AsyncCallback(ReceiveDataCallback), rcvBuff);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                // エラーイベント発生
                OnErrorEventAction?.Invoke(e);
            }
            
        }

        /// <summary>
        /// 非同期データ受信
        /// </summary>
        /// <param name="ar"></param>
        private void ReceiveDataCallback(IAsyncResult ar)
        {
            Debug.WriteLine("ReceiveDataCallback" + " ThreadID:" + Thread.CurrentThread.ManagedThreadId);

            int len = -1;
            lock (syncLock)
            {
                if (IsClosed)
                    return;

                //データ受信終了
                len = mySocket.EndReceive(ar);
            }

            //切断された
            if (len <= 0)
            {
                Close();
                return;
            }

            //受信データ取り出し
            byte[] rcvBuff = (byte[])ar.AsyncState;
            try
            {
                //受信データ保存
                myMs.Write(rcvBuff, 0, len);

                if (myMs.Length >= 2)
                {
                    //\r\nかチェック
                    myMs.Seek(-2, SeekOrigin.End);
                    if (myMs.ReadByte() == '\r' && myMs.ReadByte() == '\n')
                    {
                        //受信データを文字列に変換
                        string rsvStr = enc.GetString(myMs.ToArray());
                        //受信データ初期化
                        myMs.Close();
                        myMs = new MemoryStream();
                        //データ受信イベント発生
                        OnReceiveDataAction?.Invoke(this, rsvStr);

                    }
                    else
                    {
                        //ストリーム位置を戻す
                        myMs.Seek(0, SeekOrigin.End);
                    }
                }
            }
            catch (Exception)
            {

                myMs.Close();
            }

            lock (syncLock)
            {
                //非同期受信を再開始
                if (!IsClosed)
                    mySocket.BeginReceive(rcvBuff, 0, rcvBuff.Length, SocketFlags.None, new AsyncCallback(ReceiveDataCallback), rcvBuff);
            }
        }

        /// <summary>
        /// メッセージを送信する
        /// </summary>
        /// <param name="str"></param>
        public void Send(string str)
        {
            Debug.WriteLine("Send" + " ThreadID:" + Thread.CurrentThread.ManagedThreadId);

            if (!IsClosed && mySocket!=null)
            {
                //文字列をBYTE配列に変換
                byte[] sendBytes = enc.GetBytes(str + "\r\n");
                try
                {
                    lock (syncLock)
                    {
                        //送信
                        mySocket.Send(sendBytes);
                    }
                }
                catch (Exception)
                {

                    throw;
                }

            }
        }

    }
}