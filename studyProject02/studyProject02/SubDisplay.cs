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
    public partial class SubDisplay : UserControl
    {
        // シングルトン格納用(シングルトン化のため隠蔽)
        private static SubDisplay _sharedInstance;

        public static SubDisplay sharedIncetance
        {
            get
            {
                return _sharedInstance;
            }
        }

        /// <summary>
        /// コンストラクタ(シングルトン化のため、コンストラクタはprivateで宣言)
        /// </summary>
        private SubDisplay()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 静的コンストラクタ
        /// </summary>
        static SubDisplay()
        {
            _sharedInstance = new SubDisplay();
        }

        private void returnButtonOnClick(object sender, EventArgs e)
        {
            // 自身を無効化
            this.Visible = false;
            // メインのほう有効化
            UserControl1.sharedIncetance.Visible = true;
        }
    }
}
