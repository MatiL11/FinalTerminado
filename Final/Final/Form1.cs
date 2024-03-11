using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Final
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnPanelPrincipal_Click(object sender, EventArgs e)
        {
            PanelPrincipal formPrincipal = new PanelPrincipal();
            formPrincipal.Show();
            Hide();
        }

        private void btnComposiciones_Click(object sender, EventArgs e)
        {
            PanelComposiciones formComposiciones = new PanelComposiciones();
            formComposiciones.Show();
            Hide();
        }

        private void btnStockPedidos_Click(object sender, EventArgs e)
        {
            StockPedidos stockPedidos = new StockPedidos();
            stockPedidos.Show();
            Hide();
        }

        private void btnStockPiezas_Click(object sender, EventArgs e)
        {
            StockPiezas stockPiezas = new StockPiezas();
            stockPiezas.Show();
            Hide();
        }
    }
}
