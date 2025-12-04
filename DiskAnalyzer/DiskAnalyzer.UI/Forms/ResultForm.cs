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

        public void SetResult(FilesMeasurementRecord result)
        {
            metricLabel.Text = $"Количество файлов: {result.FileCount}\n" +
                              $"Общий размер: {FormatSize(result.TotalSize)}";

            metricResultLabel.Text = $"Путь: {result.Path}";
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
