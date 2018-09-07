using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace studyProject02
{
    class StudyController
    {
        // 画面リスト
        private Dictionary<String, UserControl> displaylist;

        // 現在表示中の画面ID
        private String activeDisplayID;

        public StudyController()
        {
            // 空作成
            displaylist = new Dictionary<string, UserControl>();
        }

        /**
         * ディスプレイを追加する
         * 
         * @return ディスプレイリスト
         **/
        public void addDisplay(String key,UserControl newDisplay)
        {
            bool chk = true;
            // 重複チェック（ID単位）
            foreach(KeyValuePair<String, UserControl> list in displaylist)
            {
                if (key.Equals(list.Key))
                {
                    chk = false;
                    break;
                }
            }

            if (chk) {
                // 追加
                displaylist.Add(key, newDisplay);
                Console.WriteLine("画面ID : " + key + " を追加しました。" );
            } else {
                Console.WriteLine("画面ID : " + key + " を追加できませんでした。");
            }

        }

        /**
         * ディスプレイリストをセットする
         * 
         **/
        public void setDisplayList(Dictionary<String, UserControl> displayList)
        {
            this.displaylist = displayList;
        }

        /**
         * ディスプレイリストをゲットする
         * 
         * @return ディスプレイリスト
         **/
        public Dictionary<String, UserControl> getDisplayList()
        {
            return this.displaylist;
        }

        /**
         * 表示ディスプレイを切り替える 
         **/
        public void switchDisplay(String target)
        {
            
            List<string> keyList = new List<string>(displaylist.Keys);
            foreach(String key in keyList)
            {
                if (target.Equals(key)) {
                    displaylist[key].Visible = true;
                    activeDisplayID = key;
                } else {
                    displaylist[key].Visible = false;
                }
            }

        }

        public String getActiveDisplayID()
        {
            return activeDisplayID;
        }
    }
}
