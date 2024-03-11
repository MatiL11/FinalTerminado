namespace Final
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
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
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.btnPanelPrincipal = new System.Windows.Forms.Button();
            this.btnComposiciones = new System.Windows.Forms.Button();
            this.btnStockPedidos = new System.Windows.Forms.Button();
            this.btnStockPiezas = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.25F);
            this.label1.Location = new System.Drawing.Point(31, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 22);
            this.label1.TabIndex = 3;
            this.label1.Text = "Menu Principal";
            // 
            // btnPanelPrincipal
            // 
            this.btnPanelPrincipal.Location = new System.Drawing.Point(44, 53);
            this.btnPanelPrincipal.Margin = new System.Windows.Forms.Padding(1);
            this.btnPanelPrincipal.Name = "btnPanelPrincipal";
            this.btnPanelPrincipal.Size = new System.Drawing.Size(89, 39);
            this.btnPanelPrincipal.TabIndex = 6;
            this.btnPanelPrincipal.Text = "Panel Principal";
            this.btnPanelPrincipal.UseVisualStyleBackColor = true;
            this.btnPanelPrincipal.Click += new System.EventHandler(this.btnPanelPrincipal_Click);
            // 
            // btnComposiciones
            // 
            this.btnComposiciones.Location = new System.Drawing.Point(44, 94);
            this.btnComposiciones.Margin = new System.Windows.Forms.Padding(1);
            this.btnComposiciones.Name = "btnComposiciones";
            this.btnComposiciones.Size = new System.Drawing.Size(89, 39);
            this.btnComposiciones.TabIndex = 7;
            this.btnComposiciones.Text = "Composiciones";
            this.btnComposiciones.UseVisualStyleBackColor = true;
            this.btnComposiciones.Click += new System.EventHandler(this.btnComposiciones_Click);
            // 
            // btnStockPedidos
            // 
            this.btnStockPedidos.Location = new System.Drawing.Point(44, 135);
            this.btnStockPedidos.Margin = new System.Windows.Forms.Padding(1);
            this.btnStockPedidos.Name = "btnStockPedidos";
            this.btnStockPedidos.Size = new System.Drawing.Size(89, 39);
            this.btnStockPedidos.TabIndex = 8;
            this.btnStockPedidos.Text = "Stock Pedidos";
            this.btnStockPedidos.UseVisualStyleBackColor = true;
            this.btnStockPedidos.Click += new System.EventHandler(this.btnStockPedidos_Click);
            // 
            // btnStockPiezas
            // 
            this.btnStockPiezas.Location = new System.Drawing.Point(44, 176);
            this.btnStockPiezas.Margin = new System.Windows.Forms.Padding(1);
            this.btnStockPiezas.Name = "btnStockPiezas";
            this.btnStockPiezas.Size = new System.Drawing.Size(89, 39);
            this.btnStockPiezas.TabIndex = 9;
            this.btnStockPiezas.Text = "Fabricacion";
            this.btnStockPiezas.UseVisualStyleBackColor = true;
            this.btnStockPiezas.Click += new System.EventHandler(this.btnStockPiezas_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Highlight;
            this.ClientSize = new System.Drawing.Size(187, 251);
            this.Controls.Add(this.btnStockPiezas);
            this.Controls.Add(this.btnStockPedidos);
            this.Controls.Add(this.btnComposiciones);
            this.Controls.Add(this.btnPanelPrincipal);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Name = "Form1";
            this.Text = "Menu Principal";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPanelPrincipal;
        private System.Windows.Forms.Button btnComposiciones;
        private System.Windows.Forms.Button btnStockPedidos;
        private System.Windows.Forms.Button btnStockPiezas;
    }
}

