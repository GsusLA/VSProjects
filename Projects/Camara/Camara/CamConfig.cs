using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camara
{
    class CamConfig
    {
        string[] datos = new string[3];
        public CamConfig() { 
        }  
        /** Metodo pra leer los datos de configuracion de la camara guardados en el archivo
         * de texto llamado camaras.txt
         */ 
        public string[] recDatos()
        {

            int l = 0;
            string line = "";
            string linea = "";
            try
            {
                using (StreamReader sr = new StreamReader("camaras.txt", false))
                {
                    while ((line = sr.ReadLine()) != null)
                    {

                        linea = line;
                        datos[l] = line;
                        l++;
                    }
                    Console.WriteLine("Pasó" + linea);
                }
                return datos;
            }
            catch (Exception e)
            {
                Console.WriteLine("El archivo no se puede leer!!!" + e.ToString());
            }
            return null;
        }
        /** Metodo para guardar los datos de configuracion de la camara en el archivo de texto
         * llamado camaras.txt
         */ 
        public void guardar(string[] data)
        {
            StreamWriter datos = File.CreateText("camaras.txt");
            datos.WriteLine(data[0]);
            datos.WriteLine(data[1]);
            datos.WriteLine(data[2]);
            datos.Flush();
            datos.Close();
        }
    }
}
