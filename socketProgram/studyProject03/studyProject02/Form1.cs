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

        // 画面リスト
        private Dictionary<String, UserControl> displaylist;

        // 現在表示中の画面ID
        private String activeDisplayID;

        public Form1()
        {
            InitializeComponent();
            // 空作成
            displaylist = new Dictionary<string, UserControl>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 初期表示設定
            initPanelSetting();
        }
        /// <summary>
        /// 画面(UserControl)の追加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="newDisplay"></param>
        private void addDisplay(String key, UserControl newDisplay)
        {
            if (key == null || newDisplay == null)
            {
                Console.WriteLine("key or value is not exist");
                return;
            }

            // 指定したKEYまたはValueが存在するか
            if (displaylist.ContainsKey(key) || displaylist.ContainsValue(newDisplay))
            {
                Console.WriteLine("画面ID : " + key + " を追加できませんでした。");
                return;
            }

            // 追加
            displaylist.Add(key, newDisplay);
            // Formへの反映
            panel1.Controls.Add(newDisplay);
            Console.WriteLine("画面ID : " + key + " を追加しました。");

        }

        /// <summary>
        /// 画面(コントロール)の切り替え
        /// </summary>
        /// <param name="target"></param>
        public void OnSwitchDisplay(String target)
        {

            List<string> keyList = new List<string>(displaylist.Keys);
            foreach (String key in keyList)
            {
                if (target.Equals(key))
                {
                    displaylist[key].Visible = true;
                    activeDisplayID = key;
                }
                else
                {
                    displaylist[key].Visible = false;
                }
            }

        }

        private void initPanelSetting()
        {
            UserControl1 userControl1 = new UserControl1();
            userControl1.OnSwitchDisplayAction += OnSwitchDisplay;
            addDisplay(Constants.DISPLAY_USERCONTROL, userControl1);

            SubDisplay subDisplay = new SubDisplay();
            subDisplay.OnSwitchDisplayAction += OnSwitchDisplay;
            addDisplay(Constants.DISPLAY_SUBDISPLAY, subDisplay);

            OnSwitchDisplay(Constants.DISPLAY_USERCONTROL);
        }
    }
}
