using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DiskAnalyzer.UI
{
    partial class MainWindow : Form
    {
        private Dictionary<string, Dictionary<string, string>> filters;
        public MainWindow()
        {
            InitializeComponent();

            if (!IsInDesigner())
            {
                SetupCustomControls();
                Load += MainWindow_Load;
                filterListBox.ItemCheck += FilterListBox_ItemCheck;
            }
        }

        private bool IsInDesigner()
        {
            return LicenseManager.UsageMode == LicenseUsageMode.Designtime;
        }


        private void SetupCustomControls()
        {
            this.Controls.AddRange(new Control[] { });
        }

        private async void MainWindow_Load(object sender, EventArgs e)
        {
            if (!IsInDesigner())
                await LoadFiltersAsync();
        }


        private async Task LoadFiltersAsync()
        {
            if (apiClient == null) return;

            filters = await apiClient.GetAvailableFiltersAsync();
            filterListBox.Items.Clear();
            foreach (var filterName in filters.Keys)
            {
                filterListBox.Items.Add(filterName);
            }
        }
    }
}
