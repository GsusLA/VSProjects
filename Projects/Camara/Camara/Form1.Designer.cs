namespace Camara
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
            this.panelVideoPreview = new System.Windows.Forms.Panel();
            this.listaAudio = new System.Windows.Forms.ListBox();
            this.listaVideo = new System.Windows.Forms.ListBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnGrabar = new System.Windows.Forms.Button();
            this.btnVisualizar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panelVideoPreview
            // 
            this.panelVideoPreview.Location = new System.Drawing.Point(374, 12);
            this.panelVideoPreview.Name = "panelVideoPreview";
            this.panelVideoPreview.Size = new System.Drawing.Size(640, 480);
            this.panelVideoPreview.TabIndex = 1;
            // 
            // listaAudio
            // 
            this.listaAudio.FormattingEnabled = true;
            this.listaAudio.Location = new System.Drawing.Point(12, 126);
            this.listaAudio.Name = "listaAudio";
            this.listaAudio.Size = new System.Drawing.Size(338, 108);
            this.listaAudio.TabIndex = 6;
            // 
            // listaVideo
            // 
            this.listaVideo.FormattingEnabled = true;
            this.listaVideo.Location = new System.Drawing.Point(12, 12);
            this.listaVideo.Name = "listaVideo";
            this.listaVideo.Size = new System.Drawing.Size(338, 108);
            this.listaVideo.TabIndex = 5;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Location = new System.Drawing.Point(39, 253);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(75, 23);
            this.btnGuardar.TabIndex = 7;
            this.btnGuardar.Text = "Salvar Conf";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnGrabar
            // 
            this.btnGrabar.Location = new System.Drawing.Point(122, 307);
            this.btnGrabar.Name = "btnGrabar";
            this.btnGrabar.Size = new System.Drawing.Size(75, 23);
            this.btnGrabar.TabIndex = 8;
            this.btnGrabar.Text = "Grabar";
            this.btnGrabar.UseVisualStyleBackColor = true;
            this.btnGrabar.Click += new System.EventHandler(this.btnGrabar_Click);
            // 
            // btnVisualizar
            // 
            this.btnVisualizar.Location = new System.Drawing.Point(206, 253);
            this.btnVisualizar.Name = "btnVisualizar";
            this.btnVisualizar.Size = new System.Drawing.Size(75, 23);
            this.btnVisualizar.TabIndex = 9;
            this.btnVisualizar.Text = "Visualizar";
            this.btnVisualizar.UseVisualStyleBackColor = true;
            this.btnVisualizar.Click += new System.EventHandler(this.btnVisualizar_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1031, 511);
            this.Controls.Add(this.btnVisualizar);
            this.Controls.Add(this.btnGrabar);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.listaAudio);
            this.Controls.Add(this.listaVideo);
            this.Controls.Add(this.panelVideoPreview);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelVideoPreview;
        private System.Windows.Forms.ListBox listaAudio;
        private System.Windows.Forms.ListBox listaVideo;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnGrabar;
        private System.Windows.Forms.Button btnVisualizar;
    }
}

