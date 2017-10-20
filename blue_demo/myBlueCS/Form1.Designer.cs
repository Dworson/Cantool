namespace myBlueCS
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonSend = new System.Windows.Forms.Button();
            this.labelInfo = new System.Windows.Forms.Label();
            this.buttonSelectFile = new System.Windows.Forms.Button();
            this.labelPath = new System.Windows.Forms.Label();
            this.buttonSelectBluetooth = new System.Windows.Forms.Button();
            this.labelAddress = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonListen = new System.Windows.Forms.Button();
            this.labelRecInfo = new System.Windows.Forms.Label();
            this.buttonselectRecDir = new System.Windows.Forms.Button();
            this.labelRecDir = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonSend);
            this.groupBox1.Controls.Add(this.labelInfo);
            this.groupBox1.Controls.Add(this.buttonSelectFile);
            this.groupBox1.Controls.Add(this.labelPath);
            this.groupBox1.Controls.Add(this.buttonSelectBluetooth);
            this.groupBox1.Controls.Add(this.labelAddress);
            this.groupBox1.Location = new System.Drawing.Point(32, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(405, 153);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "发送";
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(297, 96);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(75, 23);
            this.buttonSend.TabIndex = 5;
            this.buttonSend.Text = "发送";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Location = new System.Drawing.Point(32, 92);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(53, 12);
            this.labelInfo.TabIndex = 4;
            this.labelInfo.Text = "发送信息";
            // 
            // buttonSelectFile
            // 
            this.buttonSelectFile.Location = new System.Drawing.Point(297, 67);
            this.buttonSelectFile.Name = "buttonSelectFile";
            this.buttonSelectFile.Size = new System.Drawing.Size(75, 23);
            this.buttonSelectFile.TabIndex = 3;
            this.buttonSelectFile.Text = "选择文件";
            this.buttonSelectFile.UseVisualStyleBackColor = true;
            this.buttonSelectFile.Click += new System.EventHandler(this.buttonSelectFile_Click);
            // 
            // labelPath
            // 
            this.labelPath.AutoSize = true;
            this.labelPath.Location = new System.Drawing.Point(32, 63);
            this.labelPath.Name = "labelPath";
            this.labelPath.Size = new System.Drawing.Size(53, 12);
            this.labelPath.TabIndex = 2;
            this.labelPath.Text = "文件路径";
            // 
            // buttonSelectBluetooth
            // 
            this.buttonSelectBluetooth.Location = new System.Drawing.Point(297, 36);
            this.buttonSelectBluetooth.Name = "buttonSelectBluetooth";
            this.buttonSelectBluetooth.Size = new System.Drawing.Size(75, 23);
            this.buttonSelectBluetooth.TabIndex = 1;
            this.buttonSelectBluetooth.Text = "选择蓝牙";
            this.buttonSelectBluetooth.UseVisualStyleBackColor = true;
            this.buttonSelectBluetooth.Click += new System.EventHandler(this.buttonSelectBluetooth_Click);
            // 
            // labelAddress
            // 
            this.labelAddress.AutoSize = true;
            this.labelAddress.Location = new System.Drawing.Point(32, 32);
            this.labelAddress.Name = "labelAddress";
            this.labelAddress.Size = new System.Drawing.Size(53, 12);
            this.labelAddress.TabIndex = 0;
            this.labelAddress.Text = "蓝牙地址";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonListen);
            this.groupBox2.Controls.Add(this.labelRecInfo);
            this.groupBox2.Controls.Add(this.buttonselectRecDir);
            this.groupBox2.Controls.Add(this.labelRecDir);
            this.groupBox2.Location = new System.Drawing.Point(32, 187);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(405, 96);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "接收";
            // 
            // buttonListen
            // 
            this.buttonListen.Location = new System.Drawing.Point(297, 67);
            this.buttonListen.Name = "buttonListen";
            this.buttonListen.Size = new System.Drawing.Size(75, 23);
            this.buttonListen.TabIndex = 5;
            this.buttonListen.Text = "监听";
            this.buttonListen.UseVisualStyleBackColor = true;
            this.buttonListen.Click += new System.EventHandler(this.buttonListen_Click);
            // 
            // labelRecInfo
            // 
            this.labelRecInfo.AutoSize = true;
            this.labelRecInfo.Location = new System.Drawing.Point(32, 63);
            this.labelRecInfo.Name = "labelRecInfo";
            this.labelRecInfo.Size = new System.Drawing.Size(53, 12);
            this.labelRecInfo.TabIndex = 4;
            this.labelRecInfo.Text = "监听信息";
            // 
            // buttonselectRecDir
            // 
            this.buttonselectRecDir.Location = new System.Drawing.Point(297, 20);
            this.buttonselectRecDir.Name = "buttonselectRecDir";
            this.buttonselectRecDir.Size = new System.Drawing.Size(75, 23);
            this.buttonselectRecDir.TabIndex = 3;
            this.buttonselectRecDir.Text = "接收目录";
            this.buttonselectRecDir.UseVisualStyleBackColor = true;
            this.buttonselectRecDir.Click += new System.EventHandler(this.buttonselectRecDir_Click);
            // 
            // labelRecDir
            // 
            this.labelRecDir.AutoSize = true;
            this.labelRecDir.Location = new System.Drawing.Point(32, 16);
            this.labelRecDir.Name = "labelRecDir";
            this.labelRecDir.Size = new System.Drawing.Size(53, 12);
            this.labelRecDir.TabIndex = 2;
            this.labelRecDir.Text = "接收文件";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 292);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.Button buttonSelectFile;
        private System.Windows.Forms.Label labelPath;
        private System.Windows.Forms.Button buttonSelectBluetooth;
        private System.Windows.Forms.Label labelAddress;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonListen;
        private System.Windows.Forms.Label labelRecInfo;
        private System.Windows.Forms.Button buttonselectRecDir;
        private System.Windows.Forms.Label labelRecDir;
    }
}

