using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NativeWifi;

namespace Camaras
{
    class ManagedWifi
    {
        WlanClient client;
        public ManagedWifi() {
            client = new WlanClient();
        }
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
