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
    public partial class OtherEffectSelectionForm : Form
    {
        public string SelectedEffect { get; private set; }
        public float[,] Kernel { get; private set; }

        public OtherEffectSelectionForm()
        {
            InitializeComponent();
            InitializeEffectList();
            InitializeKernelList();
        }

        private void InitializeEffectList()
        {
            comboBoxEffect.Items.AddRange(new string[]
            {
                "Dilation",
                "Erosion",
                "Opening",
                "Closing",
                "Top hat"
            });
            comboBoxEffect.SelectedIndex = 0;
        }

        private void InitializeKernelList()
        {
            comboBoxKernel.Items.AddRange(new string[]
            {
                "3x3 Крест",
                "3x3 Квадрат",
                "5x5 Крест",
                "5x5 Квадрат",
                "Диагональный крест"
            });
            comboBoxKernel.SelectedIndex = 0;
        }

        private float[,] GetSelectedKernel()
        {
            switch (comboBoxKernel.SelectedItem.ToString())
            {
                case "3x3 Крест":
                    return new float[,] {
                        { 0f, 1f, 0f },
                        { 1f, 1f, 1f },
                        { 0f, 1f, 0f }
                    };
                case "3x3 Квадрат":
                    return new float[,] {
                        { 1f, 1f, 1f },
                        { 1f, 1f, 1f },
                        { 1f, 1f, 1f }
                    };
                case "5x5 Крест":
                    return new float[,] {
                        { 0f, 0f, 1f, 0f, 0f },
                        { 0f, 0f, 1f, 0f, 0f },
                        { 1f, 1f, 1f, 1f, 1f },
                        { 0f, 0f, 1f, 0f, 0f },
                        { 0f, 0f, 1f, 0f, 0f }
                    };
                case "5x5 Квадрат":
                    return new float[,] {
                        { 1f, 1f, 1f, 1f, 1f },
                        { 1f, 1f, 1f, 1f, 1f },
                        { 1f, 1f, 1f, 1f, 1f },
                        { 1f, 1f, 1f, 1f, 1f },
                        { 1f, 1f, 1f, 1f, 1f }
                    };
                case "Диагональный крест":
                    return new float[,] {
                        { 0f, 0f, 1f, 0f, 0f },
                        { 0f, 1f, 1f, 1f, 0f },
                        { 1f, 1f, 1f, 1f, 1f },
                        { 0f, 1f, 1f, 1f, 0f },
                        { 0f, 0f, 1f, 0f, 0f }
                    };
                default:
                    return new float[,] { { 0f, 1f, 0f }, { 1f, 1f, 1f }, { 0f, 1f, 0f } };
            }
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            SelectedEffect = comboBoxEffect.SelectedItem.ToString();
            Kernel = GetSelectedKernel();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
