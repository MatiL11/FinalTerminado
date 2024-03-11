using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Reflection;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Final
{
    public partial class PanelPrincipal : Form
    {
        public PanelPrincipal()
        {
            InitializeComponent();
        }

        ArrayList pedidos = new ArrayList();
        ArrayList modelos = new ArrayList();
        ArrayList piezas = new ArrayList();

        public void CargarArchivo(string boton)
        {
            //leer el archivo de texto
            string[] lineas = File.ReadAllLines(boton);
            //recorrer el archivo de texto
            foreach (string linea in lineas)
            {
                //separamos los datos por el caracter |
                string[] datos = linea.Split('|');
                Pedido nuevoPedido = new Pedido();
                Modelos nuevoModelo = new Modelos();
                Piezas nuevaPieza = new Piezas();
                if (boton == "Pedidos.txt")
                {
                    nuevoPedido.modelo = Convert.ToInt32(datos[0]);
                    nuevoPedido.nroConcesionaria = Convert.ToInt32(datos[1]);
                    nuevoPedido.cantidad = Convert.ToInt32(datos[2]);
                    pedidos.Add(nuevoPedido);
                }
                else if (boton == "StockTerminado.txt")
                {
                    nuevoModelo.modelo = Convert.ToInt32(datos[0]);
                    nuevoModelo.descripcion = datos[1];
                    nuevoModelo.cantidad = Convert.ToInt32(datos[2]);
                    modelos.Add(nuevoModelo);
                }
                else
                {
                    nuevaPieza.modelo = Convert.ToInt32(datos[0]);
                    nuevaPieza.descripcion = datos[1];
                    nuevaPieza.cantidad = Convert.ToInt32(datos[2]);
                    piezas.Add(nuevaPieza);
                }
            }
            ActualizarDGVDatos(boton);

        }


        public bool SeleccionePedido()
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar un pedido");
            }
            return dgvDatos.SelectedRows.Count != 0;
        }

        public void ActualizarDGVDatos(string boton)
        {
            if (boton == "Pedidos.txt")
            {
                dgvDatos.DataSource = null;
                dgvDatos.DataSource = pedidos;
            }
            else if (boton == "StockPiezas.txt")
            {
                dgvDatos.DataSource = null;
                dgvDatos.DataSource = piezas;
            }
            else
            {
                dgvDatos.DataSource = null;
                dgvDatos.DataSource = modelos;
            }
        }

        public void LimpiarCampos()
        {
            txtModelo.Text = "";
            txtDescripcion.Text = "";
            txtNroConcesionaria.Text = "";
            txtCantidad.Text = "";
        }

        private bool ValidarCamposPedidos()
        {
            if (txtModelo.Text == "" || txtNroConcesionaria.Text == "" || txtCantidad.Text == "")
            {
                MessageBox.Show("Debe completar todos los campos");
                return false;
            }
            return true;
        }

        private bool ValidarCamposModelosPiezas()
        {
            if (txtModelo.Text == "" || txtDescripcion.Text == "")
            {
                MessageBox.Show("Debe completar todos los campos");
                return false;
            }
            return true;
        }

        private bool ValidarCamposModificados()
        {
            if (txtModelo.Text == "" || txtDescripcion.Text == "" || txtCantidad.Text == "")
            {
                MessageBox.Show("Debe completar todos los campos");
                return false;
            }
            return true;
        }


        public void EnviarDatos(string archivo)
        {

            int modelo = Convert.ToInt32(txtModelo.Text);
            int cantidad = Convert.ToInt32(txtCantidad.Text);

            if (modelo < 1 || modelo > 5)
            {
                MessageBox.Show("El modelo debe ser un número entero entre 1 y 5");
                return;
            }

            if (archivo == "StockTerminado.txt")
            {
                if (!ValidarCamposModelosPiezas())
                {
                    return;
                }
                string descripcion = txtDescripcion.Text;

                //instanciar la clase modelo
                Modelos nuevoModelo = new Modelos();
                nuevoModelo.modelo = modelo;
                nuevoModelo.descripcion = descripcion;
                nuevoModelo.cantidad = cantidad;

                modelos.Add(nuevoModelo);
            }
            else if (archivo == "StockPiezas.txt")
            {
                if (!ValidarCamposModelosPiezas())
                {
                    return;
                }
                //instanciar la clase pieza
                Piezas nuevaPieza = new Piezas();
                nuevaPieza.modelo = modelo;
                if (nuevaPieza.modelo == 1)
                {
                    nuevaPieza.descripcion = "Carroceria";
                }
                else if (nuevaPieza.modelo == 2)
                {
                    nuevaPieza.descripcion = "Motor";
                }
                else if (nuevaPieza.modelo == 3)
                {
                    nuevaPieza.descripcion = "Caja de Cambios";
                }
                else if (nuevaPieza.modelo == 4)
                {
                    nuevaPieza.descripcion = "Puertas";
                }
                else
                {
                    nuevaPieza.descripcion = "Ruedas";
                }
                nuevaPieza.cantidad = cantidad;

                piezas.Add(nuevaPieza);
            }
            else
            {
                //validar que el número de concesionaria sea 10, 20, 30, 40 o 50
                if (!int.TryParse(txtNroConcesionaria.Text, out int concesionaria) || (concesionaria != 10 && concesionaria != 20 && concesionaria != 30 && concesionaria != 40 && concesionaria != 50))
                {
                    MessageBox.Show("El número de concesionaria debe ser 10, 20, 30, 40 o 50");
                    return;
                }
                else if (cantidad < 1)
                {
                    MessageBox.Show("La cantidad debe ser un número mayor a 0");
                    return;
                }
                else if (!ValidarCamposPedidos())
                {
                    return;
                }

                //instanciar la clase pedido
                Pedido nuevoPedido = new Pedido();
                nuevoPedido.modelo = modelo;
                nuevoPedido.nroConcesionaria = concesionaria;
                nuevoPedido.cantidad = cantidad;

                var pedidosOrdenados = pedidos.Cast<Pedido>().OrderBy(p => p.modelo).ThenBy(p => p.nroConcesionaria).ToList();
                //agregar el pedido al arraylist
                pedidos.Add(nuevoPedido);

                //ordenar el arraylist
                pedidos.Sort(new ComparadorElemento());
            }

            ActualizarDGVDatos(archivo);
            LimpiarCampos();
            MessageBox.Show("Pedido enviado");
        }

        public void LimpiarArchivo(string archivo)
        {
            TextWriter escribir = new StreamWriter(archivo);
            escribir.Close();
        }

        public void GuardarArchivo(string archivo)
        {
            //creo el archivo de texto si no existe
            TextWriter escribir = new StreamWriter(archivo, true);
            escribir.Close();

            if (archivo == "Pedidos.txt")
            {
                foreach (Pedido pedido in pedidos)
                {
                    escribir = new StreamWriter(archivo, true);
                    escribir.WriteLine(pedido.modelo + "|" + pedido.nroConcesionaria + "|" + pedido.cantidad);
                    escribir.Close();
                }
            }
            else if (archivo == "StockPiezas.txt")
            {
                foreach (Piezas pieza in piezas)
                {
                    escribir = new StreamWriter(archivo, true);
                    escribir.WriteLine(pieza.modelo + "|" + pieza.descripcion + "|" + pieza.cantidad);
                    escribir.Close();
                }
            }
            else
            {
                foreach (Modelos modelo in modelos)
                {
                    escribir = new StreamWriter(archivo, true);
                    escribir.WriteLine(modelo.modelo + "|" + modelo.descripcion + "|" + modelo.cantidad);
                    escribir.Close();
                }
            }

            MessageBox.Show("Archivo guardado");
        }

        public void ModificarPedido(string archivo)
        {
            if (!SeleccionePedido())
            {
                return;
            }
            else if (archivo == "Pedidos.txt")
            {
                if (!ValidarCamposPedidos())
                {
                    return;
                }
                //seleccionamos el pedido que queremos modificar
                Pedido pedido = (Pedido)dgvDatos.CurrentRow.DataBoundItem;

                //modificamos el pedido
                pedido.modelo = Convert.ToInt32(txtModelo.Text);
                pedido.nroConcesionaria = Convert.ToInt32(txtNroConcesionaria.Text);
                pedido.cantidad = Convert.ToInt32(txtCantidad.Text);


                //ordenamos el arraylist
                pedidos.Sort(new ComparadorElemento());
            }
            else if (archivo == "StockTerminado.txt")
            {
                if (!ValidarCamposModificados())
                {
                    return;
                }
                //seleccionamos el pedido que queremos modificar
                Modelos modelo = (Modelos)dgvDatos.CurrentRow.DataBoundItem;

                //modificamos el pedido
                modelo.modelo = Convert.ToInt32(txtModelo.Text);
                modelo.descripcion = txtDescripcion.Text;
                modelo.cantidad = Convert.ToInt32(txtCantidad.Text);
            }
            else
            {
                if (!ValidarCamposModificados())
                {
                    return;
                }
                Piezas pieza = (Piezas)dgvDatos.CurrentRow.DataBoundItem;

                pieza.modelo = Convert.ToInt32(txtModelo.Text);
                pieza.descripcion = txtDescripcion.Text;
                pieza.cantidad = Convert.ToInt32(txtCantidad.Text);
            }

            MessageBox.Show("Pedido modificado");

            ActualizarDGVDatos(archivo);

            LimpiarCampos();
        }

        public void BajaPedido(string archivo)
        {
            if (!SeleccionePedido())
            {
                return;
            }
            else if (archivo == "Pedidos.txt")
            {
                //seleccionamos el pedido que queremos dar de baja
                Pedido pedido = (Pedido)dgvDatos.CurrentRow.DataBoundItem;
                pedidos.Remove(pedido);
            }
            else if (archivo == "StockTerminado.txt")
            {
                //seleccionamos el pedido que queremos dar de baja
                Modelos modelo = (Modelos)dgvDatos.CurrentRow.DataBoundItem;
                modelos.Remove(modelo);
            }
            else
            {
                //seleccionamos el pedido que queremos dar de baja
                Piezas pieza = (Piezas)dgvDatos.CurrentRow.DataBoundItem;
                piezas.Remove(pieza);
            }
            MessageBox.Show("Pedido eliminado");
            ActualizarDGVDatos(archivo);
        }

        private void btnPedidos_Click(object sender, EventArgs e)
        {
            string archivo = "Pedidos.txt";
            CargarArchivo(archivo);
        }


        private void btnModelos_Click(object sender, EventArgs e)
        {
            string archivo = "StockTerminado.txt";
            CargarArchivo(archivo);
        }

        private void btnPiezas_Click(object sender, EventArgs e)
        {
            string archivo = "StockPiezas.txt";
            CargarArchivo(archivo);
        }

        private void btnEnviarPedido_Click(object sender, EventArgs e)
        {
            string archivo = "Pedidos.txt";
            EnviarDatos(archivo);
        }

        private void btnEnviarModelo_Click(object sender, EventArgs e)
        {
            string archivo = "StockTerminado.txt";
            EnviarDatos(archivo);
        }

        private void btnEnviarPieza_Click(object sender, EventArgs e)
        {
            string archivo = "StockPiezas.txt";
            EnviarDatos(archivo);
        }

        private void txtDescripcion_TextChanged(object sender, EventArgs e)
        {
            txtNroConcesionaria.Enabled = false;
            if (txtDescripcion.Text == "")
            {
                txtNroConcesionaria.Enabled = true;
            }
        }

        private void txtNroConcesionaria_TextChanged(object sender, EventArgs e)
        {
            txtDescripcion.Enabled = false;
            if (txtNroConcesionaria.Text == "")
            {
                txtDescripcion.Enabled = true;
            }
        }

        private void btnAltaPedidos_Click(object sender, EventArgs e)
        {
            string archivo = "Pedidos.txt";
            LimpiarArchivo(archivo);
            GuardarArchivo(archivo);
        }

        private void btnModificarPedidos_Click(object sender, EventArgs e)
        {
            string archivo = "Pedidos.txt";
            ModificarPedido(archivo);
        }

        private void btnModificarModelos_Click(object sender, EventArgs e)
        {
            string archivo = "StockTerminado.txt";
            ModificarPedido(archivo);
        }

        private void btnModificarPiezas_Click(object sender, EventArgs e)
        {
            string archivo = "StockPiezas.txt";
            ModificarPedido(archivo);
        }

        private void btnBajaPedidos_Click(object sender, EventArgs e)
        {
            string archivo = "Pedidos.txt";
            BajaPedido(archivo);
        }

        private void btnBajaModelos_Click(object sender, EventArgs e)
        {
            string archivo = "StockTerminado.txt";
            BajaPedido(archivo);
        }

        private void btnBajaPiezas_Click(object sender, EventArgs e)
        {
            string archivo = "StockPiezas.txt";
            BajaPedido(archivo);
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void btnAltaModelos_Click(object sender, EventArgs e)
        {
            string archivo = "StockTerminado.txt";
            LimpiarArchivo(archivo);
            GuardarArchivo(archivo);
        }

        private void btnAltaPiezas_Click(object sender, EventArgs e)
        {
            string archivo = "StockPiezas.txt";
            LimpiarArchivo(archivo);
            GuardarArchivo(archivo);
        }
    }
}
