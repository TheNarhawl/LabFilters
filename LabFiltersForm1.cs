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
                    MessageBox.Show("что-то пошло не так..." + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "PNG Image|*.png|JPEG Image|*.jpg;*.jpeg|Bitmap Image|*.bmp";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Bitmap bitmap = new Bitmap(pictureBox.Image);

                    bitmap.Save(saveFileDialog.FileName);

                    MessageBox.Show("Изображение успешно сохранено!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Произошла ошибка при сохранении: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Bitmap imageCopy;
            lock (image)
                imageCopy = new Bitmap(image);



            this.Invoke((MethodInvoker)delegate {
                progressBar1.Visible = true;
                button1.Visible = true;
                executeLabel.Visible = true;

                executeLabel.BringToFront();
            });

            Bitmap newImage = ((Filters)e.Argument).processImage(imageCopy, backgroundWorker1);

            if (backgroundWorker1.CancellationPending != true)
                image = newImage;
            else
                e.Cancel = true;

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

            progressBar1.Visible = false;
            button1.Visible = false;
            executeLabel.Visible = false;

            progressBar1.Value = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }
        private void инверсияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            executeLabel.Text = "Идёт выполнение: Инверсия";

            InvertFilter filter = new InvertFilter();
            backgroundWorker1.RunWorkerAsync(filter);

        }

        private void размытиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            executeLabel.Text = "Идёт выполнение: Размытие";

            Filters filter = new BlurFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void размытиеПоГауссуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            executeLabel.Text = "Идёт выполнение: Размытие по Гауссу";

            Filters filter = new GaussianFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void чернобелыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            executeLabel.Text = "Идёт выполнение: Черно-белый";

            Filters filter = new BlackAndWhiteFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void сепияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            executeLabel.Text = "Идёт выполнение: Сепия";

            Filters filter = new SepiaFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void увеличитьЯркостьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            executeLabel.Text = "Идёт выполнение: Увеличить яркость";

            Filters filter = new IncreaseBrightness();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void увеличитьРезкостьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            executeLabel.Text = "Идёт выполнение: Увеличить резкость";

            Filters filter = new SharpenFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void алгоритмСобеляToolStripMenuItem_Click(object sender, EventArgs e)
        {
            executeLabel.Text = "Идёт выполнение: Алгоритм Собеля";

            Filters filter = new SobelFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

    }
}
