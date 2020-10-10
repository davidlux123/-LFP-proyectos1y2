namespace _LFP_Proyecto1
{
    partial class TablaErrores
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
            this.tablaErroresTbl = new System.Windows.Forms.DataGridView();
            this.Numero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Error = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DescripcionError = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FilaErrror = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnaError = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CerrarBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tablaErroresTbl)).BeginInit();
            this.SuspendLayout();
            // 
            // tablaErroresTbl
            // 
            this.tablaErroresTbl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tablaErroresTbl.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Numero,
            this.Error,
            this.DescripcionError,
            this.FilaErrror,
            this.ColumnaError});
            this.tablaErroresTbl.Location = new System.Drawing.Point(12, 70);
            this.tablaErroresTbl.Name = "tablaErroresTbl";
            this.tablaErroresTbl.RowTemplate.Height = 24;
            this.tablaErroresTbl.Size = new System.Drawing.Size(770, 326);
            this.tablaErroresTbl.TabIndex = 0;
            // 
            // Numero
            // 
            this.Numero.HeaderText = "No.";
            this.Numero.Name = "Numero";
            this.Numero.ReadOnly = true;
            // 
            // Error
            // 
            this.Error.HeaderText = "Error";
            this.Error.Name = "Error";
            this.Error.ReadOnly = true;
            // 
            // DescripcionError
            // 
            this.DescripcionError.HeaderText = "Descripcion";
            this.DescripcionError.Name = "DescripcionError";
            this.DescripcionError.ReadOnly = true;
            // 
            // FilaErrror
            // 
            this.FilaErrror.HeaderText = "Fila ";
            this.FilaErrror.Name = "FilaErrror";
            this.FilaErrror.ReadOnly = true;
            // 
            // ColumnaError
            // 
            this.ColumnaError.HeaderText = "Columna";
            this.ColumnaError.Name = "ColumnaError";
            this.ColumnaError.ReadOnly = true;
            // 
            // CerrarBtn
            // 
            this.CerrarBtn.Location = new System.Drawing.Point(622, 415);
            this.CerrarBtn.Name = "CerrarBtn";
            this.CerrarBtn.Size = new System.Drawing.Size(161, 33);
            this.CerrarBtn.TabIndex = 1;
            this.CerrarBtn.Text = "Cerrar";
            this.CerrarBtn.UseVisualStyleBackColor = true;
            this.CerrarBtn.Click += new System.EventHandler(this.AceptarBtn_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(260, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(302, 41);
            this.label1.TabIndex = 2;
            this.label1.Text = "Tabla de Errores Lexicos ";
            // 
            // TablaErrores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 460);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CerrarBtn);
            this.Controls.Add(this.tablaErroresTbl);
            this.Name = "TablaErrores";
            this.Text = "Tabla Tokens";
            ((System.ComponentModel.ISupportInitialize)(this.tablaErroresTbl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView tablaErroresTbl;
        private System.Windows.Forms.Button CerrarBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Numero;
        private System.Windows.Forms.DataGridViewTextBoxColumn Error;
        private System.Windows.Forms.DataGridViewTextBoxColumn DescripcionError;
        private System.Windows.Forms.DataGridViewTextBoxColumn FilaErrror;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnaError;
    }
}