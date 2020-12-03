using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;

using AForge.Controls;


namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        private FilterInfoCollection VideoCaptureDevices;

        private VideoCaptureDevice FinalVideo = null;
        private VideoCaptureDeviceForm captureDevice;
        private Bitmap video;
        //private Vide
        Server ServerObject = null;
        public Form1()
        {
            InitializeComponent();
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {
            VideoCaptureDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            captureDevice = new VideoCaptureDeviceForm();
            //Process.Start("https://javisystems.keka.com/ui/#/me/attendance/logs");
            ServerObject = new Server(this);
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
            
        }
        public void Addlog(String msg)
        {
            this.Invoke((MethodInvoker)delegate
           {
               textBox1.Text += msg;
           });
            
        }
        public Bitmap  bitmap_img()
        {
            //this.Invoke((MethodInvoker)delegate
            //{
            //    return video;
            //});
            return video;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (captureDevice.ShowDialog(this) == DialogResult.OK)
            {
                FinalVideo = captureDevice.VideoDevice;
                FinalVideo.NewFrame += new NewFrameEventHandler(FinalVideo_NewFrame);
                FinalVideo.Start();
            }
            ServerObject.Start();
        }
        void FinalVideo_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            video = (Bitmap)eventArgs.Frame.Clone();
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(FinalVideo == null)
            {
                //Application.Exit();
                
            }
            if (FinalVideo.IsRunning)
            {
                this.FinalVideo.Stop();
                pictureBox1.Image = null;
            }

            
        }

        
    }
}
