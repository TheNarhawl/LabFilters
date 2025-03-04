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
            this.KeyDown += new KeyEventHandler(hotkeysManager.HandleHotkey);
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
            if (image == null || pictureBox == null)
            {
                MessageBox.Show("Пожалуйста, загрузите изображение перед применением фильтра.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
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

            Bitmap newImage = imageCopy;

            if (e.Argument is Filters)
            {
                Filters filter = (Filters)e.Argument;
                newImage = filter.processImage(imageCopy, backgroundWorker);
            }
            else if (e.Argument is List<Filters> filters)
            {
                foreach (var filter in filters)
                {
                    if (backgroundWorker.CancellationPending)
                        break;

                    newImage = filter.processImage(newImage, backgroundWorker);

                    this.Invoke((MethodInvoker)delegate {
                        image = new Bitmap(newImage);
                        pictureBox.Image = image;
                        pictureBox.Refresh();
                    });
                }
            }
            else if (e.Argument is object[] topHatArgs && topHatArgs.Length == 2)
            {
                Bitmap originalImage = topHatArgs[0] as Bitmap;
                List<Filters> topHatFilters = topHatArgs[1] as List<Filters>;

                if (originalImage == null || topHatFilters == null)
                {
                    MessageBox.Show("Ошибка: неверные аргументы для Top hat.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                foreach (var filter in topHatFilters)
                {
                    if (backgroundWorker.CancellationPending)
                        break;

                    newImage = filter.processImage(newImage, backgroundWorker);

                    this.Invoke((MethodInvoker)delegate {
                        image = new Bitmap(newImage);
                        pictureBox.Image = image;
                        pictureBox.Refresh();
                    });
                }

                TopHatFilter topHatFilter = new TopHatFilter(originalImage, newImage);
                newImage = topHatFilter.processImage(originalImage, backgroundWorker);

                this.Invoke((MethodInvoker)delegate {
                    image = new Bitmap(newImage);
                    pictureBox.Image = image;
                    pictureBox.Refresh();
                });
            }


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

        //private void ApplyFiltersSequentially(List<Filters> filters)
        //{
        //    Bitmap currentImage = new Bitmap(image);

        //    foreach (var filter in filters)
        //    {
        //        if (backgroundWorker.CancellationPending)
        //            break;

        //        currentImage = filter.processImage(currentImage, backgroundWorker);
        //        this.Invoke((MethodInvoker)delegate {
        //            image = new Bitmap(currentImage);
        //            pictureBox.Image = image;
        //            pictureBox.Refresh();
        //        });
        //    }
        //}

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
        private void размытиеВДвиженииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            executeLabel.Text = "Идёт выполнение: Размытие в движении";

            Filters filter = new MotionBlurFilter(5);
            backgroundWorker.RunWorkerAsync(filter);
        }
        private void эффектстеклаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            executeLabel.Text = "Идёт волнение: Эффект \"стекла\"";

            Filters filter = new GlassFilter();
            backgroundWorker.RunWorkerAsync(filter);
        }
        private void тиснениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            executeLabel.Text = "Идёт волнение: Тиснение";

            Filters filter = new EmbossFilter();
            backgroundWorker.RunWorkerAsync(filter);
        }
        private void усилитьКонтурыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            executeLabel.Text = "Идёт волнение: Усилить контуры";

            Filters filter = new MaximumFilter(1);
            backgroundWorker.RunWorkerAsync(filter);
        }
        private void светящиесяКраяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            executeLabel.Text = "Идёт волнение: Светящиеся края";

            List<Filters> filters = new List<Filters>
            {
                new MedianFilter(2),
                new SobelFilter(),
                new MaximumFilter(1)
            };

            backgroundWorker.RunWorkerAsync(filters);
        }
        private void повернутьНа90ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            executeLabel.Text = "Идёт выполнение: 90 градусов по ч.с.";
            Filters filter = new RotateImage(90.0);
            backgroundWorker.RunWorkerAsync(filter);
        }
        private void повернутьНа90ГрадусовПротивЧсToolStripMenuItem_Click(object sender, EventArgs e)
        {
            executeLabel.Text = "Идёт выполнение: 90 градусов против ч.с.";
            Filters filter = new RotateImage(-90.0);
            backgroundWorker.RunWorkerAsync(filter);
        }
        private void волны1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            executeLabel.Text = "Идёт выполнение: Волны 1";
            Filters filter = new WaveFilterOne();
            backgroundWorker.RunWorkerAsync(filter);
        }

        private void волны2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            executeLabel.Text = "Идёт выполнение: Волны 2";
            Filters filter = new WaveFilterTwo();
            backgroundWorker.RunWorkerAsync(filter);
        }
        private void линейноеРастяжениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            executeLabel.Text = "Идёт выполнение: Линейное растяжение";
            Filters filter = new LinearStretching();
            backgroundWorker.RunWorkerAsync(filter);
        }
        private void операторToolStripMenuItem_Click(object sender, EventArgs e)
        {
            executeLabel.Text = "Идёт выполнение: Оператор Прюитта";
            Filters filter = new PruittFilter();
            backgroundWorker.RunWorkerAsync(filter);
        }
        private void операторЩалляToolStripMenuItem_Click(object sender, EventArgs e)
        {
            executeLabel.Text = "Идёт выполнение: Оператор Щарра";
            Filters filter = new ScharrFilter();
            backgroundWorker.RunWorkerAsync(filter);
        }
        private void идеальныйОтражательToolStripMenuItem_Click(object sender, EventArgs e)
        {
            executeLabel.Text = "Идёт выполнение: Идеальный отражатель";
            Filters filter = new PerfectReflector();
            backgroundWorker.RunWorkerAsync(filter);
        }
        private void смещениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            executeLabel.Text = "Идёт выполнение: Смещение";
            Filters filter = new OffsetFilter();
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

        private void другоеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OtherEffectSelectionForm otherForm = new OtherEffectSelectionForm();
            if (otherForm.ShowDialog() == DialogResult.OK)
            {
                string selectedEffect = otherForm.SelectedEffect;
                float[,] kernel = otherForm.Kernel;

                Filters filter = null;

                switch (selectedEffect)
                {
                    case "Dilation":
                        executeLabel.Text = "Идёт выполнение: Dilation";
                        filter = new DilationFilter(kernel);
                        backgroundWorker.RunWorkerAsync(filter);
                        break;
                    case "Erosion":
                        executeLabel.Text = "Идёт выполнение: Erosion";
                        filter = new ErosionFilter(kernel);
                        backgroundWorker.RunWorkerAsync(filter);
                        break;
                    case "Opening":
                        executeLabel.Text = "Идёт выполнение: Opening";
                        List<Filters> openingFilters = new List<Filters>
                        {
                            new ErosionFilter(kernel),
                            new DilationFilter(kernel),
                        };

                        backgroundWorker.RunWorkerAsync(openingFilters);
                        break;
                    case "Closing":
                        executeLabel.Text = "Идёт выполнение: Closing";
                        List<Filters> closingFilters = new List<Filters>
                        {
                            new DilationFilter(kernel),
                            new ErosionFilter(kernel),
                        };

                        backgroundWorker.RunWorkerAsync(closingFilters);
                        break;
                    case "Top hat":
                        executeLabel.Text = "Идёт выполнение: Top hat";
                        List<Filters> topHatFilters = new List<Filters>
                        {
                            new ErosionFilter(kernel),
                            new DilationFilter(kernel),
                        };

                        Bitmap originalImageCopy = new Bitmap(image);

                        backgroundWorker.RunWorkerAsync(new object[] { originalImageCopy, topHatFilters });
                        break;
                    default:
                        MessageBox.Show("Неверный выбор эффекта.");
                        break;
                }
            }

        }

    }
}
