namespace VehicleChecking
{
    partial class frmSetting
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblFontFamily = new System.Windows.Forms.Label();
            this.lblBackground = new System.Windows.Forms.Label();
            this.lblAlarm = new System.Windows.Forms.Label();
            this.lblNormal = new System.Windows.Forms.Label();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.txtTopMargin = new System.Windows.Forms.TextBox();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.txtLeftMargin = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtLocalPort = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtLocalIp = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtServerInterval = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtServerPort = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtServerAddress = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnPlaySound = new System.Windows.Forms.Button();
            this.btnChoiceSound = new System.Windows.Forms.Button();
            this.txtSoundFile = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(427, 426);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(510, 426);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblFontFamily);
            this.groupBox1.Controls.Add(this.lblBackground);
            this.groupBox1.Controls.Add(this.lblAlarm);
            this.groupBox1.Controls.Add(this.lblNormal);
            this.groupBox1.Controls.Add(this.txtWidth);
            this.groupBox1.Controls.Add(this.txtTopMargin);
            this.groupBox1.Controls.Add(this.txtHeight);
            this.groupBox1.Controls.Add(this.txtLeftMargin);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(22, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(560, 174);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "LED显示";
            // 
            // lblFontFamily
            // 
            this.lblFontFamily.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.lblFontFamily.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFontFamily.Location = new System.Drawing.Point(81, 125);
            this.lblFontFamily.Name = "lblFontFamily";
            this.lblFontFamily.Size = new System.Drawing.Size(466, 21);
            this.lblFontFamily.TabIndex = 45;
            this.lblFontFamily.Click += new System.EventHandler(this.lblFontFamily_Click);
            // 
            // lblBackground
            // 
            this.lblBackground.BackColor = System.Drawing.Color.Black;
            this.lblBackground.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBackground.Location = new System.Drawing.Point(81, 99);
            this.lblBackground.Name = "lblBackground";
            this.lblBackground.Size = new System.Drawing.Size(174, 21);
            this.lblBackground.TabIndex = 6;
            this.lblBackground.Click += new System.EventHandler(this.lblNormal_Click);
            // 
            // lblAlarm
            // 
            this.lblAlarm.BackColor = System.Drawing.Color.Red;
            this.lblAlarm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAlarm.Location = new System.Drawing.Point(365, 73);
            this.lblAlarm.Name = "lblAlarm";
            this.lblAlarm.Size = new System.Drawing.Size(182, 21);
            this.lblAlarm.TabIndex = 5;
            this.lblAlarm.Click += new System.EventHandler(this.lblNormal_Click);
            // 
            // lblNormal
            // 
            this.lblNormal.BackColor = System.Drawing.Color.Green;
            this.lblNormal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNormal.Location = new System.Drawing.Point(81, 73);
            this.lblNormal.Name = "lblNormal";
            this.lblNormal.Size = new System.Drawing.Size(174, 21);
            this.lblNormal.TabIndex = 4;
            this.lblNormal.Click += new System.EventHandler(this.lblNormal_Click);
            // 
            // txtWidth
            // 
            this.txtWidth.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtWidth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtWidth.Location = new System.Drawing.Point(365, 46);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(182, 21);
            this.txtWidth.TabIndex = 3;
            // 
            // txtTopMargin
            // 
            this.txtTopMargin.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtTopMargin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTopMargin.Location = new System.Drawing.Point(365, 20);
            this.txtTopMargin.Name = "txtTopMargin";
            this.txtTopMargin.Size = new System.Drawing.Size(182, 21);
            this.txtTopMargin.TabIndex = 1;
            // 
            // txtHeight
            // 
            this.txtHeight.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtHeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHeight.Location = new System.Drawing.Point(81, 46);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(174, 21);
            this.txtHeight.TabIndex = 2;
            // 
            // txtLeftMargin
            // 
            this.txtLeftMargin.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtLeftMargin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLeftMargin.Location = new System.Drawing.Point(81, 20);
            this.txtLeftMargin.Name = "txtLeftMargin";
            this.txtLeftMargin.Size = new System.Drawing.Size(174, 21);
            this.txtLeftMargin.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(16, 126);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 21);
            this.label9.TabIndex = 44;
            this.label9.Text = "字体";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(16, 101);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 21);
            this.label7.TabIndex = 42;
            this.label7.Text = "背景";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(300, 73);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 21);
            this.label6.TabIndex = 41;
            this.label6.Text = "告警车辆";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(16, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 21);
            this.label5.TabIndex = 40;
            this.label5.Text = "正常车辆";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(300, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 21);
            this.label4.TabIndex = 39;
            this.label4.Text = "上边距";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(16, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 21);
            this.label3.TabIndex = 38;
            this.label3.Text = "高度";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(300, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 21);
            this.label2.TabIndex = 37;
            this.label2.Text = "宽度";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 21);
            this.label1.TabIndex = 36;
            this.label1.Text = "左边距";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtLocalPort);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.txtLocalIp);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Location = new System.Drawing.Point(22, 193);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(273, 160);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "过车信息";
            // 
            // txtLocalPort
            // 
            this.txtLocalPort.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtLocalPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLocalPort.Location = new System.Drawing.Point(81, 47);
            this.txtLocalPort.Name = "txtLocalPort";
            this.txtLocalPort.Size = new System.Drawing.Size(174, 21);
            this.txtLocalPort.TabIndex = 1;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(16, 47);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(59, 21);
            this.label14.TabIndex = 55;
            this.label14.Text = "监听端口";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtLocalIp
            // 
            this.txtLocalIp.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtLocalIp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLocalIp.Location = new System.Drawing.Point(81, 20);
            this.txtLocalIp.Name = "txtLocalIp";
            this.txtLocalIp.Size = new System.Drawing.Size(174, 21);
            this.txtLocalIp.TabIndex = 0;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(16, 20);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(59, 21);
            this.label13.TabIndex = 53;
            this.label13.Text = "本机地址";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtServerInterval);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.txtPassword);
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Controls.Add(this.txtUserName);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.txtServerPort);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.txtServerAddress);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Location = new System.Drawing.Point(316, 193);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(267, 160);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "黑名单信息";
            // 
            // txtServerInterval
            // 
            this.txtServerInterval.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtServerInterval.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtServerInterval.Location = new System.Drawing.Point(77, 128);
            this.txtServerInterval.Name = "txtServerInterval";
            this.txtServerInterval.Size = new System.Drawing.Size(149, 21);
            this.txtServerInterval.TabIndex = 64;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(0, 128);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 21);
            this.label8.TabIndex = 65;
            this.label8.Text = "获取间隔";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Location = new System.Drawing.Point(77, 101);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(174, 21);
            this.txtPassword.TabIndex = 3;
            // 
            // label18
            // 
            this.label18.Location = new System.Drawing.Point(0, 101);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(71, 21);
            this.label18.TabIndex = 63;
            this.label18.Text = "密码";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtUserName
            // 
            this.txtUserName.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUserName.Location = new System.Drawing.Point(77, 74);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(174, 21);
            this.txtUserName.TabIndex = 2;
            // 
            // label17
            // 
            this.label17.Location = new System.Drawing.Point(0, 74);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(71, 21);
            this.label17.TabIndex = 61;
            this.label17.Text = "用户名";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtServerPort
            // 
            this.txtServerPort.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtServerPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtServerPort.Location = new System.Drawing.Point(77, 47);
            this.txtServerPort.Name = "txtServerPort";
            this.txtServerPort.Size = new System.Drawing.Size(174, 21);
            this.txtServerPort.TabIndex = 1;
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(0, 47);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(71, 21);
            this.label15.TabIndex = 59;
            this.label15.Text = "服务器端口";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtServerAddress
            // 
            this.txtServerAddress.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtServerAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtServerAddress.Location = new System.Drawing.Point(77, 20);
            this.txtServerAddress.Name = "txtServerAddress";
            this.txtServerAddress.Size = new System.Drawing.Size(174, 21);
            this.txtServerAddress.TabIndex = 0;
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(6, 20);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(65, 21);
            this.label16.TabIndex = 57;
            this.label16.Text = "服务器地址";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnPlaySound);
            this.groupBox4.Controls.Add(this.btnChoiceSound);
            this.groupBox4.Controls.Add(this.txtSoundFile);
            this.groupBox4.Controls.Add(this.label19);
            this.groupBox4.Location = new System.Drawing.Point(19, 359);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(563, 55);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "告警信息";
            // 
            // btnPlaySound
            // 
            this.btnPlaySound.Location = new System.Drawing.Point(467, 18);
            this.btnPlaySound.Name = "btnPlaySound";
            this.btnPlaySound.Size = new System.Drawing.Size(75, 23);
            this.btnPlaySound.TabIndex = 2;
            this.btnPlaySound.Text = "播放";
            this.btnPlaySound.UseVisualStyleBackColor = true;
            this.btnPlaySound.Click += new System.EventHandler(this.btnPlaySound_Click);
            // 
            // btnChoiceSound
            // 
            this.btnChoiceSound.Location = new System.Drawing.Point(386, 18);
            this.btnChoiceSound.Name = "btnChoiceSound";
            this.btnChoiceSound.Size = new System.Drawing.Size(75, 23);
            this.btnChoiceSound.TabIndex = 1;
            this.btnChoiceSound.Text = "选择";
            this.btnChoiceSound.UseVisualStyleBackColor = true;
            this.btnChoiceSound.Click += new System.EventHandler(this.btnChoiceSound_Click);
            // 
            // txtSoundFile
            // 
            this.txtSoundFile.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtSoundFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSoundFile.Location = new System.Drawing.Point(81, 20);
            this.txtSoundFile.Name = "txtSoundFile";
            this.txtSoundFile.ReadOnly = true;
            this.txtSoundFile.Size = new System.Drawing.Size(299, 21);
            this.txtSoundFile.TabIndex = 0;
            // 
            // label19
            // 
            this.label19.Location = new System.Drawing.Point(16, 20);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(59, 21);
            this.label19.TabIndex = 57;
            this.label19.Text = "声音文件";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(543, 321);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(24, 21);
            this.label10.TabIndex = 66;
            this.label10.Text = "min";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 457);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmSetting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置";
            this.Load += new System.EventHandler(this.frmSetting_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblBackground;
        private System.Windows.Forms.Label lblAlarm;
        private System.Windows.Forms.Label lblNormal;
        private System.Windows.Forms.TextBox txtWidth;
        private System.Windows.Forms.TextBox txtTopMargin;
        private System.Windows.Forms.TextBox txtHeight;
        private System.Windows.Forms.TextBox txtLeftMargin;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtLocalPort;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtLocalIp;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtServerPort;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtServerAddress;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnPlaySound;
        private System.Windows.Forms.Button btnChoiceSound;
        private System.Windows.Forms.TextBox txtSoundFile;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label lblFontFamily;
        private System.Windows.Forms.TextBox txtServerInterval;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
    }
}