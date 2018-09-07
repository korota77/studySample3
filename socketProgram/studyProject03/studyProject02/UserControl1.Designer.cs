namespace studyProject02
{
    partial class UserControl1
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 69.41177F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.58824F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 213F));
            this.tableLayoutPanel1.Controls.Add(this.button3, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.button2, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.button1, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.button4, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.button5, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.button6, 2, 6);
            this.tableLayoutPanel1.Controls.Add(this.textBox1, 1, 5);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.52632F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 89.47369F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 63F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 61F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 66F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 58F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(683, 435);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(472, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(182, 39);
            this.button1.TabIndex = 0;
            this.button1.Text = "サブ画面行くよぼたん";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.SubDisplayButtonClick);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(472, 73);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(182, 39);
            this.button2.TabIndex = 1;
            this.button2.Text = "ダイアログだそうかなボタン";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2OnClick);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(472, 136);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(182, 39);
            this.button3.TabIndex = 2;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.TestMethodSample);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(472, 197);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(182, 41);
            this.button4.TabIndex = 3;
            this.button4.Text = "コネクト";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.ConnectButtonClick);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(472, 257);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(182, 43);
            this.button5.TabIndex = 4;
            this.button5.Text = "送信";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.SendButtonClick);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(472, 313);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(182, 46);
            this.button6.TabIndex = 5;
            this.button6.Text = "切断";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.CloseButtonClick);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(329, 257);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(123, 19);
            this.textBox1.TabIndex = 6;
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(686, 440);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.TextBox textBox1;
    }
}
