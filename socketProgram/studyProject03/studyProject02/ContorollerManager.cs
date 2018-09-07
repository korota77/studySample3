using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace studyProject02
{
    class ControllerManager
    {
        // シングルトンなcontroller
        private static StudyController controller;

        public ControllerManager()
        {
            controller = new StudyController();
            controller.addDisplay(DISPLAY_USERCONTROL, new UserControl1());
            controller.addDisplay(DISPLAY_SUBDISPLAY, new SubDisplay());

            // 初期表示
            controller.switchDisplay(DISPLAY_USERCONTROL);
        }
    }
}
