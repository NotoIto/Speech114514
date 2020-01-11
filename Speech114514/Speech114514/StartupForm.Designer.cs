namespace NotoIto.App.Speech114514
{
    partial class StartupForm
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
            this.audioGroupBox = new System.Windows.Forms.GroupBox();
            this.outputLabel = new System.Windows.Forms.Label();
            this.outputComboBox = new System.Windows.Forms.ComboBox();
            this.inputLabel = new System.Windows.Forms.Label();
            this.inputComboBox = new System.Windows.Forms.ComboBox();
            this.startButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.audioGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // audioGroupBox
            // 
            this.audioGroupBox.Controls.Add(this.outputLabel);
            this.audioGroupBox.Controls.Add(this.outputComboBox);
            this.audioGroupBox.Controls.Add(this.inputLabel);
            this.audioGroupBox.Controls.Add(this.inputComboBox);
            this.audioGroupBox.Location = new System.Drawing.Point(12, 12);
            this.audioGroupBox.Name = "audioGroupBox";
            this.audioGroupBox.Size = new System.Drawing.Size(321, 99);
            this.audioGroupBox.TabIndex = 0;
            this.audioGroupBox.TabStop = false;
            this.audioGroupBox.Text = "Audio device";
            // 
            // outputLabel
            // 
            this.outputLabel.AutoSize = true;
            this.outputLabel.Location = new System.Drawing.Point(6, 61);
            this.outputLabel.Name = "outputLabel";
            this.outputLabel.Size = new System.Drawing.Size(45, 12);
            this.outputLabel.TabIndex = 3;
            this.outputLabel.Text = "Output :";
            // 
            // outputComboBox
            // 
            this.outputComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.outputComboBox.FormattingEnabled = true;
            this.outputComboBox.Location = new System.Drawing.Point(57, 58);
            this.outputComboBox.Name = "outputComboBox";
            this.outputComboBox.Size = new System.Drawing.Size(258, 20);
            this.outputComboBox.TabIndex = 2;
            this.outputComboBox.SelectedIndexChanged += new System.EventHandler(this.outputComboBox_SelectedIndexChanged);
            // 
            // inputLabel
            // 
            this.inputLabel.AutoSize = true;
            this.inputLabel.Location = new System.Drawing.Point(6, 35);
            this.inputLabel.Name = "inputLabel";
            this.inputLabel.Size = new System.Drawing.Size(36, 12);
            this.inputLabel.TabIndex = 1;
            this.inputLabel.Text = "Input :";
            // 
            // inputComboBox
            // 
            this.inputComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.inputComboBox.FormattingEnabled = true;
            this.inputComboBox.Location = new System.Drawing.Point(57, 32);
            this.inputComboBox.Name = "inputComboBox";
            this.inputComboBox.Size = new System.Drawing.Size(258, 20);
            this.inputComboBox.TabIndex = 0;
            this.inputComboBox.SelectedIndexChanged += new System.EventHandler(this.inputComboBox_SelectedIndexChanged);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(135, 132);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 4;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Noto Sans Japanese Regular", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(179, 183);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "©Copyright NotoIto 2020.";
            // 
            // StartupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 209);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.audioGroupBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "StartupForm";
            this.Text = "Speech114514 ver";
            this.Load += new System.EventHandler(this.StartupForm_Load);
            this.audioGroupBox.ResumeLayout(false);
            this.audioGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox audioGroupBox;
        private System.Windows.Forms.Label outputLabel;
        private System.Windows.Forms.ComboBox outputComboBox;
        private System.Windows.Forms.Label inputLabel;
        private System.Windows.Forms.ComboBox inputComboBox;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Label label1;
    }
}

