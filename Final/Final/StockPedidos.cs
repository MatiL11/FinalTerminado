using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace Final
{
    public partial class StockPedidos : Form
    {
        public StockPedidos()
        {
            InitializeComponent();
        }

        public void InicializarDataGridViewStock()
        {
            // Agregar las columnas al DataGridViewStock
            DataGridViewTextBoxColumn columnaModelo = new DataGridViewTextBoxColumn();
            columnaModelo.HeaderText = "Modelo";
            DataGridViewTextBoxColumn columnaStockInicial = new DataGridViewTextBoxColumn();
            columnaStockInicial.HeaderText = "Stock Inicial";
            DataGridViewTextBoxColumn columnaCantidadPedida = new DataGridViewTextBoxColumn();
            columnaCantidadPedida.HeaderText = "Cantidad Pedida";
            DataGridViewTextBoxColumn columnaStockFinal = new DataGridViewTextBoxColumn();
            columnaStockFinal.HeaderText = "Stock Final";
            DataGridViewTextBoxColumn columnaAFabricar = new DataGridViewTextBoxColumn();
            columnaAFabricar.HeaderText = "A Fabricar";

            // Agregar las columnas al DataGridViewStock
            dgvStockPedidos.Columns.Add(columnaModelo);
            dgvStockPedidos.Columns.Add(columnaStockInicial);
            dgvStockPedidos.Columns.Add(columnaCantidadPedida);
            dgvStockPedidos.Columns.Add(columnaStockFinal);
            dgvStockPedidos.Columns.Add(columnaAFabricar);
        }

        public void GenerarReporte()
        {
            InicializarDataGridViewStock();
            // Leer el archivo de pedidos
            string[] lineasPedidos = File.ReadAllLines("Pedidos.txt");

            // Crear un diccionario para almacenar la cantidad pedida por modelo
            Dictionary<int, int> cantidadPedidaPorModelo = new Dictionary<int, int>();

            // Recorrer el archivo de pedidos para obtener la cantidad pedida por modelo
            foreach (string lineaPedido in lineasPedidos)
            {
                string[] datosPedido = lineaPedido.Split('|');
                int modelo = Convert.ToInt32(datosPedido[0]);
                int cantidad = Convert.ToInt32(datosPedido[2]);

                if (cantidadPedidaPorModelo.ContainsKey(modelo))
                {
                    cantidadPedidaPorModelo[modelo] += cantidad;
                }
                else
                {
                    cantidadPedidaPorModelo.Add(modelo, cantidad);
                }
            }

            // Limpiar el dgvStockPedidos antes de agregar nuevos datos
            dgvStockPedidos.Rows.Clear();

            // Crear el reporte y agregarlo al dgvStockPedidos
            Random random = new Random();
            ArrayList fabricar = new ArrayList();
            foreach (var kvp in cantidadPedidaPorModelo)
            {
                int modelo = kvp.Key;
                int cantidadPedida = kvp.Value;

                // Generar un stock inicial aleatorio
                int stockInicial = random.Next(1, 200); // Números aleatorios entre 1 y 200

                // Calcular el stock final y la cantidad a fabricar
                int stockFinal = stockInicial - cantidadPedida;
                int aux = stockFinal;
                int stockFinalAux = Math.Max(0, stockInicial - cantidadPedida);
                int aFabricar = aux < 0 ? Math.Abs(aux) : 0;

                // Agregar la fila al dgvStockPedidos
                dgvStockPedidos.Rows.Add(modelo, stockInicial, cantidadPedida, stockFinalAux, aFabricar);

                // Guardar los datos en un ArrayList
                fabricar.Add(new Fabricacion(modelo, aFabricar));
            }

            // Escribir los datos en el archivo AFabricar.txt
            using (StreamWriter sw = new StreamWriter("AFabricar.txt"))
            {
                foreach (Fabricacion fabricacion in fabricar)
                {
                    sw.WriteLine(fabricacion.Modelo + "|" + fabricacion.Cantidad);
                }
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Form1 formPrincipal = new Form1();
            formPrincipal.Show();
            Hide();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            GenerarReporte();
        }
    }
}
