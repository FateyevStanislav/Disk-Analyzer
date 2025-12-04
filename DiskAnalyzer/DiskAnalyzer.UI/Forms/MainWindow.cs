using System.Windows.Forms;
using System.Drawing;
using System;

namespace DiskAnalyzer.UI
{
    partial class MainWindow : Form
    {
        private Dictionary<string, Dictionary<string, string>> filters;
        public MainWindow()
        {
            InitializeComponent();
            SetupCustomControls();
            Load += MainWindow_Load;
            filterListBox.ItemCheck += FilterListBox_ItemCheck;
        }

        private void SetupCustomControls()
        {
            this.Controls.AddRange(new Control[] { });
        }

        private async void MainWindow_Load(object sender, EventArgs e)
        {
            await LoadFiltersAsync();
        }

        private async Task LoadFiltersAsync()
        {
            filters = await apiClient.GetAvailableFiltersAsync();
            filterListBox.Items.Clear();
            foreach (var filterName in filters.Keys)
            {
                filterListBox.Items.Add(filterName);
            }
        }
    }
}
