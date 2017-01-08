namespace Metasense.ObjectSpace
{
    partial class LoadObjectForm
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
            this.filePathTextBox = new System.Windows.Forms.TextBox();
            this.filePathLabel = new System.Windows.Forms.Label();
            this.objectTypeLabel = new System.Windows.Forms.Label();
            this.objectTypeComboBox = new System.Windows.Forms.ComboBox();
            this.loadObjLabel = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // filePathTextBox
            // 
            this.filePathTextBox.Location = new System.Drawing.Point(111, 80);
            this.filePathTextBox.Name = "filePathTextBox";
            this.filePathTextBox.Size = new System.Drawing.Size(744, 22);
            this.filePathTextBox.TabIndex = 0;
            // 
            // filePathLabel
            // 
            this.filePathLabel.AutoSize = true;
            this.filePathLabel.Location = new System.Drawing.Point(34, 83);
            this.filePathLabel.Name = "filePathLabel";
            this.filePathLabel.Size = new System.Drawing.Size(67, 17);
            this.filePathLabel.TabIndex = 1;
            this.filePathLabel.Text = "File Path:";
            // 
            // objectTypeLabel
            // 
            this.objectTypeLabel.AutoSize = true;
            this.objectTypeLabel.Location = new System.Drawing.Point(12, 49);
            this.objectTypeLabel.Name = "objectTypeLabel";
            this.objectTypeLabel.Size = new System.Drawing.Size(89, 17);
            this.objectTypeLabel.TabIndex = 2;
            this.objectTypeLabel.Text = "Object Type:";
            // 
            // objectTypeComboBox
            // 
            this.objectTypeComboBox.FormattingEnabled = true;
            this.objectTypeComboBox.Items.AddRange(new object[] {
            "Table (#TBL)",
            "Neural Network (#NNT)"});
            this.objectTypeComboBox.Location = new System.Drawing.Point(111, 46);
            this.objectTypeComboBox.Name = "objectTypeComboBox";
            this.objectTypeComboBox.Size = new System.Drawing.Size(175, 24);
            this.objectTypeComboBox.TabIndex = 3;
            // 
            // loadObjLabel
            // 
            this.loadObjLabel.AutoSize = true;
            this.loadObjLabel.Location = new System.Drawing.Point(13, 13);
            this.loadObjLabel.Name = "loadObjLabel";
            this.loadObjLabel.Size = new System.Drawing.Size(424, 17);
            this.loadObjLabel.TabIndex = 4;
            this.loadObjLabel.Text = "Select the type of object you would like to load and its data source";
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(687, 117);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(81, 23);
            this.okButton.TabIndex = 5;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(774, 117);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(81, 23);
            this.cancelButton.TabIndex = 6;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // LoadObjectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(867, 155);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.loadObjLabel);
            this.Controls.Add(this.objectTypeComboBox);
            this.Controls.Add(this.objectTypeLabel);
            this.Controls.Add(this.filePathLabel);
            this.Controls.Add(this.filePathTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "LoadObjectForm";
            this.Text = "Load Object";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox filePathTextBox;
        private System.Windows.Forms.Label filePathLabel;
        private System.Windows.Forms.Label objectTypeLabel;
        private System.Windows.Forms.ComboBox objectTypeComboBox;
        private System.Windows.Forms.Label loadObjLabel;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
    }
}