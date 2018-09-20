using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace studyProject02.Model.TCPModel
{
    class TcpAsyncServer
    {
        private readonly IPEndPoint endPoint;

        public TcpAsyncServer(String port)
        {
            int intPort;
            // portをint型に変換できるか判定
            if (!int.TryParse(port, out intPort))
            {
                Console.WriteLine(" StringPort is Not intParse");
                return;
            }

            Console.WriteLine("Program ThreadID:" + Thread.CurrentThread.ManagedThreadId);
            IPAddress iPAddress = null;
            // v4のIDアドレス取得
            foreach (IPAddress address in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    iPAddress = address;
                    break;
                }

            }
            try
            {
                endPoint = new IPEndPoint(iPAddress, intPort);
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        public void serverStert()
        {
            if (endPoint != null)
            {
                run(endPoint);
            }
        }

        private async Task run(IPEndPoint RunEndPoint)
        {
            TcpListener listner = new TcpListener(RunEndPoint);
            // backlogは一先ず10で。。。
            listner.Start(10);
            while (true)
            {
                System.Net.Sockets.TcpClient client = await listner.AcceptTcpClientAsync();
                Console.WriteLine("Server Accepted");
                // .ConfigureAwait(false);よくわからないからあとでぐぐる
                Task.Run(() => Receive(client));
            }
        }

        private void Receive(System.Net.Sockets.TcpClient client)
        {
            byte[] resByte = new byte[256];
            int resSize = 0;

            using (client)
            using (NetworkStream stream = client.GetStream())
            using (MemoryStream memoryStream = new MemoryStream())
            {
                //do
                //{
                    // でかすぎるデータ(byte配列に収まらん奴)が来たらﾁｰﾝ　一回で送信できるように設計ｵﾅｼｬｽ!!
                resSize = stream.Read(resByte, 0, resByte.Length);

                if (resSize == 0)
                {
                    // クライアントが切断
                    Console.WriteLine("Error Client Disconnected");
                    return;
                }
                memoryStream.Write(resByte, 0, resSize);

                // 読み込んだデータが正しいか判定(改行コード付きコマンドか)
                memoryStream.Seek(-2, SeekOrigin.End);
                if (memoryStream.ReadByte() == '\r' && memoryStream.ReadByte() == '\n')
                {
                    //受信データを文字列に変換
                    string rsvStr = Encoding.UTF8.GetString(memoryStream.ToArray());
                    Console.WriteLine("Received Data = {0}", rsvStr);
                    // ここでイベント発生させる(予定)受信完了イベント
                    // 一応ここでストリームを閉じて、再生成すれば、でかいデータにも対応できなくはない、、、？
                    // 代わりにusing使えなくなるんで、finallyとかの考慮必須
                }
                    //else
                    //{
                    //    // 末尾に移動
                    //    // でかいデータ来ることを考慮するとこの分岐も必要()
                    //    memoryStream.Seek(0, SeekOrigin.End);
                    //}

                //} while (resSize > 0);
            }

        }
    }
}
