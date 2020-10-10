namespace _LFP_Proyecto1
{
    partial class TablaTokens
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
            this.tablaTokensTbl = new System.Windows.Forms.DataGridView();
            this.Numero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Lexama = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tipoLexema = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FilaToken = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnaToken = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cerrarbtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tablaTokensTbl)).BeginInit();
            this.SuspendLayout();
            // 
            // tablaTokensTbl
            // 
            this.tablaTokensTbl.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tablaTokensTbl.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Numero,
            this.Lexama,
            this.tipoLexema,
            this.FilaToken,
            this.ColumnaToken});
            this.tablaTokensTbl.Location = new System.Drawing.Point(13, 63);
            this.tablaTokensTbl.Name = "tablaTokensTbl";
            this.tablaTokensTbl.RowTemplate.Height = 24;
            this.tablaTokensTbl.Size = new System.Drawing.Size(775, 316);
            this.tablaTokensTbl.TabIndex = 0;
            // 
            // Numero
            // 
            this.Numero.HeaderText = "No.";
            this.Numero.Name = "Numero";
            this.Numero.ReadOnly = true;
            // 
            // Lexama
            // 
            this.Lexama.HeaderText = "Lexema";
            this.Lexama.Name = "Lexama";
            this.Lexama.ReadOnly = true;
            // 
            // tipoLexema
            // 
            this.tipoLexema.HeaderText = "Tipo";
            this.tipoLexema.Name = "tipoLexema";
            this.tipoLexema.ReadOnly = true;
            // 
            // FilaToken
            // 
            this.FilaToken.HeaderText = "Fila";
            this.FilaToken.Name = "FilaToken";
            this.FilaToken.ReadOnly = true;
            // 
            // ColumnaToken
            // 
            this.ColumnaToken.HeaderText = "Columna";
            this.ColumnaToken.Name = "ColumnaToken";
            this.ColumnaToken.ReadOnly = true;
            // 
            // cerrarbtn
            // 
            this.cerrarbtn.Location = new System.Drawing.Point(639, 392);
            this.cerrarbtn.Name = "cerrarbtn";
            this.cerrarbtn.Size = new System.Drawing.Size(149, 33);
            this.cerrarbtn.TabIndex = 1;
            this.cerrarbtn.Text = "Cerrar";
            this.cerrarbtn.UseVisualStyleBackColor = true;
            this.cerrarbtn.Click += new System.EventHandler(this.atrasbtn_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(301, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(250, 36);
            this.label1.TabIndex = 2;
            this.label1.Text = "Tabla de Simbolos";
            // 
            // TablaTokens
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 440);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cerrarbtn);
            this.Controls.Add(this.tablaTokensTbl);
            this.Name = "TablaTokens";
            this.Text = "Simbolos";
            ((System.ComponentModel.ISupportInitialize)(this.tablaTokensTbl)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView tablaTokensTbl;
        private System.Windows.Forms.Button cerrarbtn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Numero;
        private System.Windows.Forms.DataGridViewTextBoxColumn Lexama;
        private System.Windows.Forms.DataGridViewTextBoxColumn tipoLexema;
        private System.Windows.Forms.DataGridViewTextBoxColumn FilaToken;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnaToken;
        private System.Windows.Forms.Label label1;
    }
}