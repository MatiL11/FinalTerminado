using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Final
{
    internal class Pedido
    {
        //propiedades definidas para trasladar informacion
        public int modelo { get; set; }
        public int nroConcesionaria { get; set; }
        public int cantidad { get; set; }
    }

    class ComparadorElemento : IComparer
    {
        //metodo para comparar los pedidos
        public int Compare(object x, object y)
        {
            Pedido pedidoX = (Pedido)x;
            Pedido pedidoY = (Pedido)y;

            // Primero, comparamos por número de modelo
            int comparacionModelo = pedidoX.modelo.CompareTo(pedidoY.modelo);
            if (comparacionModelo != 0)
            {
                return comparacionModelo;
            }

            // Si los números de modelo son iguales, comparamos por número de concesionaria
            return pedidoX.nroConcesionaria.CompareTo(pedidoY.nroConcesionaria);
        }
    }
}
