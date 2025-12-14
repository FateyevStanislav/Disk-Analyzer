using DiskAnalyzer.Domain.Models.Results;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiskAnalyzer.UI.Forms
{
    public partial class ResultForm : Form
    {
        public ResultForm()
        {
            InitializeComponent();
        }

        public void SetMetricsResult(MeasurementAnalysisResult result)
        {
            var countText = result.Measurements.TryGetValue("Count", out var count)
                ? "Количество файлов: " + count
                : string.Empty;

            var sizeText = result.Measurements.TryGetValue("Size", out var size)
                ? "Размер файлов: " + FormatSize(long.Parse(size))
                : string.Empty;

            typeLabel.Text = $"{countText}\n{sizeText}";


            ResultLabel.Text = $"Путь: {result.Path}";
        }

        public void SetGroupingResult(GroupingAnalysisResult result)
        {
            Controls.Clear();

            var rows = result.Groups.Select(g => new
            {
                Группа = g.Key,
                Файлов = g.Metrics.TryGetValue("Count", out var filesCount) ? filesCount : 0,
                Размер = g.Metrics.TryGetValue("Size", out var totalSize) ? FormatSize(totalSize) : "-"
            }).ToList();

            var dataGrid = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                DataSource = rows
            };

            Controls.Add(dataGrid);
        }

        private string FormatSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double size = bytes;
            int order = 0;

            while (size >= 1024 && order < sizes.Length - 1)
            {
                order++;
                size /= 1024;
            }

            return $"{size:0.##} {sizes[order]}";
        }
    }
}
