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
using System.Drawing.Drawing2D;

namespace Final
{
    public partial class StockPiezas : Form
    {
        public StockPiezas()
        {
            InitializeComponent();
        }

        public void InicializarDataGridView()
        {
            // Agregar las columnas al DataGridViewStock
            DataGridViewTextBoxColumn columnaPieza = new DataGridViewTextBoxColumn();
            columnaPieza.HeaderText = "Nro de Pieza";
            DataGridViewTextBoxColumn columnaPiezasNecesarias = new DataGridViewTextBoxColumn();
            columnaPiezasNecesarias.HeaderText = "Piezas Necesarias";
            DataGridViewTextBoxColumn columnaStockInicial = new DataGridViewTextBoxColumn();
            columnaStockInicial.HeaderText = "Stock Inicial";
            DataGridViewTextBoxColumn columnaStockFinal = new DataGridViewTextBoxColumn();
            columnaStockFinal.HeaderText = "Stock Final";
            DataGridViewTextBoxColumn columnaMinCompra = new DataGridViewTextBoxColumn();
            columnaMinCompra.HeaderText = "Minimo A Comprar";

            // Agregar las columnas al DataGridViewStock
            dgvStockAutomoviles.Columns.Add(columnaPieza);
            dgvStockAutomoviles.Columns.Add(columnaPiezasNecesarias);
            dgvStockAutomoviles.Columns.Add(columnaStockInicial);
            dgvStockAutomoviles.Columns.Add(columnaStockFinal);
            dgvStockAutomoviles.Columns.Add(columnaMinCompra);
        }

        public class ModeloVehiculo
        {
            public int Modelo { get; set; }
            public int StockInicial { get; set; }
            public int CantidadAFabricar { get; set; }
            public Dictionary<int, int> PiezasNecesarias { get; set; }

            public ModeloVehiculo(int modelo, int stockInicial, int cantidadAFabricar, Dictionary<int, int> piezasNecesarias)
            {
                Modelo = modelo;
                StockInicial = stockInicial;
                CantidadAFabricar = cantidadAFabricar;
                PiezasNecesarias = piezasNecesarias;
            }
        }

        public void GenerarReporteAutomoviles()
        {
            try
            {
                InicializarDataGridView();

                // Leer el archivo de pedidos
                string[] lineasPedidos = File.ReadAllLines("Pedidos.txt");
                Dictionary<int, int> cantidadPedidaPorModelo = new Dictionary<int, int>();

                // Recorrer el archivo de pedidos para acumular la cantidad pedida por modelo
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

                // Leer los datos del archivo de composición de vehículos
                string[] lineasComposicion = File.ReadAllLines("ComposicionVehiculo.txt");
                Dictionary<int, Dictionary<int, int>> composicionesVehiculos = new Dictionary<int, Dictionary<int, int>>();

                // Recorrer cada línea del archivo y guardar los datos en el diccionario
                foreach (string lineaComposicion in lineasComposicion)
                {
                    string[] datosComposicion = lineaComposicion.Split(',');
                    int modelo = Convert.ToInt32(datosComposicion[0]);
                    Dictionary<int, int> piezasNecesarias = new Dictionary<int, int>();

                    // Iterar a través de los subvalores (a partir del segundo elemento)
                    for (int i = 1; i < datosComposicion.Length; i++)
                    {
                        string[] piezaCantidad = datosComposicion[i].Trim().Split('-');

                        if (piezaCantidad.Length >= 2)
                        {
                            int numeroPieza = Convert.ToInt32(piezaCantidad[0]);
                            int cantidadPiezas = Convert.ToInt32(piezaCantidad[1]);

                            if (piezasNecesarias.ContainsKey(numeroPieza))
                            {
                                piezasNecesarias[numeroPieza] += cantidadPiezas;
                            }
                            else
                            {
                                piezasNecesarias.Add(numeroPieza, cantidadPiezas);
                            }
                        }
                    }

                    composicionesVehiculos.Add(modelo, piezasNecesarias);
                }

                // Leer los datos del archivo AFabricar.txt
                string[] lineasAFabricar = File.ReadAllLines("AFabricar.txt");
                Dictionary<int, int> cantidadAFabricarPorModelo = new Dictionary<int, int>();

                // Recorrer cada línea del archivo y guardar los datos en el diccionario
                foreach (string lineaAFabricar in lineasAFabricar)
                {
                    string[] datosAFabricar = lineaAFabricar.Split('|');
                    int modelo = Convert.ToInt32(datosAFabricar[0]);
                    int cantidadAFabricar = Convert.ToInt32(datosAFabricar[1]);

                    cantidadAFabricarPorModelo.Add(modelo, cantidadAFabricar);
                }

                // Leer el archivo de stock terminado
                string[] lineasStockTerminado = File.ReadAllLines("StockPiezas.txt");
                Dictionary<int, int> stockInicialPiezas = new Dictionary<int, int>();

                // Recorrer el archivo de stock terminado para obtener el stock inicial de piezas
                foreach (string lineaStockTerminado in lineasStockTerminado)
                {
                    string[] datosStockTerminado = lineaStockTerminado.Split('|');
                    int numeroPieza = Convert.ToInt32(datosStockTerminado[0]);
                    int stockInicial = Convert.ToInt32(datosStockTerminado[2]);

                    stockInicialPiezas.Add(numeroPieza, stockInicial);
                }

                // Limpiar el dgvStockAutomoviles antes de agregar nuevos datos
                dgvStockAutomoviles.Rows.Clear();

                // Crear el reporte y agregarlo al dgvStockAutomoviles
                foreach (var kvp in composicionesVehiculos)
                {
                    int modelo = kvp.Key;
                    Dictionary<int, int> piezasNecesariasPorPieza = kvp.Value;
                    int cantidadAFabricar = cantidadAFabricarPorModelo.ContainsKey(modelo) ? cantidadAFabricarPorModelo[modelo] : 0;
                    int stockInicialPiezasModelo = stockInicialPiezas.ContainsKey(modelo) ? stockInicialPiezas[modelo] : 0;

                    // Calcular el total de piezas necesarias para el modelo
                    int totalPiezasNecesarias = piezasNecesariasPorPieza.Values.Sum() * cantidadAFabricar;

                    // Calcular el stock final de piezas
                    int stockFinalPiezas = Math.Max(0, stockInicialPiezasModelo - totalPiezasNecesarias);
                    int aux = stockInicialPiezasModelo - totalPiezasNecesarias;

                    // Calcular el mínimo a fabricar si no alcanza el stock
                    int minimoAFabricar = aux < 0 ? Math.Abs(aux) : 0;

                    // Agregar la fila al dgvStockAutomoviles
                    dgvStockAutomoviles.Rows.Add(modelo, totalPiezasNecesarias, stockInicialPiezasModelo, stockFinalPiezas, minimoAFabricar);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el reporte: " + ex.Message);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Form1 formPrincipal = new Form1();
            formPrincipal.Show();
            Hide();
        }

        private void btnFabricar_Click(object sender, EventArgs e)
        {
            GenerarReporteAutomoviles();
        }
    }
}
