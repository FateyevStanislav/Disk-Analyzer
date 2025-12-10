using DiskAnalyzer.Domain.Records.Grouping;
using DiskAnalyzer.Domain.Records.Measurement;
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

        public void SetMetricsResult(FilesMeasurementRecord result)
        {
            typeLabel.Text = $"Количество файлов: {result.FileCount}\n" +
                              $"Общий размер: {FormatSize(result.TotalSize)}";

            ResultLabel.Text = $"Путь: {result.Path}";
        }

        public void SetGroupingResult(FilesGroupingRecord result)
        {
            Controls.Clear();

            var dataGrid = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                DataSource = result.Groups.Select(g => new
                {
                    Группа = g.Key,
                    Файлов = g.Files.Count,
                    Размер = FormatSize(GetGroupSize(g))
                }).ToList()
            };

            Controls.Add(dataGrid);
        }

        private long GetGroupSize(Group group)
        {
            return group.Files.Sum(f => f.Size);
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
