using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace studyProject02
{
    static class ControllerManager
    {
        
        private static StudyController _controller;

        public static StudyController controller
        {
            get
            {
                return _controller;
            }
        }


        /**
         * 基本はエントリポイントでのみ使う。
         * 中身を初期化する。
         **/
        public static void init()
        {
            // singleton pattern.
            _controller = new StudyController();

            // 使用するディスプレイを予め登録
            _controller.addDisplay(Constants.DISPLAY_USERCONTROL, new UserControl1());
            _controller.addDisplay(Constants.DISPLAY_SUBDISPLAY, new SubDisplay());
        }
        
    }
}
