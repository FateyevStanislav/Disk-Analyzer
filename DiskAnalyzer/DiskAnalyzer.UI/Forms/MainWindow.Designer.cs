using DiskAnalyzer.Api;
using DiskAnalyzer.Api.Controllers.Dtos;
using DiskAnalyzer.Api.Factories;
using DiskAnalyzer.Domain.Extensions;
using DiskAnalyzer.Domain.Models.Results;
using DiskAnalyzer.UI.Forms;
using System.Net.Http.Json;
using System.Text.Json;
using System.Windows.Forms;

namespace DiskAnalyzer.UI
{
    internal partial class MainWindow
    {
        private System.ComponentModel.IContainer components = null;
        private IApiClient apiClient = new ApiClient(new HttpClient());
        private Dictionary<string, string> _filterParameters = new();
        private bool historyOpened = false;
        private AnalysisResult selectedHistoryRecord;

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
            duplicateCheckBox = new CheckBox();
            pathLabel = new Label();
            pathTextBox = new TextBox();
            analyzeButton = new Button();
            depthLabel = new Label();
            depthUpDown = new NumericUpDown();
            metricsLabel = new Label();
            filterListBox = new CheckedListBox();
            filterLabel = new Label();
            metricsListBox = new CheckedListBox();
            groupingListBox = new CheckedListBox();
            groupingLabel = new Label();
            showHistoryButton = new Button();
            historyDataGrid = new DataGridView();
            historyDetailPanel = new Panel();
            historyDetailText = new RichTextBox();
            historyButtonPanel = new Panel();
            deleteHistoryButton = new Button();
            closeHistoryDetailButton = new Button();

            ((System.ComponentModel.ISupportInitialize)depthUpDown).BeginInit();
            SuspendLayout();

            pathLabel.AutoSize = true;
            pathLabel.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Point, 204);
            pathLabel.Location = new Point(15, 9);
            pathLabel.Margin = new Padding(4, 0, 4, 0);
            pathLabel.Name = "pathLabel";
            pathLabel.Size = new Size(263, 24);
            pathLabel.TabIndex = 0;
            pathLabel.Text = "Введите путь для оценки";

            pathTextBox.BackColor = Color.Gainsboro;
            pathTextBox.BorderStyle = BorderStyle.FixedSingle;
            pathTextBox.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 204);
            pathTextBox.Location = new Point(20, 58);
            pathTextBox.Margin = new Padding(4, 3, 4, 3);
            pathTextBox.Name = "pathTextBox";
            pathTextBox.Size = new Size(616, 20);
            pathTextBox.TabIndex = 1;

            analyzeButton.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            analyzeButton.Location = new Point(270, 520);
            analyzeButton.Margin = new Padding(4, 3, 4, 3);
            analyzeButton.Name = "analyzeButton";
            analyzeButton.Size = new Size(112, 33);
            analyzeButton.TabIndex = 2;
            analyzeButton.Text = "Оценить";
            analyzeButton.UseVisualStyleBackColor = true;
            analyzeButton.Click += OnAnalyzeButtonClick;

            showHistoryButton.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            showHistoryButton.Location = new Point(356, 400);
            showHistoryButton.Margin = new Padding(4, 3, 4, 3);
            showHistoryButton.Name = "showHistoryButton";
            showHistoryButton.Size = new Size(224, 33);
            showHistoryButton.TabIndex = 2;
            showHistoryButton.Text = "Показать историю";
            showHistoryButton.UseVisualStyleBackColor = true;
            showHistoryButton.Click += ShowHistoryButton_Click;

            depthLabel.AutoSize = true;
            depthLabel.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Point, 204);
            depthLabel.Location = new Point(15, 107);
            depthLabel.Margin = new Padding(4, 0, 4, 0);
            depthLabel.Name = "depthLabel";
            depthLabel.Size = new Size(263, 24);
            depthLabel.TabIndex = 0;
            depthLabel.Text = "Введите глубину анализа";

            groupingLabel.AutoSize = true;
            groupingLabel.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Point, 204);
            groupingLabel.Location = new Point(15, 350);
            groupingLabel.Name = "groupingLabel";
            groupingLabel.Size = new Size(253, 24);
            groupingLabel.TabIndex = 10;
            groupingLabel.Text = "Выберите тип группировки";

            depthUpDown.BackColor = Color.Gainsboro;
            depthUpDown.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            depthUpDown.Location = new Point(20, 157);
            depthUpDown.Margin = new Padding(4, 3, 4, 3);
            depthUpDown.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            depthUpDown.Name = "depthUpDown";
            depthUpDown.Size = new Size(62, 21);
            depthUpDown.TabIndex = 3;
            depthUpDown.Value = new decimal(new int[] { 0, 0, 0, 0 });

            metricsLabel.AutoSize = true;
            metricsLabel.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Point, 204);
            metricsLabel.Location = new Point(59, 202);
            metricsLabel.Name = "metricsLabel";
            metricsLabel.Size = new Size(195, 24);
            metricsLabel.TabIndex = 5;
            metricsLabel.Text = "Выберите метрики";

            filterListBox.FormattingEnabled = true;
            filterListBox.Location = new Point(356, 241);
            filterListBox.Name = "filterListBox";
            filterListBox.Size = new Size(280, 94);
            filterListBox.TabIndex = 6;
            filterListBox.ItemCheck += FilterListBox_ItemCheck;

            filterLabel.AutoSize = true;
            filterLabel.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Point, 204);
            filterLabel.Location = new Point(392, 202);
            filterLabel.Name = "filterLabel";
            filterLabel.Size = new Size(201, 24);
            filterLabel.TabIndex = 7;
            filterLabel.Text = "Выберите фильтры";

            duplicateCheckBox.AutoSize = true;
            duplicateCheckBox.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            duplicateCheckBox.Location = new Point(356, 350);
            duplicateCheckBox.Name = "duplicateCheckBox";
            duplicateCheckBox.Size = new Size(236, 29);
            duplicateCheckBox.TabIndex = 8;
            duplicateCheckBox.Text = "Найти дубликаты";
            duplicateCheckBox.UseVisualStyleBackColor = true;

            metricsListBox.FormattingEnabled = true;
            metricsListBox.Location = new Point(20, 241);
            metricsListBox.Name = "metricsListBox";
            metricsListBox.Size = new Size(280, 94);
            metricsListBox.TabIndex = 9;
            metricsListBox.Items.AddRange(new object[] { "Size", "Count" });

            groupingListBox.FormattingEnabled = true;
            groupingListBox.Location = new Point(20, 390);
            groupingListBox.Name = "groupingListBox";
            groupingListBox.Size = new Size(280, 94);
            groupingListBox.TabIndex = 11;
            groupingListBox.Items.AddRange(new object[] { "Extension", "LastAccessTime", "SizeBucket" });

            historyDataGrid.Location = new Point(650, 0);
            historyDataGrid.Size = new Size(650, 580);
            historyDataGrid.Visible = false;
            historyDataGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            historyDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            historyDataGrid.ReadOnly = true;
            historyDataGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "История",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            historyDataGrid.DefaultCellStyle = new DataGridViewCellStyle
            {
                WrapMode = DataGridViewTriState.True,
                Alignment = DataGridViewContentAlignment.TopLeft
            };
            historyDataGrid.CellDoubleClick += HistoryDataGrid_CellDoubleClick;

            historyDetailPanel.Dock = DockStyle.Bottom;
            historyDetailPanel.Height = 300;
            historyDetailPanel.Visible = false;
            historyDetailPanel.BorderStyle = BorderStyle.FixedSingle;
            historyDetailPanel.BackColor = Color.White;

            historyDetailText.Dock = DockStyle.Fill;
            historyDetailText.ReadOnly = true;
            historyDetailText.Font = new Font("Consolas", 10);
            historyDetailText.WordWrap = true;
            historyDetailText.ScrollBars = RichTextBoxScrollBars.Vertical;
            historyDetailText.BorderStyle = BorderStyle.None;
            historyDetailText.Margin = new Padding(10);

            historyButtonPanel.Dock = DockStyle.Bottom;
            historyButtonPanel.Height = 50;
            historyButtonPanel.BackColor = SystemColors.Control;

            deleteHistoryButton.Text = "Удалить из истории";
            deleteHistoryButton.Size = new Size(180, 35);
            deleteHistoryButton.Location = new Point(20, 8);
            deleteHistoryButton.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            deleteHistoryButton.Click += DeleteHistoryButton_Click;

            closeHistoryDetailButton.Text = "✖️ Закрыть";
            closeHistoryDetailButton.Size = new Size(120, 35);
            closeHistoryDetailButton.Location = new Point(220, 8);
            closeHistoryDetailButton.Font = new Font("Microsoft Sans Serif", 10F);
            closeHistoryDetailButton.Click += (s, e) => HideHistoryDetail();

            historyButtonPanel.Controls.Add(deleteHistoryButton);
            historyButtonPanel.Controls.Add(closeHistoryDetailButton);
            historyDetailPanel.Controls.Add(historyDetailText);
            historyDetailPanel.Controls.Add(historyButtonPanel);

            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.GradientActiveCaption;
            ClientSize = new Size(649, 580);
            Controls.Add(duplicateCheckBox);
            Controls.Add(groupingListBox);
            Controls.Add(metricsListBox);
            Controls.Add(filterLabel);
            Controls.Add(filterListBox);
            Controls.Add(metricsLabel);
            Controls.Add(depthUpDown);
            Controls.Add(depthLabel);
            Controls.Add(analyzeButton);
            Controls.Add(pathTextBox);
            Controls.Add(pathLabel);
            Controls.Add(groupingLabel);
            Controls.Add(showHistoryButton);
            Controls.Add(historyDataGrid);
            Controls.Add(historyDetailPanel);
            Margin = new Padding(4, 3, 4, 3);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MainWindow";
            Text = "DiskAnalyzer";
            ((System.ComponentModel.ISupportInitialize)depthUpDown).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label pathLabel;
        private TextBox pathTextBox;
        private Button analyzeButton;
        private Label depthLabel;
        private NumericUpDown depthUpDown;
        private Label metricsLabel;
        private Label groupingLabel;
        private CheckedListBox filterListBox;
        private Label filterLabel;
        private CheckedListBox metricsListBox;
        private CheckedListBox groupingListBox;
        private CheckBox duplicateCheckBox;
        private Button showHistoryButton;
        private DataGridView historyDataGrid;
        private Panel historyDetailPanel;
        private RichTextBox historyDetailText;
        private Button deleteHistoryButton;
        private Button closeHistoryDetailButton;
        private Panel historyButtonPanel;

        private async void OnAnalyzeButtonClick(object sender, EventArgs e)
        {
            try
            {
                var path = pathTextBox.Text.EscapeSlashes();
                var maxDepth = (int)depthUpDown.Value;
                var filterDtos = BuildFilterDtos();

                bool findDuplicates = duplicateCheckBox.Checked;
                bool hasMetrics = metricsListBox.CheckedItems.Count > 0;
                bool hasGrouping = groupingListBox.CheckedItems.Count > 0;

                if (!hasMetrics && !findDuplicates)
                {
                    MessageBox.Show("Выберите метрики!");
                    return;
                }
                if (hasGrouping && findDuplicates)
                {
                    MessageBox.Show("Либо группировка либо дубликаты!");
                    return;
                }

                if (!hasGrouping && !findDuplicates)
                {
                    await RunMeasurementAnalysis(path, maxDepth, filterDtos);
                }
                else if (hasGrouping && !findDuplicates)
                {
                    await RunGroupingAnalysis(path, maxDepth, filterDtos);
                }
                else if (!hasGrouping && findDuplicates)
                {
                    await RunDuplicateAnalysis(path, maxDepth, filterDtos);
                }
            }
            catch (HttpRequestException httpEx)
            {
                MessageBox.Show($"Ошибка соединения с сервером:\n{httpEx.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private async Task RunMeasurementAnalysis(string path, int maxDepth, List<FilterDto> filterDtos)
        {
            var strategy = GetFMSType();
            var requestDto = new FilesMeasurementDto(path, strategy, maxDepth, filterDtos);
            var result = await apiClient.CreateMeasurementAsync(requestDto);

            var resultForm = new ResultForm();
            resultForm.SetMetricsResult(result);
            resultForm.Show();
        }

        private async Task RunGroupingAnalysis(string path, int maxDepth, List<FilterDto> filterDtos)
        {
            var groupingType = GetFGSType();
            var strategy = GetFMSType();
            var requestDto = new GroupingMeasurementDto(path, groupingType, maxDepth, strategy, filterDtos);
            var result = await apiClient.CreateGroupingAsync(requestDto);

            var resultForm = new ResultForm();
            resultForm.SetGroupingResult(result);
            resultForm.Show();
        }

        private async Task RunDuplicateAnalysis(string path, int maxDepth, List<FilterDto> filterDtos)
        {
            var requestDto = new DuplicateFinderDto(path, maxDepth, filterDtos);
            var result = await apiClient.FindDuplicatesAsync(requestDto);

            var resultForm = new ResultForm();
            resultForm.SetDuplicateResult(result);
            resultForm.Show();
        }

        private IEnumerable<FilesMeasurementType> GetFMSType()
        {
            var selected = new List<FilesMeasurementType>();
            foreach (var item in metricsListBox.CheckedItems)
            {
                if (Enum.TryParse<FilesMeasurementType>(item.ToString(), out var type))
                    selected.Add(type);
            }
            return selected;
        }
        private FilesGroupingType GetFGSType()
        {
            return Enum.Parse<FilesGroupingType>(groupingListBox.CheckedItems[0].ToString());
        }

        private void FilterListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                var filterName = filterListBox.Items[e.Index].ToString();
                using (var form = new Form())
                {
                    form.Size = new Size(500, 300);
                    form.Text = $"Введите параметры для {filterName}";
                    form.StartPosition = FormStartPosition.CenterParent;

                    var buttonPanel = new Panel { Dock = DockStyle.Bottom, Height = 50 };
                    var textBox = new TextBox { Multiline = true, Dock = DockStyle.Fill, Text = GetDefaultParametersJson(filterName) };
                    var okButton = new Button { Text = "OK", DialogResult = DialogResult.OK, Size = new Size(80, 30), Location = new Point(165, 10) };

                    buttonPanel.Controls.Add(okButton);
                    form.Controls.Add(buttonPanel);
                    form.Controls.Add(textBox);

                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        _filterParameters[filterName] = textBox.Text;
                    }
                    else
                    {
                        e.NewValue = CheckState.Unchecked;
                    }
                }
            }
        }

        private List<FilterDto> BuildFilterDtos()
        {
            var filterDtos = new List<FilterDto>();
            foreach (var checkedItem in filterListBox.CheckedItems)
            {
                var filterName = checkedItem.ToString();
                if (_filterParameters.TryGetValue(filterName, out var json))
                {
                    var jsonDict = JsonSerializer.Deserialize<Dictionary<string, object>>(json);
                    if (!jsonDict.TryGetValue("type", out var typeValue))
                    {
                        throw new Exception($"В JSON фильтра {filterName} нет поля 'type'!");
                    }
                    var filterDto = new FilterDto(
                        Type: filterName,
                        FilterParams: jsonDict.Where(kvp => kvp.Key != "type")
                                             .ToDictionary(kvp => kvp.Key, kvp => kvp.Value?.ToString() ?? "")
                    );
                    filterDtos.Add(filterDto);
                }
                else
                {
                    throw new Exception($"Для фильтра {filterName} не указаны параметры!");
                }
            }
            return filterDtos;
        }

        private async void ShowHistoryButton_Click(object sender, EventArgs e)
        {
            historyDataGrid.Visible = !historyDataGrid.Visible;
            historyOpened = !historyOpened;

            if (historyOpened)
            {
                ClientSize = new Size(1300, 580);
                await LoadHistoryToDataGrid();
                HideHistoryDetail();
            }
            else
            {
                ClientSize = new Size(650, 580);
                HideHistoryDetail();
            }
        }

        private async Task LoadHistoryToDataGrid()
        {
            historyDataGrid.BringToFront();
            historyDataGrid.Rows.Clear();

            var history = await apiClient.GetRecordsFromHistoryAsync();

            foreach (var record in history)
            {
                string displayText = FormatHistoryItem(record);
                int rowIndex = historyDataGrid.Rows.Add(displayText);
                historyDataGrid.Rows[rowIndex].Tag = record;
            }

            historyDataGrid.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
        }

        private string FormatHistoryItem(AnalysisResult record)
        {
            if (record is DuplicateAnalysisResult duplicateAnalysis)
            {
                var filterText = duplicateAnalysis.Filters?.Any() == true
                    ? string.Join(", ", duplicateAnalysis.Filters.Select(f => f.Type))
                    : "Нет фильтров";

                return $"ПОИСК ДУБЛИКАТОВ\n" +
                       $"Дата: {duplicateAnalysis.CreatedAt:dd.MM.yy HH:mm}\n" +
                       $"Путь: {duplicateAnalysis.Path}\n" +
                       $"Потеряно места: {ResultForm.FormatSize(duplicateAnalysis.WastedSpace)}\n" +
                       $"Групп дубликатов: {duplicateAnalysis.DuplicateGroups?.Count ?? 0}\n" +
                       $"Фильтры: {filterText}";
            }
            else if (record is MeasurementAnalysisResult measurementAnalysis)
            {
                var metricsText = measurementAnalysis.Measurements?.Any() == true
                    ? string.Join(", ", measurementAnalysis.Measurements.Select(m => FormatMetric(m.Key, m.Value)))
                    : "Нет метрик";

                var filterText = measurementAnalysis.Filters?.Any() == true
                    ? string.Join(", ", measurementAnalysis.Filters.Select(f => f.Type))
                    : "Нет фильтров";

                return $"АНАЛИЗ МЕТРИК\n" +
                       $"Дата: {measurementAnalysis.CreatedAt:dd.MM.yy HH:mm}\n" +
                       $"Путь: {measurementAnalysis.Path}\n" +
                       $"Метрики: {metricsText}\n" +
                       $"Фильтры: {filterText}";
            }
            else if (record is GroupingAnalysisResult groupingAnalysis)
            {
                var groupsCount = groupingAnalysis.Groups?.Count ?? 0;
                var filterText = groupingAnalysis.Filters?.Any() == true
                    ? string.Join(", ", groupingAnalysis.Filters.Select(f => f.Type))
                    : "Нет фильтров";

                return $"ГРУППИРОВКА ПО {groupingAnalysis.GrouperType?.ToUpper()}\n" +
                       $"Дата: {groupingAnalysis.CreatedAt:dd.MM.yy HH:mm}\n" +
                       $"Путь: {groupingAnalysis.Path}\n" +
                       $"Количество групп: {groupsCount}\n" +
                       $"Фильтры: {filterText}";
            }

            return $"НЕИЗВЕСТНЫЙ ТИП РЕЗУЛЬТАТА\n" +
                   $"Дата: {record.CreatedAt:dd.MM.yy HH:mm}\n" +
                   $"Путь: {record.Path}";
        }

        private void HistoryDataGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < historyDataGrid.Rows.Count)
            {
                selectedHistoryRecord = historyDataGrid.Rows[e.RowIndex].Tag as AnalysisResult;
                if (selectedHistoryRecord != null)
                {
                    ShowHistoryDetail(selectedHistoryRecord);
                }
            }
        }

        private void ShowHistoryDetail(AnalysisResult record)
        {
            historyDetailText.Text = FormatHistoryDetail(record);
            historyDetailPanel.Visible = true;
            historyDetailPanel.BringToFront();

            foreach (DataGridViewRow row in historyDataGrid.Rows)
            {
                if (row.Tag == record)
                {
                    row.Selected = true;
                    historyDataGrid.FirstDisplayedScrollingRowIndex = row.Index;
                    break;
                }
            }
        }
        private void HideHistoryDetail()
        {
            historyDetailPanel.Visible = false;
            selectedHistoryRecord = null;
        }

        private string FormatHistoryDetail(AnalysisResult record)
        {
            if (record is MeasurementAnalysisResult measurement)
                return FormatMeasurementDetail(measurement);
            else if (record is GroupingAnalysisResult grouping)
                return FormatGroupingDetail(grouping);
            else if (record is DuplicateAnalysisResult duplicate)
                return FormatDuplicateDetail(duplicate);

            return $"Тип: {record.GetType().Name}\n" +
                   $"ID: {record.Id}\n" +
                   $"Дата: {record.CreatedAt:dd.MM.yyyy HH:mm:ss}\n" +
                   $"Путь: {record.Path}\n" +
                   $"Фильтры: {(record.Filters?.Any() == true ? string.Join(", ", record.Filters.Select(f => f.Type)) : "Нет")}";
        }

        private string FormatMeasurementDetail(MeasurementAnalysisResult result)
        {
            var text = $"АНАЛИЗ МЕТРИК\n\n";
            text += $"Дата: {result.CreatedAt:dd.MM.yyyy HH:mm:ss}\n";
            text += $"Путь: {result.Path}\n\n";

            foreach (var metric in result.Measurements)
            {
                text += $"{FormatMetric(metric.Key, metric.Value)}\n";
            }

            if (result.Filters?.Any() == true)
            {
                text += $"\nФильтры:\n";
                foreach (var filter in result.Filters)
                {
                    text += $"{filter.Type}\n";
                }
            }

            return text;
        }

        private string FormatGroupingDetail(GroupingAnalysisResult result)
        {
            var text = $"ГРУППИРОВКА ПО {result.GrouperType?.ToUpper()}\n\n";
            text += $"Дата: {result.CreatedAt:dd.MM.yyyy HH:mm:ss}\n";
            text += $"Путь: {result.Path}\n";
            text += $"Количество групп: {result.Groups.Count}\n\n";

            if (result.Filters?.Any() == true)
            {
                text += $"Фильтры: {string.Join(", ", result.Filters.Select(f => f.Type))}\n\n";
            }

            text += $"ГРУППЫ:\n--------------------------------------\n\n";

            foreach (var group in result.Groups)
            {
                var filesCount = group.Metrics.TryGetValue("Count", out var count) ? FormatCount(count.ToString()) : "0";
                var size = group.Metrics.TryGetValue("Size", out var sizeValue) ? FormatSize(sizeValue) : "-";

                text += $"Группа: {group.Key}\n";
                text += $"  Файлов: {filesCount}\n";
                text += $"  Размер: {size}\n\n";
            }

            return text;
        }

        private string FormatDuplicateDetail(DuplicateAnalysisResult result)
        {
            var text = $"ПОИСК ДУБЛИКАТОВ\n\n";
            text += $"Дата: {result.CreatedAt:dd.MM.yyyy HH:mm:ss}\n";
            text += $"Путь: {result.Path}\n\n";
            text += $"Всего групп дубликатов: {result.DuplicateGroups.Count}\n";
            text += $"Потеряно места: {FormatSize(result.WastedSpace)}\n\n";

            if (result.Filters?.Any() == true)
            {
                text += $"Фильтры:\n";
                foreach (var filter in result.Filters)
                {
                    text += $"{filter.Type}\n";
                }
                text += "\n";
            }

            text += $"ГРУППЫ ДУБЛИКАТОВ:\n--------------------------------------\n\n";

            foreach (var group in result.DuplicateGroups)
            {
                text += $"• Размер: {FormatSize(group.FileSize)} | ";
                text += $"Файлов: {group.FileCount} | ";
                text += $"Потеряно: {FormatSize(group.TotalWastedSpace)}\n";
                text += $"  Оригинал: {group.OriginalFile.Path}\n\n";
            }

            return text;
        }

        private string FormatMetric(string key, string value)
        {
            return key switch
            {
                "Count" => $"Файлов: {FormatCount(value)}",
                "Size" => $"Размер: {FormatSize(value)}",
                _ => $"{key}: {value}"
            };
        }

        private async void DeleteHistoryButton_Click(object sender, EventArgs e)
        {
            if (selectedHistoryRecord == null) return;

            var dialogResult = MessageBox.Show(
                $"Удалить результат анализа от {selectedHistoryRecord.CreatedAt:dd.MM.yyyy HH:mm}?\n" +
                $"Путь: {selectedHistoryRecord.Path}",
                "Подтверждение удаления",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);

            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    deleteHistoryButton.Enabled = false;
                    bool success = await apiClient.DeleteFromHistoryAsync(selectedHistoryRecord.Id);

                    if (success)
                    {
                        MessageBox.Show("Результат удален из истории", "Успех",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        foreach (DataGridViewRow row in historyDataGrid.Rows)
                        {
                            if (row.Tag == selectedHistoryRecord)
                            {
                                historyDataGrid.Rows.Remove(row);
                                break;
                            }
                        }
                        HideHistoryDetail();
                        await LoadHistoryToDataGrid();
                    }
                    else
                    {
                        MessageBox.Show("Не удалось удалить результат", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении: {ex.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    deleteHistoryButton.Enabled = true;
                }
            }
        }

        private static string FormatSize(long bytes)
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

        private static string FormatSize(string sizeString)
        {
            if (sizeString.Contains("B") || sizeString.Contains("KB") ||
                sizeString.Contains("MB") || sizeString.Contains("GB") ||
                sizeString.Contains("TB"))
            {
                return sizeString;
            }
            return FormatSize(long.Parse(sizeString));
        }

        private static string FormatCount(string count)
        {
            int.TryParse(count, out var intCount);
            int mod10 = intCount % 10;
            int mod100 = intCount % 100;

            if (mod100 >= 11 && mod100 <= 14) return $"{count} файлов";
            if (mod10 == 1) return $"{count} файл";
            if (mod10 >= 2 && mod10 <= 4) return $"{count} файла";
            return $"{count} файлов";
        }

        private string GetDefaultParametersJson(string filterName)
        {
            var parameters = filters[filterName];

            var jsonDict = new Dictionary<string, object>
            {
                { "type", filterName.Replace("Filter", "") }
            };

            foreach (var param in parameters)
            {
                jsonDict[param.Key] = GetDefaultValueForType(param.Value);
            }

            return JsonSerializer.Serialize(jsonDict, new JsonSerializerOptions
            {
                WriteIndented = true
            });
        }

        private object GetDefaultValueForType(string typeName)
        {
            if (typeName.Contains("String"))
            {
                return "";
            }
            else if (typeName.Contains("Int") || typeName.Contains("Long"))
            {
                return 0;
            }
            else if (typeName.Contains("DateTime"))
            {
                return DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
            }
            else
            {
                return "";
            }
        }
    }
}