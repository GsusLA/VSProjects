namespace Camaras
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.videoPanel1 = new System.Windows.Forms.Panel();
            this.videoPanel2 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.prevCam1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbAudioCam1 = new System.Windows.Forms.ComboBox();
            this.cbVideoCam1 = new System.Windows.Forms.ComboBox();
            this.recordarCam1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.prevCam2 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbVideoCam2 = new System.Windows.Forms.ComboBox();
            this.cbAudioCam2 = new System.Windows.Forms.ComboBox();
            this.bttGrabar = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // videoPanel1
            // 
            this.videoPanel1.Location = new System.Drawing.Point(72, 21);
            this.videoPanel1.Name = "videoPanel1";
            this.videoPanel1.Size = new System.Drawing.Size(640, 480);
            this.videoPanel1.TabIndex = 0;
            // 
            // videoPanel2
            // 
            this.videoPanel2.Location = new System.Drawing.Point(728, 21);
            this.videoPanel2.Name = "videoPanel2";
            this.videoPanel2.Size = new System.Drawing.Size(640, 480);
            this.videoPanel2.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.prevCam1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbAudioCam1);
            this.groupBox1.Controls.Add(this.cbVideoCam1);
            this.groupBox1.Location = new System.Drawing.Point(72, 507);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(640, 100);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Camara 1";
            // 
            // prevCam1
            // 
            this.prevCam1.Location = new System.Drawing.Point(399, 17);
            this.prevCam1.Name = "prevCam1";
            this.prevCam1.Size = new System.Drawing.Size(75, 23);
            this.prevCam1.TabIndex = 4;
            this.prevCam1.Text = "Vista Previa";
            this.prevCam1.UseVisualStyleBackColor = true;
            this.prevCam1.Click += new System.EventHandler(this.prevCam1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Audio";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Video";
            // 
            // cbAudioCam1
            // 
            this.cbAudioCam1.FormattingEnabled = true;
            this.cbAudioCam1.Location = new System.Drawing.Point(73, 58);
            this.cbAudioCam1.Name = "cbAudioCam1";
            this.cbAudioCam1.Size = new System.Drawing.Size(207, 21);
            this.cbAudioCam1.TabIndex = 1;
            // 
            // cbVideoCam1
            // 
            this.cbVideoCam1.FormattingEnabled = true;
            this.cbVideoCam1.Location = new System.Drawing.Point(73, 20);
            this.cbVideoCam1.Name = "cbVideoCam1";
            this.cbVideoCam1.Size = new System.Drawing.Size(207, 21);
            this.cbVideoCam1.TabIndex = 0;
            // 
            // recordarCam1
            // 
            this.recordarCam1.Location = new System.Drawing.Point(569, 613);
            this.recordarCam1.Name = "recordarCam1";
            this.recordarCam1.Size = new System.Drawing.Size(75, 23);
            this.recordarCam1.TabIndex = 5;
            this.recordarCam1.Text = "Recordar";
            this.recordarCam1.UseVisualStyleBackColor = true;
            this.recordarCam1.Click += new System.EventHandler(this.recordarCam1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.prevCam2);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cbVideoCam2);
            this.groupBox2.Controls.Add(this.cbAudioCam2);
            this.groupBox2.Location = new System.Drawing.Point(728, 507);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(640, 100);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Camara 2";
            // 
            // prevCam2
            // 
            this.prevCam2.Location = new System.Drawing.Point(352, 17);
            this.prevCam2.Name = "prevCam2";
            this.prevCam2.Size = new System.Drawing.Size(75, 23);
            this.prevCam2.TabIndex = 4;
            this.prevCam2.Text = "Vista Previa";
            this.prevCam2.UseVisualStyleBackColor = true;
            this.prevCam2.Click += new System.EventHandler(this.prevCam2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Audio";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Video";
            // 
            // cbVideoCam2
            // 
            this.cbVideoCam2.FormattingEnabled = true;
            this.cbVideoCam2.Location = new System.Drawing.Point(72, 20);
            this.cbVideoCam2.Name = "cbVideoCam2";
            this.cbVideoCam2.Size = new System.Drawing.Size(207, 21);
            this.cbVideoCam2.TabIndex = 0;
            // 
            // cbAudioCam2
            // 
            this.cbAudioCam2.FormattingEnabled = true;
            this.cbAudioCam2.Location = new System.Drawing.Point(72, 58);
            this.cbAudioCam2.Name = "cbAudioCam2";
            this.cbAudioCam2.Size = new System.Drawing.Size(207, 21);
            this.cbAudioCam2.TabIndex = 1;
            // 
            // bttGrabar
            // 
            this.bttGrabar.Location = new System.Drawing.Point(675, 613);
            this.bttGrabar.Name = "bttGrabar";
            this.bttGrabar.Size = new System.Drawing.Size(75, 23);
            this.bttGrabar.TabIndex = 6;
            this.bttGrabar.Text = "Grabar";
            this.bttGrabar.UseVisualStyleBackColor = true;
            this.bttGrabar.Click += new System.EventHandler(this.bttGrabar_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1407, 685);
            this.Controls.Add(this.bttGrabar);
            this.Controls.Add(this.recordarCam1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.videoPanel2);
            this.Controls.Add(this.videoPanel1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Administrador de Video";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel videoPanel1;
        private System.Windows.Forms.Panel videoPanel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button recordarCam1;
        private System.Windows.Forms.Button prevCam1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbAudioCam1;
        private System.Windows.Forms.ComboBox cbVideoCam1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button prevCam2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbVideoCam2;
        private System.Windows.Forms.ComboBox cbAudioCam2;
        private System.Windows.Forms.Button bttGrabar;
    }
}

