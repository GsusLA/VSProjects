using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Procesos
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ListViewItem itemAdd;
            listView1.Items.Clear();
            Process[] procesos = Process.GetProcesses();
            foreach (Process proc in procesos)
            {
                itemAdd = listView1.Items.Add(proc.MainWindowTitle);
                itemAdd.SubItems.Add(proc.Id.ToString());
                if (proc.MainWindowTitle.Contains("cmd.exe")) {
                    MessageBox.Show("Si esta el prceso CMD.EXE", "Aviso");
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
           // System.Diagnostics.Process.Start("test.bat"," mundo, cruel");
            // System.Diagnostics.Process.Start("test.bat"," mundo, cruel");
            EjecutarBat("test.bat"," mundo, feliz");
        }
        
        private void EjecutarBat(string pathArchivoBat, string args)
        {
            Process proceso = new Process();
            proceso.StartInfo.FileName = pathArchivoBat;
            proceso.StartInfo.Arguments = args;
            proceso.Start();
            
            proceso.WaitForExit(); //Espera a que termine la ejecución del archivo .bat
            MessageBox.Show("Proceso Terminado", "Aviso");
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string[] pdfFiles = GetFileNames("D:\\VIDEO","*.wmv");
            for (int i = 0; i < pdfFiles.Length;i++ )
            {
                listView1.Items.Add(pdfFiles[i]);
                richTextBox1.Text += pdfFiles[i] + "\n";
                MessageBox.Show(pdfFiles[i], "video");
            }


        }
        private string[] GetFileNames(string path, string filter)
        {
            string[] files = Directory.GetFiles(path, filter);
            for(int i = 0; i < files.Length; i++){
            files[i] = Path.GetFileName(files[i]);
            }
            return files;
        }
    }
}
