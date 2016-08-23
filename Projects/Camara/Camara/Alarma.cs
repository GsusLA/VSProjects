using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MySql.Data;
using MySql.Data.MySqlClient;

namespace Camara
{
    class Alarma
    {
        double latitud, longitud;
        public Alarma() { 
        }
        public bool revEstado()
        {
            string conexion = "datasource=www.pagocel.club;port=3306;username=cliente;password=c1234;database=db_patrullas";
            string query = "SELECT latitud,longitud,status FROM tbl_flotilla WHERE placa = 'U-002';";
            MySqlConnection conect = new MySqlConnection(conexion);
            MySqlCommand comando = new MySqlCommand(query, conect);
            MySqlDataReader resultado;
            try
            {
                conect.Open();
                resultado = comando.ExecuteReader();
                if (resultado.HasRows)
                {
                    while (resultado.Read())
                    {
                        if (resultado.GetInt16("status") == 3)
                        {
                            latitud = resultado.GetDouble("latitud");
                            longitud = resultado.GetDouble("longitud");
                            return true;
                        }
                        else
                        {

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

        public double getlLatitud() {
            return latitud;
        }
        public double getLongitud() {
            return longitud;
        }
    }
}
