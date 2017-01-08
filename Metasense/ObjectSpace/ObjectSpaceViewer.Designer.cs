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
            this.listView1 = new System.Windows.Forms.ListView();
            this.nameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.typeColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.fileColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.loadObjectButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addObjectButton,
            this.loadObjectButton,
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
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameColumn,
            this.typeColumn,
            this.fileColumn});
            this.listView1.Location = new System.Drawing.Point(0, 27);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(906, 416);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // nameColumn
            // 
            this.nameColumn.Text = "Name";
            // 
            // typeColumn
            // 
            this.typeColumn.Text = "Type";
            // 
            // fileColumn
            // 
            this.fileColumn.Text = "File";
            // 
            // loadObjectButton
            // 
            this.loadObjectButton.Image = ((System.Drawing.Image)(resources.GetObject("loadObjectButton.Image")));
            this.loadObjectButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.loadObjectButton.Name = "loadObjectButton";
            this.loadObjectButton.Size = new System.Drawing.Size(66, 24);
            this.loadObjectButton.Text = "Load";
            this.loadObjectButton.Click += new System.EventHandler(this.loadObjectButton_Click);
            // 
            // ObjectSpaceViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(906, 443);
            this.Controls.Add(this.listView1);
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
        private System.Windows.Forms.ToolStripButton loadObjectButton;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader nameColumn;
        private System.Windows.Forms.ColumnHeader typeColumn;
        private System.Windows.Forms.ColumnHeader fileColumn;
    }
}