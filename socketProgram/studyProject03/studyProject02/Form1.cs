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
        public static UserControl1 userControl1 = new UserControl1();
        public static SubDisplay subDisplay = new SubDisplay();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // delegateの紐づけ
            tClient.OnConnectedEventAction += tClient_OnConnected;
            tClient.OnDisconnectedEventAction += tClient_OnDisconnected;
            tClient.OnReceiveDataAction += tClient_OnReceiveData;

            // ユーザコントロールのリスト(画面のリスト)
            // 初期表示の画面を最初のIndexとして追加
            List<UserControl> userControlList = new List<UserControl>
            {
                userControl1,
                subDisplay
            };
            // パネル初期表示の設定処理
            initPanelSetting(userControlList);
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


        /// <summary>t
        /// パネルコントロールと各ユーザーコントロールの紐付け処理
        /// </summary>
        /// <param name="userControlList">紐づけを行うUserControllのリスト</param>
        private void initPanelSetting(List<UserControl> userControlList)
        {
            // 本当は??のnull演算子を使いたいのになんか怒ってくるから雑魚判定する
            if (userControlList == null)
            {
                Console.WriteLine("Error : userControllList is not exist");
                return;
            }

            // 画面分初期化処理を反映
            foreach (var result in userControlList.Select((panelItem, index) => new { panelItem,　index }))
            {
                // パネルへの登録
                panel1.Controls.Add(result.panelItem);

                // 最初のコントロールではない場合不可視状態とする
                if (result.index != 0) 
                {
                    result.panelItem.Visible = false;
                }
               
            }

        }

    }
}
