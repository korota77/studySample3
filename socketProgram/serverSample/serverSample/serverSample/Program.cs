using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace serverSample
{
    public class Program
    {

        private static ManualResetEvent SocketEvent = new ManualResetEvent(false);

        private IPEndPoint ipEndPoint;

        private Socket sock;

        private bool IsOpenSoket = true;

        public Program(String port)
        {
            Console.WriteLine("Program ThreadID:" + Thread.CurrentThread.ManagedThreadId);
            IPAddress myIP = null;
            // v4のIDアドレス取得
            foreach (IPAddress address in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    myIP = address;
                    break;
                }

            }
            // IPアドレスとポート番号からエンドポイント作成
            ipEndPoint = new IPEndPoint(myIP, Int32.Parse(port));
        }

        public void init()
        {
            Console.WriteLine("init ThreadID:" + Thread.CurrentThread.ManagedThreadId);
            // ソケットの作成
            sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                // エンドポイントへのバインド
                sock.Bind(ipEndPoint);
                // リッスン状態にする(バックログ10[キューの受付が10個まで11個目以降は受け付けない])
                sock.Listen(10);
            }
            catch (Exception)
            {
                // 例外処理(書くのめんどいからまた今度)
            }

            Console.WriteLine("サーバー起動中・・・");
            // 別スレッドで通信の接続を受信するまで待機
            Task.Run(() => Round());
        }

        void Round()
        {

            Console.WriteLine("Round ThreadID:" + Thread.CurrentThread.ManagedThreadId);
            while (IsOpenSoket)
            {
                SocketEvent.Reset();
                Console.WriteLine("テステス");
                try
                {
                    sock.BeginAccept(new AsyncCallback(OnConnectRequest), sock);
                }
                catch (Exception)
                {

                    Console.WriteLine("Error:BeginAccept");
                }

                SocketEvent.WaitOne();

            }

        }

        void OnConnectRequest(IAsyncResult ar)
        {
            Console.WriteLine("OnConnectRequest ThreadID:" + Thread.CurrentThread.ManagedThreadId);
            // 接続待機中のスレッドブロックを解除
            SocketEvent.Set();
            // ソケットが開いているか
            if (IsOpenSoket == false)
            {
                return;
            }
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);
            Console.WriteLine(handler.RemoteEndPoint.ToString() + " joined");
            StateObject state = new StateObject();
            state.workSocket = handler;
            handler.BeginReceive(state.buffer, 0, StateObject.BUFFER_SIZE, 0, new AsyncCallback(ReadCallback), state);

        }

        void ReadCallback(IAsyncResult ar)
        {
            Console.WriteLine("ReadCallback ThreadID:" + Thread.CurrentThread.ManagedThreadId);
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;
            int ReadSize = 0;
            try
            {
                ReadSize = handler.EndReceive(ar);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:{0}", ex);

                return;
            }
            if (ReadSize < 1)
            {
                Console.WriteLine(handler.RemoteEndPoint.ToString() + " disconnected");
                return;
            }
            byte[] bb = new byte[ReadSize];
            Array.Copy(state.buffer, bb, ReadSize);
            string msg = System.Text.Encoding.UTF8.GetString(bb);
            Console.WriteLine(msg);
            handler.BeginSend(bb, 0, bb.Length, 0, new AsyncCallback(WriteCallback), state);
        }

        void WriteCallback(IAsyncResult ar)
        {
            Console.WriteLine("WriteCallback ThreadID:" + Thread.CurrentThread.ManagedThreadId);
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;
            handler.EndSend(ar);
            Console.WriteLine("送信完了");
            handler.BeginReceive(state.buffer, 0, StateObject.BUFFER_SIZE, 0, new AsyncCallback(ReadCallback), state);
        }

        public void DisConnect()
        {
            Console.WriteLine("disConnect ThreadID:" + Thread.CurrentThread.ManagedThreadId);
            IsOpenSoket = false;
            sock.Close();
        }
    }
}
