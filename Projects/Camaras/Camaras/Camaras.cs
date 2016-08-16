using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Expression.Encoder.Devices;
using Microsoft.Expression.Encoder.Live;

namespace Camaras
{
    class Camaras
    {
        private LiveDeviceSource _deviceSource, _deviceSource2;
        private LiveJob _job1, _job2;
        public Camaras() { 

        }
        public void guardar(string[] data )
        {
            StreamWriter datos = File.CreateText("camaras.txt");
            datos.WriteLine(data[0]);
            datos.WriteLine(data[1]);
            datos.WriteLine(data[2]);
            datos.WriteLine(data[3]);
            datos.WriteLine(data[4]);
            datos.Flush();
            datos.Close();
        }
        public void getVideoAudio(out EncoderDevice video, out EncoderDevice audio, string[] datos)
        {
            video = null;
            audio = null;
            if (datos[0] == "" || datos[1] == "")
            {
                MessageBox.Show("No Video and Audio capture devices have been selected.\nSelect an audio and video devices from the listboxes and try again.", "Warning");
                return;
            }

            // Get the selected video device            
            foreach (EncoderDevice edv in EncoderDevices.FindDevices(EncoderDeviceType.Video))
            {
                if (String.Compare(edv.Name, datos[0]) == 0)
                {
                    video = edv;
                    break;
                }
            }

            // Get the selected audio device            
            foreach (EncoderDevice eda in EncoderDevices.FindDevices(EncoderDeviceType.Audio))
            {
                if (String.Compare(eda.Name, datos[1]) == 0)
                {
                    audio = eda;
                    break;
                }
            }
        }
    }
}
