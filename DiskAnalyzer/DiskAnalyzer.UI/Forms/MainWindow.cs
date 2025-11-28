using System.Windows.Forms;
using System.Drawing;
using System;

namespace DiskAnalyzer.UI
{
    partial class MainWindow : Form
    {
        private string inputPath;
        public MainWindow()
        {
            InitializeComponent();

            SetupCustomControls();
        }

        private void SetupCustomControls()
        {
            this.Controls.AddRange(new Control[] { });
        }

        private void MainWindow_Load(object sender, System.EventArgs e)
        {

        }
    }
}
