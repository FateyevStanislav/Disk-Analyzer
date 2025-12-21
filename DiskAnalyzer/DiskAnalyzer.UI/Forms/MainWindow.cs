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
            this.Resize += MainWindow_Resize;
            if (!IsInDesigner())
            {
                SetupCustomControls();
                Load += MainWindow_Load;
                filterListBox.ItemCheck += FilterListBox_ItemCheck;
            }
        }

        private void MainWindow_Resize(object sender, EventArgs e)
        {
            if (historyDataGrid.Visible)
            {
                historyDataGrid.Width = ClientSize.Width - historyDataGrid.Left - 20;
                historyDataGrid.Height = ClientSize.Height - historyDataGrid.Top -
                                       (historyDetailPanel.Visible ? historyDetailPanel.Height : 20);
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
