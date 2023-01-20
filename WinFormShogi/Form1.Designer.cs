namespace WinFormShogi
{
    partial class Form1
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

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.turnLabel = new System.Windows.Forms.Label();
            this.startButton = new System.Windows.Forms.Button();
            this.countLabel = new System.Windows.Forms.Label();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.playerList = new System.Windows.Forms.ListBox();
            this.comList = new System.Windows.Forms.ListBox();
            this.emptyList = new System.Windows.Forms.ListBox();
            this.playerSubList = new System.Windows.Forms.ListBox();
            this.comSubList = new System.Windows.Forms.ListBox();
            this.radioButton10 = new System.Windows.Forms.RadioButton();
            this.playerTimeLabel = new System.Windows.Forms.Label();
            this.comTimeLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // turnLabel
            // 
            this.turnLabel.AutoSize = true;
            this.turnLabel.Location = new System.Drawing.Point(484, 152);
            this.turnLabel.Name = "turnLabel";
            this.turnLabel.Size = new System.Drawing.Size(35, 12);
            this.turnLabel.TabIndex = 0;
            this.turnLabel.Text = "手番：";
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(483, 78);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 46);
            this.startButton.TabIndex = 1;
            this.startButton.Text = "対局開始";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // countLabel
            // 
            this.countLabel.AutoSize = true;
            this.countLabel.Location = new System.Drawing.Point(484, 178);
            this.countLabel.Name = "countLabel";
            this.countLabel.Size = new System.Drawing.Size(35, 12);
            this.countLabel.TabIndex = 2;
            this.countLabel.Text = "手数：";
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(478, 42);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(41, 16);
            this.radioButton3.TabIndex = 3;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "3分";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // playerList
            // 
            this.playerList.FormattingEnabled = true;
            this.playerList.ItemHeight = 12;
            this.playerList.Location = new System.Drawing.Point(656, 16);
            this.playerList.Name = "playerList";
            this.playerList.Size = new System.Drawing.Size(70, 280);
            this.playerList.TabIndex = 5;
            // 
            // comList
            // 
            this.comList.FormattingEnabled = true;
            this.comList.ItemHeight = 12;
            this.comList.Location = new System.Drawing.Point(732, 16);
            this.comList.Name = "comList";
            this.comList.Size = new System.Drawing.Size(70, 280);
            this.comList.TabIndex = 6;
            // 
            // emptyList
            // 
            this.emptyList.FormattingEnabled = true;
            this.emptyList.ItemHeight = 12;
            this.emptyList.Location = new System.Drawing.Point(808, 16);
            this.emptyList.Name = "emptyList";
            this.emptyList.Size = new System.Drawing.Size(70, 280);
            this.emptyList.TabIndex = 6;
            // 
            // playerSubList
            // 
            this.playerSubList.FormattingEnabled = true;
            this.playerSubList.ItemHeight = 12;
            this.playerSubList.Location = new System.Drawing.Point(884, 16);
            this.playerSubList.Name = "playerSubList";
            this.playerSubList.Size = new System.Drawing.Size(70, 112);
            this.playerSubList.TabIndex = 5;
            // 
            // comSubList
            // 
            this.comSubList.FormattingEnabled = true;
            this.comSubList.ItemHeight = 12;
            this.comSubList.Location = new System.Drawing.Point(884, 134);
            this.comSubList.Name = "comSubList";
            this.comSubList.Size = new System.Drawing.Size(70, 112);
            this.comSubList.TabIndex = 7;
            // 
            // radioButton10
            // 
            this.radioButton10.AutoSize = true;
            this.radioButton10.Location = new System.Drawing.Point(525, 42);
            this.radioButton10.Name = "radioButton10";
            this.radioButton10.Size = new System.Drawing.Size(47, 16);
            this.radioButton10.TabIndex = 3;
            this.radioButton10.TabStop = true;
            this.radioButton10.Text = "10分";
            this.radioButton10.UseVisualStyleBackColor = true;
            // 
            // playerTimeLabel
            // 
            this.playerTimeLabel.AutoSize = true;
            this.playerTimeLabel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.playerTimeLabel.Location = new System.Drawing.Point(484, 266);
            this.playerTimeLabel.Name = "playerTimeLabel";
            this.playerTimeLabel.Size = new System.Drawing.Size(43, 16);
            this.playerTimeLabel.TabIndex = 8;
            this.playerTimeLabel.Text = "00:00";
            // 
            // comTimeLabel
            // 
            this.comTimeLabel.AutoSize = true;
            this.comTimeLabel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.comTimeLabel.Location = new System.Drawing.Point(484, 325);
            this.comTimeLabel.Name = "comTimeLabel";
            this.comTimeLabel.Size = new System.Drawing.Size(43, 16);
            this.comTimeLabel.TabIndex = 8;
            this.comTimeLabel.Text = "00:00";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(484, 243);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "あなた";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(484, 301);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "コンピュータ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(484, 217);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "残り時間";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(483, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "持ち時間選択";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 483);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comTimeLabel);
            this.Controls.Add(this.playerTimeLabel);
            this.Controls.Add(this.comSubList);
            this.Controls.Add(this.emptyList);
            this.Controls.Add(this.comList);
            this.Controls.Add(this.playerSubList);
            this.Controls.Add(this.playerList);
            this.Controls.Add(this.radioButton10);
            this.Controls.Add(this.radioButton3);
            this.Controls.Add(this.countLabel);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.turnLabel);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label turnLabel;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Label countLabel;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.ListBox playerList;
        private System.Windows.Forms.ListBox comList;
        private System.Windows.Forms.ListBox emptyList;
        private System.Windows.Forms.ListBox playerSubList;
        private System.Windows.Forms.ListBox comSubList;
        private System.Windows.Forms.RadioButton radioButton10;
        private System.Windows.Forms.Label playerTimeLabel;
        private System.Windows.Forms.Label comTimeLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label4;
    }
}
