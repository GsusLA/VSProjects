using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.Expression.Encoder.Devices;
using Microsoft.Expression.Encoder.Live;
using Microsoft.Expression.Encoder;
using System.Runtime.InteropServices;
using System.IO;
using System.Net;
using System.Threading;

namespace Camaras
{
    public partial class Form1 : Form
    {
        private LiveJob _job1, _job2;
        Camaras cam;
        dataTablet datos;
        Alarma alarm;
        ManagedWifi wifi;

        string[] nVideos = new string[2];
        string[] nombres = new string[6];

        string lat;
        string longt;
        

        /// <summary>
        /// Device for live source
        /// </summary>
        private LiveDeviceSource _deviceSource, _deviceSource2;

        System.Windows.Forms.Timer Tarea;
        System.Windows.Forms.Timer Tarea2;
        System.Windows.Forms.Timer Tarea3;
        

        private bool _bStartedRecording = false;
        public Form1()
        {
            InitializeComponent();
            cam = new Camaras();
            datos = new dataTablet();
            
            Tarea = new System.Windows.Forms.Timer();
            Tarea.Interval = 1000 * 15;
            Tarea.Tick += new EventHandler(this.OnTimerTick);

            Tarea2 = new System.Windows.Forms.Timer();
            Tarea2.Interval = 1000 * 60 * 60;
            Tarea2.Tick += new EventHandler(this.grabacion);

            Tarea3 = new System.Windows.Forms.Timer();
            Tarea3.Interval = 1000 * 60;
            Tarea3.Tick += new EventHandler(this.enviarVideos);


        }
        Boolean w = true;
        private void enviarVideos(object sender, EventArgs e) {
            wifi = new ManagedWifi();
            if (wifi.scanWifi())
            {
                if (w)
                {
                    try
                    {
                        System.Diagnostics.Process.Start("mover.bat");
                        w = false;
                    }
                    catch (Exception)
                    {


                    }
                }
            }
            else {
                w = true;
            }
        }
        Boolean f = true;
        private void OnTimerTick(object sender, EventArgs e)
        {
           
            
            alarm = new Alarma();
            if (alarm.revEstado())
            {
                if (f)
                {
                    //MessageBox.Show("Vehiculo Alarmado\nLatitud: " + alarm.getLatitud().ToString() + "\nLongitud:  " + alarm.getLongitud().ToString(), "Alarma", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    try
                    {
                        /*string[] laa = alarm.getLatitud().ToString().Split(',');
                        string[] loo = alarm.getLongitud().ToString().Split(',');
                        lat = laa[0] + "." + laa[1];
                        longt = loo[0] + "." + loo[1];*/
                        lat = alarm.getLatitud().ToString();
                        longt = alarm.getLongitud().ToString();
                    }
                    catch (Exception err)
                    {
                        //MessageBox.Show("Error de ejecución \n"+err.ToString(), "Alarma", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        
                        throw;
                    }
                    
                    //MessageBox.Show("Vehiculo Alarmado\nLatitud: "+lat+"\nLongitud:  "+longt, "Alarma", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    
                    time();
                }
                f = false;
            }
            else {
                f = true;
            }
            
           
        }
        private void grabacion(object sender, EventArgs e)
        {
            bttGrabar.PerformClick();
            confInicial();
            bttGrabar.PerformClick();
        }
        async void time() {
            Tarea2.Enabled = false;
            await Task.Delay(2000);
            if (bttGrabar.Text == "Detener") bttGrabar.PerformClick();
            await Task.Delay(1000);
            confInicial();
            await Task.Delay(1000);
            bttGrabar.PerformClick();//Inicia 1er video 
            await Task.Delay(500);
            nombres[0] = nVideos[0];
            nombres[1] = nVideos[1];
            await Task.Delay(1000 * 25);
            bttGrabar.PerformClick();//termina 1er video
            //subirPac(1);
            await Task.Delay(1000);
            confInicial();
            bttGrabar.PerformClick();//Inicia 2o video 
            await Task.Delay(500);
            nombres[2] = nVideos[0];
            nombres[3] = nVideos[1];
            await Task.Delay(1000 * 25);
            bttGrabar.PerformClick();//termina 2o video
            await Task.Delay(1000);
            confInicial();
            bttGrabar.PerformClick();//Inicia 3er video 
            await Task.Delay(500);
            nombres[4] = nVideos[0];
            nombres[5] = nVideos[1];
            await Task.Delay(1000 * 25);
            bttGrabar.PerformClick();//termina 3er video
            await Task.Delay(1000);
            confInicial();
            guardar(nombres);
            Thread subir = new Thread(new ThreadStart( subirVideos));
            subir.Start();
            bttGrabar.PerformClick();//Reiniciamos actividad normal de grabacion
            Tarea2.Enabled = true;




            //MessageBox.Show("Vehiculo Alarmado", "Alarma", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);


        }
        public void subirPac(int  n){
            try
            {
                for (int i = 0; i < nombres.Length; i++)
			    {
                    WebClient client = new WebClient();
                    string myFile = @"D:\VIDEO\" + nombres[i];
                    //string myFile = @"camaras.txt";
                    client.Credentials = CredentialCache.DefaultCredentials;
                    client.UploadFile(@"http://www.pagocel.club/Patrullas/subir.php?id="+nombres[i], "POST", myFile);
                    client.Dispose();
                }
             }
            catch (Exception err)
            {
                MessageBox.Show("" + err.ToString());
            }
        }
        public void subirVideos() {
            try
            {
                for (int i = 0; i < nombres.Length; i++)
			    {
                    WebClient client = new WebClient();
                    string myFile = @"D:\VIDEO\" + nombres[i];
                    //string myFile = @"camaras.txt";
                    client.Credentials = CredentialCache.DefaultCredentials;
                    client.UploadFile(@"http://www.pagocel.club/Patrullas/subir.php?id="+nombres[i], "POST", myFile);
                    
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

            



            //MessageBox.Show("Alarma registrada, Videos y Correo Enviados Correctamente");
        }
        public void guardar(string[] data)
        {
            StreamWriter datos = File.CreateText("nombres.txt");
            datos.WriteLine(data[0]);
            datos.WriteLine(data[1]);
            datos.WriteLine(data[2]);
            datos.WriteLine(data[3]);
            datos.WriteLine(data[4]);
            datos.WriteLine(data[5]);
            datos.Flush();
            datos.Close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            if (!datos.confData())
            {
            
                foreach (EncoderDevice edv in EncoderDevices.FindDevices(EncoderDeviceType.Video))
                {
                    cbVideoCam1.Items.Add(edv.Name);
                    cbVideoCam2.Items.Add(edv.Name);
                }
                foreach (EncoderDevice eda in EncoderDevices.FindDevices(EncoderDeviceType.Audio))
                {
                    cbAudioCam1.Items.Add(eda.Name);
                    cbAudioCam2.Items.Add(eda.Name);
                }
            }
           else {
               confInicial();
               //time();  
               bttGrabar.PerformClick();
           }
            Tarea3.Enabled = true;
            Tarea2.Enabled = true;
            Tarea.Enabled = true;  

        }
        private void bttGrabar_Click(object sender, EventArgs e)
        {
            try
            {
                // Is it Recoring ?
                if (_bStartedRecording)
                {
                    // Yes
                    // Stops encoding
                    _job1.StopEncoding();
                    _job2.StopEncoding();
                    bttGrabar.Text = "Grabar";
                    _bStartedRecording = false;
                }
                else
                {
                    // Sets up publishing format for file archival type
                    FileArchivePublishFormat fileOut1 = new FileArchivePublishFormat();
                    FileArchivePublishFormat fileOut2 = new FileArchivePublishFormat();
                    // Sets file path and name
                    nVideos[0] = String.Format("Cam_1_{0:yyyyMMdd_hhmmss}.wmv", DateTime.Now);
                    nVideos[1] = String.Format("Cam_2_{0:yyyyMMdd_hhmmss}.wmv", DateTime.Now);
                    fileOut1.OutputFileName = "D:\\VIDEO\\" + nVideos[0];
                    fileOut2.OutputFileName = "D:\\VIDEO\\" + nVideos[1];

                    // Adds the format to the job. You can add additional formats as well such as
                    // Publishing streams or broadcasting from a port
                    _job1.PublishFormats.Add(fileOut1);
                    _job2.PublishFormats.Add(fileOut2);
                    // Start encoding
                    _job1.StartEncoding();
                    _job2.StartEncoding();

                    bttGrabar.Text = "Detener";
                    _bStartedRecording = true;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(""+ err.ToString() ,"Mensaje");
            }
        }
        private void confInicial() {
            //MessageBox.Show(datos.recCamara1()+" - "+ datos.recAudio1(), "Mensaje");
            EncoderDevice video1 = null;
            EncoderDevice audio1 = null;
            EncoderDevice video2 = null;
            EncoderDevice audio2 = null;

            try
            {
                string[] device1 = new string[2];
                device1[0] = datos.recCamara1();
                device1[1] = datos.recAudio1();
                string[] device2 = new string[2];
                device2[0] = datos.recCamara2();
                device2[1] = datos.recAudio2();
                cam.getVideoAudio(out video1, out audio1, device1);
                cam.getVideoAudio(out video2, out audio2, device2);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.ToString(), "ERROR");
                return;
            }

            StopJob();

            if (video2 == null && video1 == null)
            {
                return;
            }

            // Starts new job for preview window
            _job1 = new LiveJob();
            _job2 = new LiveJob();
            
            if (video2 != null && audio2 != null && video1 != null && audio1 != null) {
                //MessageBox.Show("ok", "track");
                _deviceSource = _job1.AddDeviceSource(video1, audio1);
                _deviceSource2 = _job2.AddDeviceSource(video2, audio2);
                _deviceSource.PickBestVideoFormat(new Size(640, 480), 15);
                _deviceSource2.PickBestVideoFormat(new Size(640, 480), 15);
                SourceProperties sp1 = _deviceSource.SourcePropertiesSnapshot();
                SourceProperties sp2 = _deviceSource2.SourcePropertiesSnapshot();
                videoPanel1.Size = new Size(sp1.Size.Width, sp1.Size.Height);
                videoPanel2.Size = new Size(sp2.Size.Width, sp2.Size.Height);
                _job1.OutputFormat.VideoProfile.Size = new Size(sp1.Size.Width, sp1.Size.Height);
                _job2.OutputFormat.VideoProfile.Size = new Size(sp2.Size.Width, sp2.Size.Height);
                _deviceSource.PreviewWindow = new PreviewWindow(new HandleRef(videoPanel1, videoPanel1.Handle));
                _deviceSource2.PreviewWindow = new PreviewWindow(new HandleRef(videoPanel2, videoPanel2.Handle));
                _job1.ActivateSource(_deviceSource);
                _job2.ActivateSource(_deviceSource2);
                //MessageBox.Show("ok", "track" + DateTime.Now.ToString());

                //bttGrabar.PerformClick();
                //Tarea.Enabled = true;
                //time();
            }
            else
            {
                // Gives error message as no audio and/or video devices found
                MessageBox.Show("No Video/Audio capture devices have been found.", "Warning");
                //toolStripStatusLabel1.Text = "No Video/Audio capture devices have been found.";
            }




        }
        private void prevCam2_Click(object sender, EventArgs e)
        {
            EncoderDevice video2 = null;
            EncoderDevice audio2 = null;
            string[] device = new string[2];

            //Console.Write(cbVideoCam2.SelectedItem.ToString() + " - " + cbAudioCam2.SelectedItem.ToString());
            //Console.ReadLine();

            if (cbVideoCam2.SelectedIndex >=0  && cbAudioCam2.SelectedIndex >=0)
            {
                device[0] = cbVideoCam2.SelectedItem.ToString();
                device[1] = cbAudioCam2.SelectedItem.ToString();
            }
            else
            {
                MessageBox.Show("Asegurese de haber elegido los dispositivos de Audio y Video", "ERROR");
                return;
            }

            cam.getVideoAudio(out video2, out audio2, device);
            StopJob();

            if (video2 == null)
            {
                return;
            }

            // Starts new job for preview window
            _job2 = new LiveJob();

            // Checks for a/v devices
            if (video2 != null && audio2 != null)
            {
                // Create a new device source. We use the first audio and video devices on the system
                _deviceSource2 = _job2.AddDeviceSource(video2, audio2);

                // Is it required to show the configuration dialogs ?

                // No
                // Setup the video resolution and frame rate of the video device
                // NOTE: Of course, the resolution and frame rate you specify must be supported by the device!
                // NOTE2: May be not all video devices support this call, and so it just doesn't work, as if you don't call it (no error is raised)
                // NOTE3: As a workaround, if the .PickBestVideoFormat method doesn't work, you could force the resolution in the 
                //        following instructions (called few lines belows): 'panelVideoPreview.Size=' and '_job.OutputFormat.VideoProfile.Size=' 
                //        to be the one you choosed (640, 480).
                _deviceSource2.PickBestVideoFormat(new Size(640, 480), 15);


                // Get the properties of the device video
                SourceProperties sp = _deviceSource2.SourcePropertiesSnapshot();

                // Resize the preview panel to match the video device resolution set
                videoPanel2.Size = new Size(sp.Size.Width, sp.Size.Height);

                // Setup the output video resolution file as the preview
                _job2.OutputFormat.VideoProfile.Size = new Size(sp.Size.Width, sp.Size.Height);

                // Display the video device properties set
                //toolStripStatusLabel1.Text = sp.Size.Width.ToString() + "x" + sp.Size.Height.ToString() + "  " + sp.FrameRate.ToString() + " fps";

                // Sets preview window to winform panel hosted by xaml window
                _deviceSource2.PreviewWindow = new PreviewWindow(new HandleRef(videoPanel2, videoPanel2.Handle));

                // Make this source the active one
                _job2.ActivateSource(_deviceSource2);

            }
            else
            {
                // Gives error message as no audio and/or video devices found
                MessageBox.Show("No Video/Audio capture devices have been found.", "Warning");
                //toolStripStatusLabel1.Text = "No Video/Audio capture devices have been found.";
            }
        }
        private void prevCam1_Click(object sender, EventArgs e)
        {
            EncoderDevice video = null;
            EncoderDevice audio = null;
            string[] device = new string[2];
            if (cbVideoCam1.SelectedIndex >= 0 && cbAudioCam1.SelectedIndex >= 0)
            {
                device[0] = cbVideoCam1.SelectedItem.ToString();
                device[1] = cbAudioCam1.SelectedItem.ToString();
            }
            else {
                MessageBox.Show("Asegurese de haber elegido los dispositivos de Audio y Video" , "ERROR");
                return;
            }

            cam.getVideoAudio(out video, out audio, device);
            StopJob();

            if (video == null)
            {
                return;
            }

            // Starts new job for preview window
            _job1 = new LiveJob();

            // Checks for a/v devices
            if (video != null && audio != null)
            {
                // Create a new device source. We use the first audio and video devices on the system
                _deviceSource = _job1.AddDeviceSource(video, audio);

                // Is it required to show the configuration dialogs ?
                
                    // No
                    // Setup the video resolution and frame rate of the video device
                    // NOTE: Of course, the resolution and frame rate you specify must be supported by the device!
                    // NOTE2: May be not all video devices support this call, and so it just doesn't work, as if you don't call it (no error is raised)
                    // NOTE3: As a workaround, if the .PickBestVideoFormat method doesn't work, you could force the resolution in the 
                    //        following instructions (called few lines belows): 'panelVideoPreview.Size=' and '_job.OutputFormat.VideoProfile.Size=' 
                    //        to be the one you choosed (640, 480).
                    _deviceSource.PickBestVideoFormat(new Size(640, 480), 15);
                

                // Get the properties of the device video
                SourceProperties sp = _deviceSource.SourcePropertiesSnapshot();

                // Resize the preview panel to match the video device resolution set
                videoPanel1.Size = new Size(sp.Size.Width, sp.Size.Height);

                // Setup the output video resolution file as the preview
                _job1.OutputFormat.VideoProfile.Size = new Size(sp.Size.Width, sp.Size.Height);

                // Display the video device properties set
                //toolStripStatusLabel1.Text = sp.Size.Width.ToString() + "x" + sp.Size.Height.ToString() + "  " + sp.FrameRate.ToString() + " fps";

                // Sets preview window to winform panel hosted by xaml window
                _deviceSource.PreviewWindow = new PreviewWindow(new HandleRef(videoPanel1, videoPanel1.Handle));

                // Make this source the active one
                _job1.ActivateSource(_deviceSource);

            }
            else
            {
                // Gives error message as no audio and/or video devices found
                MessageBox.Show("No Video/Audio capture devices have been found.", "Warning");
                //toolStripStatusLabel1.Text = "No Video/Audio capture devices have been found.";
            }
        }
        void StopJob()
        {
            // Has the Job already been created ?
            if (_job1 != null && _job2 != null)
            {
                // Yes
                // Is it capturing ?
                //if (_job.IsCapturing)
                /*if (_bStartedRecording)
                {
                    // Yes
                    // Stop Capturing
                    btnStartStopRecording.PerformClick();
                }
                */
                _job1.StopEncoding();
                _job2.StopEncoding();

                // Remove the Device Source and destroy the job
                _job1.RemoveDeviceSource(_deviceSource);
                _job2.RemoveDeviceSource(_deviceSource2);

                // Destroy the device source
                _deviceSource.PreviewWindow = null;
                _deviceSource = null;
                _deviceSource2.PreviewWindow = null;
                _deviceSource2 = null;
            }
        }
        private void recordarCam1_Click(object sender, EventArgs e)
        {
            string[] datos = new string[5];
            datos[0] = "TRUE";
            datos[1] = cbVideoCam1.SelectedItem.ToString();
            datos[2] = cbAudioCam1.SelectedItem.ToString();
            datos[3] = cbVideoCam2.SelectedItem.ToString();
            datos[4] = cbAudioCam2.SelectedItem.ToString();
            cam.guardar(datos);
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopJob();
        }
    }
}
