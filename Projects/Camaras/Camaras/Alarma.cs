    using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Camaras
{
    class Alarma
    {
        string latitud;
        string longitud;
        int stat = 0;
        string[] datos = new string[5];
        public bool alarmado() {
            int l = 0;
            string line = "";
            string linea = "";
            try
            {
                using (StreamReader sr = new StreamReader(@"D:\alertas\alarma.txt", false))
                {


                    while ((line = sr.ReadLine()) != null)
                    {

                        linea = line;
                        datos[l] = line;
                        l++;
                    }
                    Console.WriteLine("Pasó" + linea);
                }

                if (datos[0].Equals("TRUE"))
                {

                    return true;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("El archivo no se puede leer!!!" + e.ToString());
            }
            
            return false;
        }
        public bool revEstado() {
            string conexion = "datasource=www.pagocel.club;port=3306;username=cliente;password=c1234;database=db_patrullas";
            string query = "SELECT latitud,longitud,status FROM tbl_flotilla WHERE placa = 'U-002';";
            MySqlConnection conect = new MySqlConnection(conexion);
            MySqlCommand comando = new MySqlCommand(query, conect);
            MySqlDataReader resultado;
            try
            {
                conect.Open();
                resultado = comando.ExecuteReader();
                if(resultado.HasRows){
                    while (resultado.Read()) {
                        if (resultado.GetInt16("status") == 3)
                        {
                            latitud = resultado.GetString("latitud");
                            longitud = resultado.GetString("longitud");
                            return true;
                        }
                        else { 

                        }
                    }
                }

            }
            catch (Exception)
            {
                
                throw;
            }
            return false;
        }
        public string getLatitud()
        {
            return latitud;
        }
        public string getLongitud()
        {
            return longitud;
        }
    }
}
