
namespace ZILab4
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.keyBox4 = new System.Windows.Forms.TextBox();
            this.keyBox3 = new System.Windows.Forms.TextBox();
            this.keyBox2 = new System.Windows.Forms.TextBox();
            this.keyBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.sendMessageTextBox = new System.Windows.Forms.TextBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.portTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.connectButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.ipTextBox = new System.Windows.Forms.TextBox();
            this.serverButton = new System.Windows.Forms.Button();
            this.messagesList = new System.Windows.Forms.ListBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.errorList = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // keyBox4
            // 
            this.keyBox4.Location = new System.Drawing.Point(398, 42);
            this.keyBox4.Name = "keyBox4";
            this.keyBox4.Size = new System.Drawing.Size(116, 22);
            this.keyBox4.TabIndex = 13;
            this.keyBox4.Text = "66666";
            // 
            // keyBox3
            // 
            this.keyBox3.Location = new System.Drawing.Point(273, 42);
            this.keyBox3.Name = "keyBox3";
            this.keyBox3.Size = new System.Drawing.Size(119, 22);
            this.keyBox3.TabIndex = 12;
            this.keyBox3.Text = "77777";
            // 
            // keyBox2
            // 
            this.keyBox2.Location = new System.Drawing.Point(146, 42);
            this.keyBox2.Name = "keyBox2";
            this.keyBox2.Size = new System.Drawing.Size(121, 22);
            this.keyBox2.TabIndex = 11;
            this.keyBox2.Text = "88888";
            // 
            // keyBox1
            // 
            this.keyBox1.Location = new System.Drawing.Point(22, 42);
            this.keyBox1.Name = "keyBox1";
            this.keyBox1.Size = new System.Drawing.Size(118, 22);
            this.keyBox1.TabIndex = 10;
            this.keyBox1.Text = "99999";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 17);
            this.label1.TabIndex = 14;
            this.label1.Text = "Key";
            // 
            // sendMessageTextBox
            // 
            this.sendMessageTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.sendMessageTextBox.Location = new System.Drawing.Point(22, 467);
            this.sendMessageTextBox.Multiline = true;
            this.sendMessageTextBox.Name = "sendMessageTextBox";
            this.sendMessageTextBox.Size = new System.Drawing.Size(559, 65);
            this.sendMessageTextBox.TabIndex = 15;
            this.sendMessageTextBox.Text = "Hello world";
            // 
            // sendButton
            // 
            this.sendButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sendButton.Enabled = false;
            this.sendButton.Location = new System.Drawing.Point(792, 467);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(201, 65);
            this.sendButton.TabIndex = 16;
            this.sendButton.Text = "Send";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // portTextBox
            // 
            this.portTextBox.Location = new System.Drawing.Point(273, 100);
            this.portTextBox.Name = "portTextBox";
            this.portTextBox.Size = new System.Drawing.Size(118, 22);
            this.portTextBox.TabIndex = 17;
            this.portTextBox.Text = "4055";
            this.portTextBox.TextChanged += new System.EventHandler(this.port_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(270, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 17);
            this.label2.TabIndex = 18;
            this.label2.Text = "port";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(397, 89);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(143, 42);
            this.connectButton.TabIndex = 19;
            this.connectButton.Text = "connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 17);
            this.label3.TabIndex = 20;
            this.label3.Text = "IP address";
            // 
            // ipTextBox
            // 
            this.ipTextBox.Location = new System.Drawing.Point(22, 100);
            this.ipTextBox.Name = "ipTextBox";
            this.ipTextBox.Size = new System.Drawing.Size(245, 22);
            this.ipTextBox.TabIndex = 21;
            this.ipTextBox.Text = "127.0.0.1";
            // 
            // serverButton
            // 
            this.serverButton.Location = new System.Drawing.Point(546, 89);
            this.serverButton.Name = "serverButton";
            this.serverButton.Size = new System.Drawing.Size(242, 42);
            this.serverButton.TabIndex = 22;
            this.serverButton.Text = "start server";
            this.serverButton.UseVisualStyleBackColor = true;
            this.serverButton.Click += new System.EventHandler(this.serverButton_Click);
            // 
            // messagesList
            // 
            this.messagesList.FormattingEnabled = true;
            this.messagesList.ItemHeight = 16;
            this.messagesList.Location = new System.Drawing.Point(22, 160);
            this.messagesList.Name = "messagesList";
            this.messagesList.Size = new System.Drawing.Size(479, 292);
            this.messagesList.TabIndex = 23;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // errorList
            // 
            this.errorList.FormattingEnabled = true;
            this.errorList.ItemHeight = 16;
            this.errorList.Location = new System.Drawing.Point(507, 160);
            this.errorList.Name = "errorList";
            this.errorList.Size = new System.Drawing.Size(486, 292);
            this.errorList.TabIndex = 24;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1005, 560);
            this.Controls.Add(this.errorList);
            this.Controls.Add(this.messagesList);
            this.Controls.Add(this.serverButton);
            this.Controls.Add(this.ipTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.portTextBox);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.sendMessageTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.keyBox4);
            this.Controls.Add(this.keyBox3);
            this.Controls.Add(this.keyBox2);
            this.Controls.Add(this.keyBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox keyBox4;
        private System.Windows.Forms.TextBox keyBox3;
        private System.Windows.Forms.TextBox keyBox2;
        private System.Windows.Forms.TextBox keyBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox sendMessageTextBox;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.TextBox portTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ipTextBox;
        private System.Windows.Forms.Button serverButton;
        private System.Windows.Forms.ListBox messagesList;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ListBox errorList;
    }
}

