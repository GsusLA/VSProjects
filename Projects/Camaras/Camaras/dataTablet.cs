using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camaras
{
    class dataTablet
    {
        string[] datos = new string[5];
        private string camara1 = "";
        private string audio1 = "";
        private string camara2 = "";
        private string audio2 = "";


        public dataTablet() {
            recDatos();
        }
        public void recDatos(){
            
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
            }
            catch (Exception e)
            {
                Console.WriteLine("El archivo no se puede leer!!!" + e.ToString());
            }
        }
        public bool confData() {
            if (datos[0].Equals("TRUE")) return true;
            else return false;
        }
        public string recCamara1() {
            return datos[1];
        }
        public string recAudio1() {
            return datos[2];
        }
        public string recCamara2()
        {
            return datos[3];
        }
        public string recAudio2()
        {
            return datos[4];
        }

    }
}
