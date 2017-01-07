namespace Metasense.ObjectSpace
{
    partial class ObjectSpaceViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ObjectSpaceViewer));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.addObjectButton = new System.Windows.Forms.ToolStripButton();
            this.copyObjectButton = new System.Windows.Forms.ToolStripButton();
            this.objectViewPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addObjectButton,
            this.copyObjectButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(906, 27);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // addObjectButton
            // 
            this.addObjectButton.Image = ((System.Drawing.Image)(resources.GetObject("addObjectButton.Image")));
            this.addObjectButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.addObjectButton.Name = "addObjectButton";
            this.addObjectButton.Size = new System.Drawing.Size(61, 24);
            this.addObjectButton.Text = "Add";
            // 
            // copyObjectButton
            // 
            this.copyObjectButton.Image = ((System.Drawing.Image)(resources.GetObject("copyObjectButton.Image")));
            this.copyObjectButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copyObjectButton.Name = "copyObjectButton";
            this.copyObjectButton.Size = new System.Drawing.Size(67, 24);
            this.copyObjectButton.Text = "Copy";
            // 
            // objectViewPanel
            // 
            this.objectViewPanel.BackColor = System.Drawing.Color.White;
            this.objectViewPanel.Location = new System.Drawing.Point(12, 30);
            this.objectViewPanel.Name = "objectViewPanel";
            this.objectViewPanel.Padding = new System.Windows.Forms.Padding(10);
            this.objectViewPanel.Size = new System.Drawing.Size(882, 401);
            this.objectViewPanel.TabIndex = 1;
            // 
            // ObjectSpaceViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(906, 443);
            this.Controls.Add(this.objectViewPanel);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "ObjectSpaceViewer";
            this.Text = "Object Space Viewer";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton addObjectButton;
        private System.Windows.Forms.ToolStripButton copyObjectButton;
        private System.Windows.Forms.FlowLayoutPanel objectViewPanel;
    }
}