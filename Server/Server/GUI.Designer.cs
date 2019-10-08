namespace Server
{
    partial class GUI
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
            this.startButton = new System.Windows.Forms.Button();
            this.ipText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.portText = new System.Windows.Forms.TextBox();
            this.messageBox = new System.Windows.Forms.RichTextBox();
            this.debitButton = new System.Windows.Forms.Button();
            this.creditButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // startButton
            // 
            this.startButton.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.startButton.Location = new System.Drawing.Point(279, 42);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 0;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = false;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // ipText
            // 
            this.ipText.Location = new System.Drawing.Point(76, 42);
            this.ipText.Name = "ipText";
            this.ipText.ReadOnly = true;
            this.ipText.Size = new System.Drawing.Size(100, 20);
            this.ipText.TabIndex = 1;
            this.ipText.Text = "127.0.0.1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "IP Address";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(182, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Port";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // portText
            // 
            this.portText.Location = new System.Drawing.Point(214, 42);
            this.portText.Name = "portText";
            this.portText.ReadOnly = true;
            this.portText.Size = new System.Drawing.Size(45, 20);
            this.portText.TabIndex = 3;
            this.portText.Text = "8080";
            this.portText.TextChanged += new System.EventHandler(this.portText_TextChanged);
            // 
            // messageBox
            // 
            this.messageBox.Location = new System.Drawing.Point(12, 98);
            this.messageBox.Name = "messageBox";
            this.messageBox.Size = new System.Drawing.Size(342, 176);
            this.messageBox.TabIndex = 5;
            this.messageBox.Text = "";
            this.messageBox.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // debitButton
            // 
            this.debitButton.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.debitButton.Location = new System.Drawing.Point(360, 140);
            this.debitButton.Name = "debitButton";
            this.debitButton.Size = new System.Drawing.Size(95, 22);
            this.debitButton.TabIndex = 7;
            this.debitButton.Text = "Debit Charges";
            this.debitButton.UseVisualStyleBackColor = false;
            this.debitButton.Click += new System.EventHandler(this.debitButton_Click);
            // 
            // creditButton
            // 
            this.creditButton.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.creditButton.Location = new System.Drawing.Point(360, 112);
            this.creditButton.Name = "creditButton";
            this.creditButton.Size = new System.Drawing.Size(95, 23);
            this.creditButton.TabIndex = 8;
            this.creditButton.Text = "Credit Interest";
            this.creditButton.UseVisualStyleBackColor = false;
            this.creditButton.Click += new System.EventHandler(this.creditButton_Click);
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(467, 302);
            this.Controls.Add(this.creditButton);
            this.Controls.Add(this.debitButton);
            this.Controls.Add(this.messageBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.portText);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ipText);
            this.Controls.Add(this.startButton);
            this.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.Name = "GUI";
            this.Text = "Server";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.TextBox ipText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox portText;
        private System.Windows.Forms.RichTextBox messageBox;
        private System.Windows.Forms.Button debitButton;
        private System.Windows.Forms.Button creditButton;
    }
}

