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
            this.Start = new System.Windows.Forms.Button();
            this.sp = new System.Windows.Forms.Button();
            this.Info = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Start
            // 
            this.Start.Location = new System.Drawing.Point(921, 548);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(84, 23);
            this.Start.TabIndex = 0;
            this.Start.Text = "Build";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // sp
            // 
            this.sp.Location = new System.Drawing.Point(824, 547);
            this.sp.Name = "sp";
            this.sp.Size = new System.Drawing.Size(75, 23);
            this.sp.TabIndex = 1;
            this.sp.Text = "Splay";
            this.sp.UseVisualStyleBackColor = true;
            this.sp.Click += new System.EventHandler(this.sp_Click);
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1055, 596);
            this.Controls.Add(this.Info);
            this.Controls.Add(this.sp);
            this.Controls.Add(this.Start);
            this.Name = "MainForm";
            this.Text = "ViS";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.Button sp;
        private System.Windows.Forms.Label Info;
    }
}

