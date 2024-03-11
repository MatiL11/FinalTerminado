using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Final
{
    public partial class PanelComposiciones : Form
    {
        public PanelComposiciones()
        {
            InitializeComponent();
        }

        ArrayList compos = new ArrayList();
        ArrayList vehiculos = new ArrayList();

        private bool ValidarCampos()
        {
            if (txtModelo.Text == "" || txtPieza.Text == "" || txtCantidad.Text == "")
            {
                MessageBox.Show("Debe completar todos los campos");
                return false;
            }
            return true;
        }

        public void LimpiarCampos()
        {
            txtModelo.Text = "";
            txtPieza.Text = "";
            txtCantidad.Text = "";
        }

        public void ActualizarDGVDatos()
        {
            dgvDatos.DataSource = null;
            dgvDatos.DataSource = compos;
        }

        public void LimpiarArchivo()
        {
            TextWriter escribir = new StreamWriter("ComposicionVehiculo.txt");
            escribir.Close();
        }

        public void GuardarArchivo()
        {
            LimpiarArchivo();
            TextWriter escribir = new StreamWriter("ComposicionVehiculo.txt", true);

            int contador = 0;
            string linea = "";

            foreach (Composiciones composicion in compos)
            {
                if (contador < 4) // Agregar 4 composiciones en la misma línea
                {
                    linea += composicion.modelo + "," + composicion.pieza + "," + composicion.cantidad + "-";
                    contador++;
                }
                else // Cuando se alcanzan 5 composiciones, escribir en el archivo y reiniciar contador y línea
                {
                    linea += composicion.modelo + "," + composicion.pieza + "," + composicion.cantidad;
                    escribir.WriteLine(linea);
                    contador = 0;
                    linea = "";
                }
            }

            if (!string.IsNullOrEmpty(linea)) // Escribir la última línea si hay composiciones pendientes
            {
                escribir.WriteLine(linea);
            }

            escribir.Close();
            MessageBox.Show("Archivo guardado correctamente");
        }

        public void CargarArchivo()
        {
            if (File.Exists("ComposicionVehiculo.txt"))
            {
                TextReader leer = new StreamReader("ComposicionVehiculo.txt");
                string linea = "";
                while ((linea = leer.ReadLine()) != null)
                {
                    string[] composiciones = linea.Split('-');

                    foreach (string composicionData in composiciones)
                    {
                        string[] datos = composicionData.Split(',');

                        if (datos.Length == 3) // Verificar si hay suficientes datos para crear una composición completa
                        {
                            Composiciones composicion = new Composiciones();
                            composicion.modelo = Convert.ToInt32(datos[0]);
                            composicion.pieza = Convert.ToInt32(datos[1]);
                            composicion.cantidad = Convert.ToInt32(datos[2]);
                            compos.Add(composicion);
                        }
                    }
                }
                leer.Close();
                ActualizarDGVDatos();
            }
        }



        public void BajaPedido()
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar un pedido");
                return;
            }

            int indice = dgvDatos.SelectedRows[0].Index;
            compos.RemoveAt(indice);
            ActualizarDGVDatos();
            MessageBox.Show("Pedido eliminado");
        }

        public void ModificarPedido()
        {

            if (!ValidarCampos())
            {
                return;
            }
            else if (dgvDatos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar un pedido");
                return;
            }

            //seleccionamos el pedido que queremos modificar
            Composiciones composicion = (Composiciones)dgvDatos.CurrentRow.DataBoundItem;

            //modificamos el pedido
            composicion.modelo = Convert.ToInt32(txtModelo.Text);
            composicion.pieza = Convert.ToInt32(txtPieza.Text);
            composicion.cantidad = Convert.ToInt32(txtCantidad.Text);
            ActualizarDGVDatos();
        }

        public void EnviarDatos()
        {
            if (!ValidarCampos())
            {
                return;
            }

            int modelo = Convert.ToInt32(txtModelo.Text);
            int cantidad = Convert.ToInt32(txtCantidad.Text);
            int pieza = Convert.ToInt32(txtPieza.Text);


            if (modelo < 1 || modelo > 5)
            {
                MessageBox.Show("El modelo debe ser un número entero entre 1 y 5");
                return;
            }
            else if (pieza < 1 || pieza > 5)
            {
                MessageBox.Show("La pieza debe ser un número entero entre 1 y 5");
                return;
            }
            
            Composiciones composicion = new Composiciones();
            composicion.modelo = modelo;
            composicion.pieza = pieza;
            composicion.cantidad = cantidad;

            compos.Add(composicion);

            ActualizarDGVDatos();
            LimpiarCampos();
            MessageBox.Show("Pedido enviado");
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            EnviarDatos();
        }

        private void btnAlta_Click(object sender, EventArgs e)
        {
            LimpiarArchivo();
            GuardarArchivo();
        }

        private void btnBaja_Click(object sender, EventArgs e)
        {
            BajaPedido();
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            CargarArchivo();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            ModificarPedido();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Hide();
        }
    }
}
