using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final
{
    public class Fabricacion
    {
        public int Modelo { get; set; }
        public int Cantidad { get; set; }

        public Fabricacion(int modelo, int cantidadAFabricar)
        {
            Modelo = modelo;
            Cantidad = cantidadAFabricar;
        }
    }

    public class  DatosFabricacion
    {
        public List<DatosFabricacion> datosReporte = new List<DatosFabricacion>();
    }
}
