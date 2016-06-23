namespace ViS
{
    partial class ValueGetForm
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
            this.ValueTextBox = new System.Windows.Forms.TextBox();
            this.ok = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ValueTextBox
            // 
            this.ValueTextBox.Location = new System.Drawing.Point(13, 13);
            this.ValueTextBox.Name = "ValueTextBox";
            this.ValueTextBox.Size = new System.Drawing.Size(166, 21);
            this.ValueTextBox.TabIndex = 0;
            // 
            // ok
            // 
            this.ok.Location = new System.Drawing.Point(104, 40);
            this.ok.Name = "ok";
            this.ok.Size = new System.Drawing.Size(75, 23);
            this.ok.TabIndex = 1;
            this.ok.Text = "OK";
            this.ok.UseVisualStyleBackColor = true;
            this.ok.Click += new System.EventHandler(this.ok_Click);
            // 
            // ValueGetButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(189, 77);
            this.Controls.Add(this.ok);
            this.Controls.Add(this.ValueTextBox);
            this.Name = "ValueGetButton";
            this.Text = "Insert";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ValueTextBox;
        private System.Windows.Forms.Button ok;
    }
}