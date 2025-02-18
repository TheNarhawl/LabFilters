using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabFilters
{
    public partial class LabFiltersForm1 : Form
    {

        Bitmap image;
        public LabFiltersForm1()
        {
            InitializeComponent();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image Files | *.png; *.jpg; *bmp; | All Files (*.*) | *.*";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    image = new Bitmap(dialog.FileName);
                    pictureBox.Image = image;
                    pictureBox.Refresh();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("что-то пошло не так...");
                }
            }

        }


        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Bitmap newImage = ((Filters)e.Argument).processImage(image, backgroundWorker1);
            if (backgroundWorker1.CancellationPending != true)
                image = newImage;
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                pictureBox.Image = image;
                pictureBox.Refresh();
            }
            progressBar1.Value = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }
        private void инверсияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InvertFilter filter = new InvertFilter();
            backgroundWorker1.RunWorkerAsync(filter);

        }

        private void размытиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new BlurFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void размытиеПоГауссуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new GaussianFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

    }
}
