namespace Final
{
    partial class StockPiezas
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
            this.btnVolver = new System.Windows.Forms.Button();
            this.dgvStockAutomoviles = new System.Windows.Forms.DataGridView();
            this.btnFabricar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockAutomoviles)).BeginInit();
            this.SuspendLayout();
            // 
            // btnVolver
            // 
            this.btnVolver.Location = new System.Drawing.Point(676, 391);
            this.btnVolver.Name = "btnVolver";
            this.btnVolver.Size = new System.Drawing.Size(112, 47);
            this.btnVolver.TabIndex = 1;
            this.btnVolver.Text = "Volver";
            this.btnVolver.UseVisualStyleBackColor = true;
            this.btnVolver.Click += new System.EventHandler(this.btnVolver_Click);
            // 
            // dgvStockAutomoviles
            // 
            this.dgvStockAutomoviles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStockAutomoviles.Location = new System.Drawing.Point(159, 75);
            this.dgvStockAutomoviles.Name = "dgvStockAutomoviles";
            this.dgvStockAutomoviles.Size = new System.Drawing.Size(471, 216);
            this.dgvStockAutomoviles.TabIndex = 2;
            // 
            // btnFabricar
            // 
            this.btnFabricar.Location = new System.Drawing.Point(12, 391);
            this.btnFabricar.Name = "btnFabricar";
            this.btnFabricar.Size = new System.Drawing.Size(112, 47);
            this.btnFabricar.TabIndex = 3;
            this.btnFabricar.Text = "Fabricar";
            this.btnFabricar.UseVisualStyleBackColor = true;
            this.btnFabricar.Click += new System.EventHandler(this.btnFabricar_Click);
            // 
            // StockPiezas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnFabricar);
            this.Controls.Add(this.dgvStockAutomoviles);
            this.Controls.Add(this.btnVolver);
            this.Name = "StockPiezas";
            this.Text = "StockPiezas";
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockAutomoviles)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnVolver;
        private System.Windows.Forms.DataGridView dgvStockAutomoviles;
        private System.Windows.Forms.Button btnFabricar;
    }
}