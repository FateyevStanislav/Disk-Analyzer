using DiskAnalyzer.Api;
using DiskAnalyzer.Api.Controllers;
using DiskAnalyzer.Api.Controllers.Filters;
using DiskAnalyzer.Library.Domain;
using DiskAnalyzer.Library.Domain.Attributes;
using DiskAnalyzer.Library.Domain.Metrics;
using DiskAnalyzer.Library.Infrastructure;
using DiskAnalyzer.Library.Infrastructure.Filters;
using DiskAnalyzer.UI.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Reflection;
using System.Windows.Forms;

namespace DiskAnalyzer.UI
{
    partial class MainWindow
    {
        private System.ComponentModel.IContainer components = null;
        private IConversionService conversionService = new ConversionsHandler();
        private ITypeResolver typeResolver = new TypeResolver();
        private IMetricLoader metricLoader = new MetricLoader();
        private IFilterLoader filterLoader = new FilterLoader();
        private IApiClient apiClient = new ApiClient(new HttpClient());
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
            metricsLabel = new Label();
            filterListBox = new CheckedListBox();
            filterLabel = new Label();
            historyCheckBox = new CheckBox();
            metricsListBox = new CheckedListBox();
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
            // metricsLabel
            // 
            metricsLabel.AutoSize = true;
            metricsLabel.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Point, 204);
            metricsLabel.Location = new Point(59, 202);
            metricsLabel.Name = "metricsLabel";
            metricsLabel.Size = new Size(195, 24);
            metricsLabel.TabIndex = 5;
            metricsLabel.Text = "Выберите метрику";
            // 
            // filterListBox
            // 
            filterListBox.FormattingEnabled = true;
            filterListBox.Location = new Point(356, 241);
            filterListBox.Name = "filterListBox";
            filterListBox.Size = new Size(280, 94);
            filterListBox.TabIndex = 6;
            filterListBox.Items.AddRange(filterLoader.GetAvailableFilters().ToArray());
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
            // metricsListBox
            // 
            metricsListBox.FormattingEnabled = true;
            metricsListBox.Location = new Point(20, 241);
            metricsListBox.Name = "metricsListBox";
            metricsListBox.Size = new Size(280, 94);
            metricsListBox.TabIndex = 9;
            metricsListBox.SelectionMode = SelectionMode.One;
            metricsListBox.Items.AddRange(metricLoader.GetAvailableMetrics(typeof(IFileMetric)).ToArray());
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientInactiveCaption;
            ClientSize = new Size(649, 501);
            Controls.Add(metricsListBox);
            Controls.Add(historyCheckBox);
            Controls.Add(filterLabel);
            Controls.Add(filterListBox);
            Controls.Add(metricsLabel);
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

        private void GetSelectedMetrics()
        {
            var metrics = metricsListBox.CheckedItems;
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

        private async void OnAnalyzeButtonClick(object sender, EventArgs e)
        {
            try
            {
                var path = GetPath().EscapeSlashes();
                var maxDepth = GetDepth();
                var selectedMetrics = metricsListBox.CheckedItems.Cast<string>();
                var saveInHistory = SetSaveToHistory();

                var selectedMetric = metricsListBox.CheckedItems.Cast<string>().ToArray()[0];
                Type metricType = typeResolver.GetTypeByDisplayName(selectedMetric, typeof(IMetric));
                FilesMeasurementType metric = conversionService.ConvertMetricToMeasurementType(metricType);

                var requestDto = new RequestDto(metric, path, maxDepth, null);

                var result = await apiClient.CreateMeasurementAsync(requestDto);

                if (saveInHistory)
                {
                    await apiClient.SaveToHistoryAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private System.Windows.Forms.Label pathLabel;
        private System.Windows.Forms.TextBox pathTextBox;
        private System.Windows.Forms.Button analyzeButton;
        private System.Windows.Forms.Label depthLabel;
        private System.Windows.Forms.NumericUpDown depthUpDown;
        private Label metricsLabel;
        private CheckedListBox filterListBox;
        private Label filterLabel;
        private CheckBox historyCheckBox;
        private CheckedListBox metricsListBox;
    }
}

