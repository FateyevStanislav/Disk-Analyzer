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
            this.result = result;
            var rows = result.Measurements.Select(m => new
            {
                Метрика = m.Key,
                Измерение = m.Key == "Count" ? FormatCount(m.Value) : FormatSize(m.Value)
            }).ToList();

            dataGrid.DataSource = rows;
            Text = "РЕЗУЛЬТАТ АНАЛИЗА";
            SetPathLabel(result.Path);
        }

        public void SetGroupingResult(GroupingAnalysisResult result)
        {
            this.result = result;
            var rows = result.Groups.Select(g => new
            {
                Группа = g.Key,
                Файлов = g.Metrics.TryGetValue("Count", out var filesCount) ? filesCount : 0,
                Размер = g.Metrics.TryGetValue("Size", out var totalSize) ? FormatSize(totalSize) : "-"
            }).ToList();

            dataGrid.DataSource = rows;
            Text = "РЕЗУЛЬТАТ ГРУППИРОВКИ";
            SetPathLabel(result.Path);
        }

        public void SetDuplicateResult(DuplicateAnalysisResult result)
        {
            this.result = result;
            var rows = result.DuplicateGroups.Select(g => new
            {
                размер_одного = FormatSize(g.FileSize),
                одинаковых_файлов = g.FileCount,
                потерянное_место = FormatSize(g.TotalWastedSpace),
                оригинал = g.OriginalFile.Path
            }).ToList();

            dataGrid.DataSource = rows;
            Text = "РЕЗУЛЬТАТ ПОИСКА ДУБЛИКАТОВ";
            SetPathLabel(result.Path);

            ConfigureDuplicateResultColumns();
        }

        public static string FormatSize(long bytes)
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

        public static string FormatSize(string bytes)
        {
            return FormatSize(long.Parse(bytes));
        }

        public static string FormatCount(string count)
        {
            int.TryParse(count, out var intCount);
            int mod10 = intCount % 10;
            int mod100 = intCount % 100;

            if (mod100 >= 11 && mod100 <= 14) return $"{count} файлов";
            if (mod10 == 1) return $"{count} файл";
            if (mod10 >= 2 && mod10 <= 4) return $"{count} файла";
            return $"{count} файлов";
        }
    }
}
