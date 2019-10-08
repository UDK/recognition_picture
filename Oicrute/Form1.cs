using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Vision;

namespace Oicrute
{
    public partial class Form1 : Form
    {
        VideoCaptureDevice videoSource;
        public Form1()
        {
            InitializeComponent();
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            FilterInfoCollection videodevices;
            videodevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            videoSource = new VideoCaptureDevice(videodevices[0].MonikerString);
            videoSource.NewFrame += VideoSource_NewFrame;
            videoSource.Start();
        }

        private void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            if (pictureBox1.Image != null)
                pictureBox1.Image.Dispose();
            pictureBox1.Image = (Image)eventArgs.Frame.Clone();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            videoSource.Stop();
            videoSource.NewFrame += VideoSource_NewFrame;
            OpenFileDialog pathImage = new OpenFileDialog();
            if (pathImage.ShowDialog() == DialogResult.OK)
            {
                var filePath = pathImage.FileName;
                var core = new ComparePicture(Oicrute.Consts.pictureCompare);
                if (core.CompareImage(new Bitmap(filePath)))
                    MessageBox.Show("Изображения совпадают");
                else
                    MessageBox.Show("Изображения не совпадают");
            }
            videoSource.Stop();
            videoSource.NewFrame += VideoSource_NewFrame;
        }
    }
}
