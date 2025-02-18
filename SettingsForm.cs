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

    public partial class SettingsForm : Form
    {
        public int HistoryValue
        {
            get { return int.Parse(historyValueTextBox.Text); }
            set { historyValueTextBox.Text = value.ToString(); }
        }
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {

            try
            {
                int historyValue = int.Parse(historyValueTextBox.Text);

                if (historyValue < 1 || historyValue > 10)
                {
                    MessageBox.Show("Введите положительное число в диапазоне от 1 до 10", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                HistoryValue = historyValue;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Что-то пошло не так.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
