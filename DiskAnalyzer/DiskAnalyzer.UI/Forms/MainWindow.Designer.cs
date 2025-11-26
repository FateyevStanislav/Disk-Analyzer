using DiskAnalyzer.Api;
using DiskAnalyzer.Api.Controllers;
using DiskAnalyzer.Library.Domain;
using DiskAnalyzer.Library.Domain.Attributes;
using DiskAnalyzer.Library.Domain.Filters;
using DiskAnalyzer.Library.Domain.Metrics;
using DiskAnalyzer.Library.Infrastructure;
using DiskAnalyzer.UI.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Reflection;

namespace DiskAnalyzer.UI
{
    partial class MainWindow
    {
        private System.ComponentModel.IContainer components = null;
        private IConversionService conversionService = new ConversionsHandler();
        private ITypeResolver typeResolver = new TypeResolver();
        private IMetricLoader metricLoader = new MetricLoader();
        private IFilterLoader filterLoader = new FilterLoader();
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pathLabel = new Label();
            pathTextBox = new TextBox();
            analyzeButton = new Button();
            depthLabel = new Label();
            depthUpDown = new NumericUpDown();
            metricsCheckList = new CheckedListBox();
            metricsLabel = new Label();
            filterCheckList = new CheckedListBox();
            filterLabel = new Label();
            historyCheckBox = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)depthUpDown).BeginInit();
            SuspendLayout();
            // 
            // pathLabel
            // 
            pathLabel.AutoSize = true;
            pathLabel.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Point, 204);
            pathLabel.Location = new Point(15, 9);
            pathLabel.Margin = new Padding(4, 0, 4, 0);
            pathLabel.Name = "pathLabel";
            pathLabel.Size = new Size(263, 24);
            pathLabel.TabIndex = 0;
            pathLabel.Text = "Введите путь для оценки";
            // 
            // pathTextBox
            // 
            pathTextBox.BackColor = Color.Gainsboro;
            pathTextBox.BorderStyle = BorderStyle.FixedSingle;
            pathTextBox.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            pathTextBox.Location = new Point(20, 58);
            pathTextBox.Margin = new Padding(4, 3, 4, 3);
            pathTextBox.Name = "pathTextBox";
            pathTextBox.Size = new Size(647, 20);
            pathTextBox.TabIndex = 1;
            // 
            // analyzeButton
            // 
            analyzeButton.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            analyzeButton.Location = new Point(269, 456);
            analyzeButton.Margin = new Padding(4, 3, 4, 3);
            analyzeButton.Name = "analyzeButton";
            analyzeButton.Size = new Size(112, 33);
            analyzeButton.TabIndex = 2;
            analyzeButton.Text = "Оценить";
            analyzeButton.UseVisualStyleBackColor = true;
            analyzeButton.Click += OnAnalyzeButtonClick;
            // 
            // depthLabel
            // 
            depthLabel.AutoSize = true;
            depthLabel.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Point, 204);
            depthLabel.Location = new Point(15, 107);
            depthLabel.Margin = new Padding(4, 0, 4, 0);
            depthLabel.Name = "depthLabel";
            depthLabel.Size = new Size(263, 24);
            depthLabel.TabIndex = 0;
            depthLabel.Text = "Введите глубину анализа";
            // 
            // depthUpDown
            // 
            depthUpDown.BackColor = Color.Gainsboro;
            depthUpDown.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            depthUpDown.Location = new Point(20, 157);
            depthUpDown.Margin = new Padding(4, 3, 4, 3);
            depthUpDown.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            depthUpDown.Name = "depthUpDown";
            depthUpDown.Size = new Size(62, 21);
            depthUpDown.TabIndex = 3;
            depthUpDown.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // metricsCheckList
            // 
            metricsCheckList.FormattingEnabled = true;
            metricsCheckList.Location = new Point(20, 241);
            metricsCheckList.Margin = new Padding(4, 3, 4, 3);
            metricsCheckList.Name = "metricsCheckList";
            metricsCheckList.Size = new Size(280, 94);
            metricsCheckList.TabIndex = 4;

            var metricTypes = metricLoader.GetAvailableMetrics().ToArray();
            metricsCheckList.Items.Clear();
            metricsCheckList.Items.AddRange(metricTypes);
            // 
            // metricsLabel
            // 
            metricsLabel.AutoSize = true;
            metricsLabel.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Point, 204);
            metricsLabel.Location = new Point(59, 202);
            metricsLabel.Name = "metricsLabel";
            metricsLabel.Size = new Size(197, 24);
            metricsLabel.TabIndex = 5;
            metricsLabel.Text = "Выберите метрики";
            // 
            // filterCheckList
            // 
            filterCheckList.FormattingEnabled = true;
            filterCheckList.Location = new Point(356, 241);
            filterCheckList.Name = "filterCheckList";
            filterCheckList.Size = new Size(280, 94);
            filterCheckList.TabIndex = 6;

            var filterTypes = filterLoader.GetAvailableFilters().ToArray();
            filterCheckList.Items.Clear();
            filterCheckList.Items.AddRange(filterTypes);
            // 
            // filterLabel
            // 
            filterLabel.AutoSize = true;
            filterLabel.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Point, 204);
            filterLabel.Location = new Point(392, 202);
            filterLabel.Name = "filterLabel";
            filterLabel.Size = new Size(201, 24);
            filterLabel.TabIndex = 7;
            filterLabel.Text = "Выберите фильтры";
            // 
            // historyCheckBox
            // 
            historyCheckBox.AutoSize = true;
            historyCheckBox.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            historyCheckBox.Location = new Point(20, 368);
            historyCheckBox.Name = "historyCheckBox";
            historyCheckBox.Size = new Size(236, 29);
            historyCheckBox.TabIndex = 8;
            historyCheckBox.Text = "Сохранить в историю";
            historyCheckBox.UseVisualStyleBackColor = true;
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientInactiveCaption;
            ClientSize = new Size(649, 501);
            Controls.Add(historyCheckBox);
            Controls.Add(filterLabel);
            Controls.Add(filterCheckList);
            Controls.Add(metricsLabel);
            Controls.Add(metricsCheckList);
            Controls.Add(depthUpDown);
            Controls.Add(depthLabel);
            Controls.Add(analyzeButton);
            Controls.Add(pathTextBox);
            Controls.Add(pathLabel);
            Margin = new Padding(4, 3, 4, 3);
            Name = "MainWindow";
            Text = "DiskAnalyzer";
            Load += MainWindow_Load;
            ((System.ComponentModel.ISupportInitialize)depthUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        private void LoadMetricsToCheckList()
        {
            metricsCheckList.Items.Clear();

            var metricTypes = Assembly
                .Load("DiskAnalyzer.Library")
                .GetTypes()
                .Where(x => x.GetInterfaces().Contains(typeof(IMetric)))
                .Select(m => m.GetCustomAttribute<MetricNameAttribute>()?.Name ?? m.Name)
                .ToArray();

            metricsCheckList.Items.AddRange(metricTypes);
        }

        private void LoadFiltersToCheckList()
        {
            filterCheckList.Items.Clear();

            var metricTypes = Assembly
                .Load("DiskAnalyzer.Library")
                .GetTypes()
                .Where(x => x.GetInterfaces().Contains(typeof(IFileFilter)))
                .Where(x => x.GetCustomAttributes(typeof(FilterNameAttribute), false).Length > 0)
                .Select(m =>
                {
                    var attribute = m.GetCustomAttribute<FilterNameAttribute>();
                    return attribute?.Name ?? m.Name;
                })
                .ToArray();

            filterCheckList.Items.AddRange(metricTypes);
        }

        private void GetSelectedMetrics()
        {
            var metrics = metricsCheckList.CheckedItems;
        }

        private string GetPath()
        {
            return pathTextBox.Text;
        }
        
        private int GetDepth()
        {
            return (int)depthUpDown.Value;
        }

        private bool SetSaveToHistory()
        {
            return historyCheckBox.Checked;
        }

        private void OnAnalyzeButtonClick(object sender, EventArgs e)
        {
            var path = GetPath().EscapeSlashes();
            var maxDepth = GetDepth();
            var selectedMetrics = metricsCheckList.CheckedItems.Cast<string>();
            var weightingTypes = new List<WeightingType?>();
            foreach ( var metric in selectedMetrics )
            {
                var type = typeResolver.GetTypeByDisplayName(metric, typeof(IMetric));
                weightingTypes.Add(conversionService.ConvertMetricToWeightingType(type));
            }
            var selectedFilters = filterCheckList.CheckedItems.Cast<string>();
            var filterTypes = new List<FilterType?>();
            foreach (var filter in selectedFilters)
            {
                var type = typeResolver.GetTypeByDisplayName(filter, typeof(IFileFilter));
                filterTypes.Add(conversionService.ConvertFilterToFilterType(type));
            }
            var saveInHistory = SetSaveToHistory();
        }

        private async Task<WeightingRecord> SendPostRequest(
            List<WeightingType> weightingType, 
            string path, 
            int maxDepth, 
            bool saveInHistory, 
            List<FilterType> filterType)
        {
            using var client = new HttpClient();
            var requestData = new
            {
                type = weightingType.ToString(),
                path = path,
                maxDepth = maxDepth,
                saveInHistory = saveInHistory, 
                filterType = filterType
            };

            var response = await client.PostAsJsonAsync("/api/Weightings", requestData);
            var result = await response.Content.ReadFromJsonAsync<WeightingRecord>();
            return result;
        }
        private System.Windows.Forms.Label pathLabel;
        private System.Windows.Forms.TextBox pathTextBox;
        private System.Windows.Forms.Button analyzeButton;
        private System.Windows.Forms.Label depthLabel;
        private System.Windows.Forms.NumericUpDown depthUpDown;
        private System.Windows.Forms.CheckedListBox metricsCheckList;
        private Label metricsLabel;
        private CheckedListBox filterCheckList;
        private Label filterLabel;
        private CheckBox historyCheckBox;
    }
}

