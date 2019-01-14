namespace UpdateUpload
{
    partial class FormUpdateClient
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
            this.btnUpload = new System.Windows.Forms.Button();
            this.rdoKD = new System.Windows.Forms.RadioButton();
            this.rdoHD = new System.Windows.Forms.RadioButton();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(310, 87);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(195, 69);
            this.btnUpload.TabIndex = 0;
            this.btnUpload.Text = "Upload";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // rdoKD
            // 
            this.rdoKD.AutoSize = true;
            this.rdoKD.Location = new System.Drawing.Point(86, 289);
            this.rdoKD.Name = "rdoKD";
            this.rdoKD.Size = new System.Drawing.Size(39, 16);
            this.rdoKD.TabIndex = 3;
            this.rdoKD.TabStop = true;
            this.rdoKD.Text = "KD";
            this.rdoKD.UseVisualStyleBackColor = true;
            this.rdoKD.CheckedChanged += new System.EventHandler(this.rdo_CheckedChanged);
            // 
            // rdoHD
            // 
            this.rdoHD.AutoSize = true;
            this.rdoHD.Location = new System.Drawing.Point(131, 289);
            this.rdoHD.Name = "rdoHD";
            this.rdoHD.Size = new System.Drawing.Size(39, 16);
            this.rdoHD.TabIndex = 4;
            this.rdoHD.TabStop = true;
            this.rdoHD.Text = "HD";
            this.rdoHD.UseVisualStyleBackColor = true;
            this.rdoHD.CheckedChanged += new System.EventHandler(this.rdo_CheckedChanged);
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(12, 12);
            this.txtPath.Multiline = true;
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(292, 271);
            this.txtPath.TabIndex = 5;
            // 
            // FormUpdateClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 372);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.rdoHD);
            this.Controls.Add(this.rdoKD);
            this.Controls.Add(this.btnUpload);
            this.Name = "FormUpdateClient";
            this.Text = "FormUpdateClient";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.RadioButton rdoKD;
        private System.Windows.Forms.RadioButton rdoHD;
        private System.Windows.Forms.TextBox txtPath;
    }
}