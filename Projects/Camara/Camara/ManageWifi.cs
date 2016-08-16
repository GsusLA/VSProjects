using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NativeWifi;

namespace Camara
{
    /** Clase que recupera los dispositivos wifi detectador disponibles y que se usa para validar si el 
     * router predefinido( En este caso PROXIM142563 ) esta dentro del alcance del wifi de la computadora
     en caso de que ya este en el alcance se iniciara un proceso de copiado de datos al servidor predefinido
     que recibira los fragmentos de video
     */
    class ManageWifi
    {
        WlanClient client;
        public ManageWifi()  //Constructor de la clase
        {
            client = new WlanClient(); // Se inicializa la case que recupera los dispositivos Wifi
        }
        /**Metodo que escanea los dispositivos wifi que esten en el alcance de la señal y devuelve un 
         * true si lo ha detectado satisfactoriamente*/
        public bool scanWifi() {

            foreach (WlanClient.WlanInterface wlanIface in client.Interfaces)
            {
                foreach (Wlan.WlanProfileInfo profileInfo in wlanIface.GetProfiles())
                {
                    if (profileInfo.profileName.Equals("PROXIM142563")) {
                        return true;
                    }
                }
                
            }
            return false;
        }
    }
}
