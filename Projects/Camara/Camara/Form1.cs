using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Ensamblados para el uso del Encoder
using Microsoft.Expression.Encoder.Devices;
using Microsoft.Expression.Encoder.Live;
using Microsoft.Expression.Encoder;
using System.Runtime.InteropServices;

namespace Camara
{
    public partial class Form1 : Form
    {
        private LiveJob _job;
        private LiveDeviceSource _deviceSource;
        FileArchivePublishFormat fileOut;
        CamConfig camara;
        string[] datoCamaras = null;
        System.Windows.Forms.Timer TareaGrabacionOrmal;
        public Form1()
        {
            InitializeComponent();
            
            // Inicializa las clases y metodos necesarios
            camara = new CamConfig();
            buscarCamaras();
            InicioAutomatico();
        }
        
        private void btnGrabar_Click(object sender, EventArgs e)
        {
            Grabar();
        }
        private void btnVisualizar_Click(object sender, EventArgs e)
        {
            Preview();
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string[] datos = new string[5];
            datos[0] = "TRUE";
            datos[1] = listaVideo.SelectedItem.ToString();
            datos[2] = listaAudio.SelectedItem.ToString();
            camara.guardar(datos);
        }

        /*** Metodo para iniciar automaticamente la camara cuando existe una configuracion guardada
         * El archivo de configuracion es camaras.txt y debe de colocarse en el mismo lugar que el
         * archivo ejecutable del programa, el archivo txt tiene tres lineas con los siguientes valores
         *  1.- un booleano que indica que la configuracion es valida, este valor se debe cambiar a 
         *      FALSE cuando se requiera reconfigurar una camara
         *  2.- Esta linea tiene el nombre del dispositivo de video configurado
         *  3.- Esta linea tiene el nombre del microfono seleccionado
         */
        private void InicioAutomatico() {
            datoCamaras = camara.recDatos();
            if (datoCamaras != null)
            {
                if (datoCamaras[0].Equals("TRUE")) {
                    Preview();
                    Grabar();

                    TareaGrabacionOrmal = new System.Windows.Forms.Timer();
                    TareaGrabacionOrmal.Interval = 1000 * 60 * 60;
                    TareaGrabacionOrmal.Tick += new EventHandler(this.grabacion);

                    TareaGrabacionOrmal.Enabled = true;
                    //MessageBox.Show("si hay datos de dispositivo", "Mensaje");
                }

            }
        }
        /** Metodo para buscar los dispositivos de audio y video disponibles en la computadora
         */ 
        private void buscarCamaras() {
            listaVideo.ClearSelected();
            foreach (EncoderDevice edv in EncoderDevices.FindDevices(EncoderDeviceType.Video))
            {
                listaVideo.Items.Add(edv.Name);
            }

            listaAudio.ClearSelected();
            foreach (EncoderDevice eda in EncoderDevices.FindDevices(EncoderDeviceType.Audio))
            {
                listaAudio.Items.Add(eda.Name);
            }
        }
        /**Metodo para iniciar y detener la grabacion del video
         * este metodo se utiliza de dos maneras:
         * La primera es cuando el inicio automatico esta activado y este inicia y detiene automaticamente la 
         * grabacion del video.
         * La segunda es cuando el inicio automatico esta desactivado y en este caso se puede utilizar de manera
         * manual con el uso del boton de Grabar
         */ 
        private void Grabar() {
            // Obtenemos la opcion del boton para saber que caso aplicar de acuerdo a la eleccion
            switch (btnGrabar.Text)
            {
                case "Grabar":
                    // Se configura el formato del archivo de salida
                    fileOut = null;
                    fileOut = new FileArchivePublishFormat();


                    // Se indica en que direccion se desea guardar el archivo, el formato y el nombre
                    fileOut.OutputFileName = String.Format("D:\\VIDEO\\WebCam {0:yyyyMMdd_hhmmss}.wmv", DateTime.Now);

                    // Se adjunta el formato de salida del video
                    _job.PublishFormats.Add(fileOut);

                    // Se inicia la grabacion
                    _job.StartEncoding();
                    btnGrabar.Text = "Detener";
                    break;
                case "Detener":
                    // Se detiene la grabacion
                    _job.StopEncoding();
                    // Remove the Device Source and destroy the job
                    _job.RemoveDeviceSource(_deviceSource);

                    // Destroy the device source
                    _deviceSource.PreviewWindow = null;
                    _deviceSource = null;
                    _job = null;
                    btnGrabar.Text = "Grabar";
                    break;
            }
        }
        /** Metodo para iniciar la visualizacion del video en la pantalla del programa, tambien este metodo
         * actualiza los datos para generar un nuevo archivo de video 
         */ 
        private void Preview() {
            EncoderDevice video = null;
            EncoderDevice audio = null;

            GetSelectedVideoAndAudioDevices(out video, out audio, datoCamaras);
            _job = new LiveJob();

            //Revisar si el audio y video del dispositivo esta disponible
            if (video != null && audio != null)
            {
                // Crea una fuente de imagen y audio de acuerdo al dispositivo elegido
                _deviceSource = _job.AddDeviceSource(video, audio);
                // Se configura la resolucion de la imagen
                _deviceSource.PickBestVideoFormat(new Size(640, 480), 15);
                // Se obtienen las propiedades del dispositivo de video
                SourceProperties sp = _deviceSource.SourcePropertiesSnapshot();

                // Se ajusta el panel de visualizacion de video para que coincida con la resolucion
                panelVideoPreview.Size = new Size(sp.Size.Width, sp.Size.Height);

                // Se configura la resolusion de la salida de video 
                _job.OutputFormat.VideoProfile.Size = new Size(sp.Size.Width, sp.Size.Height);

                // Se inicializa el panel de video para que muestre la imagen previa
                _deviceSource.PreviewWindow = new PreviewWindow(new HandleRef(panelVideoPreview, panelVideoPreview.Handle));

                // Activa el dispositivo para visualizacion
                _job.ActivateSource(_deviceSource);

            }
        }
        /** Metodo para seleccionar los dispositivos de Audio y video de acuerdo a los parametros
         * seleccionados en la lista de dispositivos encontrados o a los parametros guardados en 
         * el archivo de texto camaras.txt
         */ 
        private void GetSelectedVideoAndAudioDevices(out EncoderDevice video, out EncoderDevice audio, string[] datos)
        {
            video = null;
            audio = null;

            string camara = "";
            string microfono = "";
            if (datos != null)
            {
                camara = datos[1];
                microfono = datos[2];
            }
            else {
                camara = listaVideo.SelectedItem.ToString();
                microfono = listaAudio.SelectedItem.ToString();
            }


            // Get the selected video device            
            foreach (EncoderDevice edv in EncoderDevices.FindDevices(EncoderDeviceType.Video))
            {
                if (String.Compare(edv.Name,camara) == 0)
                {
                    video = edv;
                    break;
                }
            }

            // Get the selected audio device            
            foreach (EncoderDevice eda in EncoderDevices.FindDevices(EncoderDeviceType.Audio))
            {
                if (String.Compare(eda.Name, microfono) == 0)
                {
                    audio = eda;
                    break;
                }
            }
        }
        /** Metodo que controla la grabacion de los segmentos de video de acuerdo al tiempo programado en la 
         * tarea programada*/
        private void grabacion(object sender, EventArgs e)
        {
            Grabar();
            Preview();
            Grabar();
        }
    }
}
