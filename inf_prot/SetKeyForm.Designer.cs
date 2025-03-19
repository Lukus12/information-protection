namespace inf_prot
{
    partial class SetKeyForm
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
            okButton = new Button();
            cancelButton = new Button();
            editKeyTextBox = new TextBox();
            generateButton = new Button();
            SuspendLayout();
            // 
            // okButton
            // 
            okButton.DialogResult = DialogResult.OK;
            okButton.Location = new Point(47, 385);
            okButton.Name = "okButton";
            okButton.Size = new Size(94, 29);
            okButton.TabIndex = 0;
            okButton.Text = "ОК";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += OkButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Location = new Point(161, 385);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(94, 29);
            cancelButton.TabIndex = 1;
            cancelButton.Text = "Отмена";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // editKeyTextBox
            // 
            editKeyTextBox.Location = new Point(47, 37);
            editKeyTextBox.Multiline = true;
            editKeyTextBox.Name = "editKeyTextBox";
            editKeyTextBox.ScrollBars = ScrollBars.Vertical;
            editKeyTextBox.Size = new Size(411, 327);
            editKeyTextBox.TabIndex = 2;
            editKeyTextBox.TextChanged += EditKeyTextBox_TextChanged;
            // 
            // generateButton
            // 
            generateButton.Location = new Point(330, 385);
            generateButton.Name = "generateButton";
            generateButton.Size = new Size(128, 29);
            generateButton.TabIndex = 3;
            generateButton.Text = "Генерировать";
            generateButton.UseVisualStyleBackColor = true;
            generateButton.Click += GenerateButton_Click;
            // 
            // SetKeyForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(512, 450);
            Controls.Add(generateButton);
            Controls.Add(editKeyTextBox);
            Controls.Add(cancelButton);
            Controls.Add(okButton);
            Name = "SetKeyForm";
            Text = "Редактирование ключа";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button okButton;
        private Button cancelButton;
        private TextBox editKeyTextBox;
        private Button generateButton;
    }
}