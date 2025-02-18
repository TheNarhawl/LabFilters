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
    public partial class LabFiltersForm : Form
    {

        Bitmap image;
        private Stack<Bitmap> undoStack = new Stack<Bitmap>();
        private Stack<Bitmap> redoStack = new Stack<Bitmap>();
        private int maxHistorySize = 10;

        public LabFiltersForm()
        {
            InitializeComponent();
            this.KeyPreview = true;

            HotkeysManager hotkeysManager = new HotkeysManager(undoStack, redoStack, undoAction, redoAction);
        }


        private void saveInHistory()
        {
            if (image != null)
            {
                
                if (undoStack.Count >= maxHistorySize)
                {
                    undoStack.Pop();
                }

                undoStack.Push((Bitmap)image.Clone());

                redoStack.Clear();
            }
        }

        private void undoAction()
        {
            if (undoStack.Count > 0)
            {
                redoStack.Push((Bitmap)image.Clone());

                image = undoStack.Pop();
                pictureBox.Image = image;
                pictureBox.Refresh();
            }
            else
            {
                MessageBox.Show("Нет действий для отмены.", "Отмена");
            }
        }

        private void redoAction()
        {
            if (redoStack.Count > 0)
            {
                undoStack.Push((Bitmap)image.Clone());

                image = redoStack.Pop();
                pictureBox.Image = image;
                pictureBox.Refresh();
            }
            else
            {
                MessageBox.Show("Нет действий для возврата.", "Возврат");
            }
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
                saveInHistory();

                progressBar.Visible = true;
                button1.Visible = true;
                executeLabel.Visible = true;

                executeLabel.BringToFront();
            });

            Bitmap newImage = ((Filters)e.Argument).processImage(imageCopy, backgroundWorker);

            if (backgroundWorker.CancellationPending != true)
                    image = newImage;
            else
                e.Cancel = true;

        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                pictureBox.Image = image;
                pictureBox.Refresh();

            }

            progressBar.Visible = false;
            button1.Visible = false;
            executeLabel.Visible = false;

            progressBar.Value = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            undoStack.Pop();
            backgroundWorker.CancelAsync();
        }
        private void инверсияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            executeLabel.Text = "Идёт выполнение: Инверсия";

            InvertFilter filter = new InvertFilter();
            backgroundWorker.RunWorkerAsync(filter);

        }

        private void размытиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            executeLabel.Text = "Идёт выполнение: Размытие";

            Filters filter = new BlurFilter();
            backgroundWorker.RunWorkerAsync(filter);
        }

        private void размытиеПоГауссуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            executeLabel.Text = "Идёт выполнение: Размытие по Гауссу";

            Filters filter = new GaussianFilter();
            backgroundWorker.RunWorkerAsync(filter);
        }

        private void чернобелыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            executeLabel.Text = "Идёт выполнение: Черно-белый";

            Filters filter = new BlackAndWhiteFilter();
            backgroundWorker.RunWorkerAsync(filter);
        }

        private void сепияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            executeLabel.Text = "Идёт выполнение: Сепия";

            Filters filter = new SepiaFilter();
            backgroundWorker.RunWorkerAsync(filter);
        }

        private void увеличитьЯркостьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            executeLabel.Text = "Идёт выполнение: Увеличить яркость";

            Filters filter = new IncreaseBrightness();
            backgroundWorker.RunWorkerAsync(filter);
        }

        private void увеличитьРезкостьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            executeLabel.Text = "Идёт выполнение: Увеличить резкость";

            Filters filter = new SharpenFilter();
            backgroundWorker.RunWorkerAsync(filter);
        }

        private void алгоритмСобеляToolStripMenuItem_Click(object sender, EventArgs e)
        {
            executeLabel.Text = "Идёт выполнение: Алгоритм Собеля";

            Filters filter = new SobelFilter();
            backgroundWorker.RunWorkerAsync(filter);
        }

        private void шагНазадlStripMenuItem_Click(object sender, EventArgs e)
        {
            undoAction();
        }

        private void шагВпередToolStripMenuItem_Click(object sender, EventArgs e)
        {
            redoAction();
        }

        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.HistoryValue = maxHistorySize;

            if (settingsForm.ShowDialog() == DialogResult.OK)
            {
                maxHistorySize = settingsForm.HistoryValue;
            }
        }
    }
}
