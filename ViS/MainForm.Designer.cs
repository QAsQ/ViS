namespace ViS
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.Info = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.SPL = new System.Windows.Forms.ToolStripButton();
            this.Build = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Info
            // 
            this.Info.AutoSize = true;
            this.Info.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Info.Location = new System.Drawing.Point(776, 94);
            this.Info.Name = "Info";
            this.Info.Size = new System.Drawing.Size(56, 16);
            this.Info.TabIndex = 2;
            this.Info.Text = "label1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SPL,
            this.Build});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1221, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // SPL
            // 
            this.SPL.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SPL.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SPL.Name = "SPL";
            this.SPL.Size = new System.Drawing.Size(23, 22);
            this.SPL.Text = "Splay";
            this.SPL.Click += new System.EventHandler(this.SPL_Click);
            // 
            // Build
            // 
            this.Build.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Build.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Build.Name = "Build";
            this.Build.Size = new System.Drawing.Size(23, 22);
            this.Build.Text = "Build";
            this.Build.Click += new System.EventHandler(this.Build_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1221, 641);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.Info);
            this.Name = "MainForm";
            this.Text = "ViS";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Info;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton SPL;
        private System.Windows.Forms.ToolStripButton Build;

    }
}

