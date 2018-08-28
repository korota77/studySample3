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
    public partial class UserControl1 : UserControl
    {
        // シングルトン保存用
        private static UserControl1 _sharedInstace;

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
    }
}
