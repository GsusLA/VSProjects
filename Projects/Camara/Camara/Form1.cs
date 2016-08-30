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
using System.Threading;
using System.Net;
using System.IO;

namespace Camara
{
    public partial class Form1 : Form
    {
        private LiveJob _job;
        private LiveDeviceSource _deviceSource;
        FileArchivePublishFormat fileOut;
        CamConfig camara;
        Alarma alarma;
        string[] datoCamaras = null;
        string[] nombres = new string[3];
        Boolean f = true;
        double lat , longt;
        string fileName = "";
        System.Windows.Forms.Timer TareaGrabacionNormal;
        System.Windows.Forms.Timer TareaVerificarAlarma;
        System.Windows.Forms.Timer TareaGrabacionAlarma;
        public Form1()
        {
            InitializeComponent();
            
            // Inicializa las clases y metodos necesarios
            
            camara = new CamConfig();
            alarma = new Alarma();
            tareasProgramadas();
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

                    

                    TareaGrabacionNormal.Enabled = true;
                    TareaVerificarAlarma.Enabled = true;
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
                    fileName = String.Format("D:\\VIDEO\\WebCam {0:yyyyMMdd_hhmmss}.wmv", DateTime.Now);
                    fileOut.OutputFileName = fileName;

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
        
        private void tareasProgramadas() {
            /**En este metodo se configuran las tareas programadas que se ejecutaran cada tiempo determinado, estas
             tareas se especificaran de acuerdo a cada acción*/

            //Tarea para iniciar grabacion normal automatica
            TareaGrabacionNormal = new System.Windows.Forms.Timer();
            TareaGrabacionNormal.Interval = 1000 * 60 * 10;
            TareaGrabacionNormal.Tick += new EventHandler(this.grabacion);

            TareaVerificarAlarma = new System.Windows.Forms.Timer();
            TareaVerificarAlarma.Interval = 1000 * 20;
            TareaVerificarAlarma.Tick += new EventHandler(this.verificarAlarma);


        }
        /** En esta region estan los metodos que controlan los pasos necesarios para la grabacion y envio de videos
         * a la pagina de pagocel en caso de que se active la alarma de la aplicacion, estara controlado por la tarea   */
        #region 
        /** Este metodo verifica en la base de datos de Pagocel si una alarma esta activada, en esta version la verificacion se hace 
         * directamentte en la base de datos de pagocel, cuando la verificacion se haga por medio del BlueTooth del stick, los cambios 
         * necesarios para recibir la alarma de la tablet se haran en este metodo. */
        private void verificarAlarma(object sender, EventArgs e)
        {
            if (alarma.revEstado())
            {
                if (f)
                {
                    try
                    {
                        /** string[] laa = alarm.getLatitud().ToString().Split(',');
                        string[] loo = alarm.getLongitud().ToString().Split(',');
                        lat = laa[0] + "." + laa[1];
                        longt = loo[0] + "." + loo[1];
                         */
                        lat = alarma.getLatitud();
                        longt = alarma.getLongitud();
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("Datos: \nLatitud = "+ lat + "longitud = "+longt, "Error de Coordenadas");
                        throw;
                    }
                    MessageBox.Show("Inicia rutina de alarma", "Error de Coordenadas");
                    //rutinaAlarma();
                }
                f = false;
            }
            else
            {
                f = true;
            }

        }
        /** Metodo que hace la grabacion de 3 secciones de video de 30 segundos cada una, una vez generados lo videos
         * se enviaran a Pagocel, donde se notificara la alarma por mail al administrador adjuntando los links de los 
         * videos y la ubicacion de la alarma en google maps, estos datos estaran incluidos en el cuerpo del mail.*/
        async void rutinaAlarma() {
            TareaGrabacionNormal.Enabled = false;
            await Task.Delay(2000);
            Grabar();
            await Task.Delay(1000);
            Preview();
            await Task.Delay(1000);
            Grabar();//Inicia 1er video 
            await Task.Delay(500);
            nombres[0] = fileName;
            await Task.Delay(1000 * 30);
            Grabar();//termina 1er video
            await Task.Delay(1000);
            Preview();
            await Task.Delay(1000);
            Grabar();//Inicia 2o video 
            await Task.Delay(500);
            nombres[1] = fileName;
            await Task.Delay(1000 * 30);
            Grabar();//termina 2o video
            await Task.Delay(1000);
            Preview();
            await Task.Delay(1000);
            Grabar();//Inicia 3er video 
            await Task.Delay(500);
            nombres[2] = fileName;
            await Task.Delay(1000 * 30);
            Grabar();//termina 3er video
            await Task.Delay(1000);
            Preview();
            guardar(nombres);
            Thread subir = new Thread(new ThreadStart(subirVideos));
            subir.Start();
            Grabar();//Reiniciamos actividad normal de grabacion
            TareaGrabacionNormal.Enabled = true;
        }
        // Este metodo sube los videos a Pagocel de acuerdo a la lista generada  
        public void subirVideos()
        {
            try
            {
                for (int i = 0; i < nombres.Length; i++)
                {
                    WebClient client = new WebClient();
                    string myFile = @"D:\VIDEO\" + nombres[i];
                    //string myFile = @"camaras.txt";
                    client.Credentials = CredentialCache.DefaultCredentials;
                    client.UploadFile(@"http://www.pagocel.club/Patrullas/subir.php?id=" + nombres[i], "POST", myFile);

                    client.Dispose();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show("" + err.ToString());
            }

            try
            {

                WebClient client = new WebClient();
                //string myFile = @"C:\VIDEO\video1.wmv";
                string myFile = @"nombres.txt";
                client.Credentials = CredentialCache.DefaultCredentials;
                client.UploadFile(@"http://www.pagocel.club/Patrullas/subir_video.php?lat=" + lat + "&log=" + longt, "POST", myFile);
                //MessageBox.Show("Enviado");
                client.Dispose();
            }
            catch (Exception err)
            {
                MessageBox.Show("" + err.ToString());
            }
        }
        // Este metodo guarda el nombre de los videos en un archivo .txt
        public void guardar(string[] data)
        {
            StreamWriter datos = File.CreateText("nombres.txt");
            datos.WriteLine(data[0]);
            datos.WriteLine(data[1]);
            datos.WriteLine(data[2]);
            datos.Flush();
            datos.Close();
        }
        #endregion

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Grabar();
        }
    }
}
