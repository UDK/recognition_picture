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
        public Form1()
        {
            InitializeComponent();
            //FilterInfoCollection videodevices;
            //VideoCaptureDevice videoSource;
            //videodevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            //videoSource = new VideoCaptureDevice(videodevices[0].MonikerString);
            //videoSourcePlayer.VideoSource = videoSource;
            //videoSource.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
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
        }
    }
}
