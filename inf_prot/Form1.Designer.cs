namespace inf_prot
{
    partial class MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            sourceMsgTextBox = new TextBox();
            encryptMsgTextBox = new TextBox();
            encryptButton = new Button();
            decryptButton = new Button();
            sourceMsgLabel = new Label();
            keyLabel = new Label();
            encryptMsgLabel = new Label();
            editKeyButton = new Button();
            changeLabComboBox = new ComboBox();
            keyErrorLabel = new Label();
            SuspendLayout();
            // 
            // sourceMsgTextBox
            // 
            sourceMsgTextBox.Location = new Point(51, 103);
            sourceMsgTextBox.Multiline = true;
            sourceMsgTextBox.Name = "sourceMsgTextBox";
            sourceMsgTextBox.ScrollBars = ScrollBars.Vertical;
            sourceMsgTextBox.Size = new Size(382, 72);
            sourceMsgTextBox.TabIndex = 0;
            sourceMsgTextBox.TextChanged += MsgTextBox_TextChanged;
            // 
            // encryptMsgTextBox
            // 
            encryptMsgTextBox.Location = new Point(51, 230);
            encryptMsgTextBox.Multiline = true;
            encryptMsgTextBox.Name = "encryptMsgTextBox";
            encryptMsgTextBox.ScrollBars = ScrollBars.Vertical;
            encryptMsgTextBox.Size = new Size(382, 72);
            encryptMsgTextBox.TabIndex = 2;
            encryptMsgTextBox.TextChanged += MsgTextBox_TextChanged;
            // 
            // encryptButton
            // 
            encryptButton.Location = new Point(51, 339);
            encryptButton.Name = "encryptButton";
            encryptButton.Size = new Size(125, 30);
            encryptButton.TabIndex = 3;
            encryptButton.Text = "Зашифровать";
            encryptButton.UseVisualStyleBackColor = true;
            encryptButton.Click += EncryptButton_Click;
            // 
            // decryptButton
            // 
            decryptButton.Location = new Point(209, 339);
            decryptButton.Name = "decryptButton";
            decryptButton.Size = new Size(125, 30);
            decryptButton.TabIndex = 4;
            decryptButton.Text = "Расшифровать";
            decryptButton.UseVisualStyleBackColor = true;
            decryptButton.Click += DecryptButton_Click;
            // 
            // sourceMsgLabel
            // 
            sourceMsgLabel.AutoSize = true;
            sourceMsgLabel.Location = new Point(51, 80);
            sourceMsgLabel.Name = "sourceMsgLabel";
            sourceMsgLabel.Size = new Size(161, 20);
            sourceMsgLabel.TabIndex = 5;
            sourceMsgLabel.Text = "Исходное сообщение";
            // 
            // keyLabel
            // 
            keyLabel.AutoSize = true;
            keyLabel.Location = new Point(477, 165);
            keyLabel.Name = "keyLabel";
            keyLabel.Size = new Size(46, 20);
            keyLabel.TabIndex = 6;
            keyLabel.Text = "Ключ";
            // 
            // encryptMsgLabel
            // 
            encryptMsgLabel.AutoSize = true;
            encryptMsgLabel.Location = new Point(51, 207);
            encryptMsgLabel.Name = "encryptMsgLabel";
            encryptMsgLabel.Size = new Size(209, 20);
            encryptMsgLabel.TabIndex = 7;
            encryptMsgLabel.Text = "Зашифрованное сообщение";
            // 
            // editKeyButton
            // 
            editKeyButton.Location = new Point(477, 198);
            editKeyButton.Name = "editKeyButton";
            editKeyButton.Size = new Size(140, 29);
            editKeyButton.TabIndex = 8;
            editKeyButton.Text = "Редактировать";
            editKeyButton.UseVisualStyleBackColor = true;
            editKeyButton.Click += EditKeyButton_Click;
            // 
            // changeLabComboBox
            // 
            changeLabComboBox.FormattingEnabled = true;
            changeLabComboBox.Items.AddRange(new object[] { "Lab1", "Lab2", "Lab3", "Lab4", "Lab5", "Lab6", "Lab7", "Lab8", "Lab9" });
            changeLabComboBox.Location = new Point(51, 32);
            changeLabComboBox.Name = "changeLabComboBox";
            changeLabComboBox.Size = new Size(151, 28);
            changeLabComboBox.TabIndex = 9;
            changeLabComboBox.SelectionChangeCommitted += ChangeLabComboBox_SelectionChangeCommitted;
            // 
            // keyErrorLabel
            // 
            keyErrorLabel.BackColor = SystemColors.Control;
            keyErrorLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            keyErrorLabel.ForeColor = Color.Firebrick;
            keyErrorLabel.Location = new Point(452, 239);
            keyErrorLabel.Name = "keyErrorLabel";
            keyErrorLabel.Size = new Size(203, 49);
            keyErrorLabel.TabIndex = 10;
            keyErrorLabel.Text = "Ключ не был изменен. Передан неверный ключ!";
            keyErrorLabel.Visible = false;
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(657, 414);
            Controls.Add(keyErrorLabel);
            Controls.Add(changeLabComboBox);
            Controls.Add(editKeyButton);
            Controls.Add(encryptMsgLabel);
            Controls.Add(keyLabel);
            Controls.Add(sourceMsgLabel);
            Controls.Add(decryptButton);
            Controls.Add(encryptButton);
            Controls.Add(encryptMsgTextBox);
            Controls.Add(sourceMsgTextBox);
            Name = "MainWindow";
            Text = "Защита информации";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox sourceMsgTextBox;
        private TextBox encryptMsgTextBox;
        private Button encryptButton;
        private Button decryptButton;
        private Label sourceMsgLabel;
        private Label keyLabel;
        private Label encryptMsgLabel;
        private Button editKeyButton;
        private ComboBox changeLabComboBox;
        private Label keyErrorLabel;
    }
}
