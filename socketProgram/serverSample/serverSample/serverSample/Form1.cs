using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace serverSample
{
    public partial class Form1 : Form
    {
        Program prog;
        public Form1()
        {
            InitializeComponent();
        }

        private void serverStart(object sender, EventArgs e)
        {
            try
            {
                Console.WriteLine("Main ThreadID:" + Thread.CurrentThread.ManagedThreadId);
                prog = new Program("60001");
                prog.init();
            }
            catch (Exception)
            {
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (prog != null)
            {
                // ソケット通信終了処理
                prog.DisConnect();
                // 解放
                prog = null;
            }
            
        }
    }
}
