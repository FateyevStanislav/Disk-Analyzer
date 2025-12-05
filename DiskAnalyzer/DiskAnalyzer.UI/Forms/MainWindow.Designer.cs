using DiskAnalyzer.Api;
using DiskAnalyzer.Api.Controllers;
using DiskAnalyzer.Api.Factories;
using DiskAnalyzer.Domain.Extensions;
using DiskAnalyzer.Infrastructure;
using DiskAnalyzer.UI.Forms;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Reflection;
using System.Text.Json;
using System.Windows.Forms;

namespace DiskAnalyzer.UI
{
    partial class MainWindow
    {
        private System.ComponentModel.IContainer components = null;
        private IApiClient apiClient = new ApiClient(new HttpClient());
        private Dictionary<string, string> _filterParameters = new();
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
            pathTextBox.Size = new Size(616, 20);
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
            depthUpDown.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            depthUpDown.Name = "depthUpDown";
            depthUpDown.Size = new Size(62, 21);
            depthUpDown.TabIndex = 3;
            depthUpDown.Value = new decimal(new int[] { 0, 0, 0, 0 });
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
            metricsListBox.Items.AddRange(
                FilesMeasurementStrategyType.Size, 
                FilesMeasurementStrategyType.Count 
                );

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

        private FilesMeasurementStrategyType GetFMSType()
        {
            if (metricsListBox.CheckedItems.Count > 1)
            {
                return FilesMeasurementStrategyType.Combined;
            }
            return Enum.Parse<FilesMeasurementStrategyType>(metricsListBox.CheckedItems[0].ToString());
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

                    var buttonPanel = new Panel
                    {
                        Dock = DockStyle.Bottom,
                        Height = 50
                    };

                    var textBox = new TextBox
                    {
                        Multiline = true,
                        Dock = DockStyle.Fill,
                        Text = GetDefaultParametersJson(filterName),
                    };

                    var okButton = new Button
                    {
                        Text = "OK",
                        DialogResult = DialogResult.OK,
                        Size = new Size(80, 30),
                        Location = new Point(165, 10)
                    };

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

        private async void OnAnalyzeButtonClick(object sender, EventArgs e)
        {
            try
            {
                var path = pathTextBox.Text.EscapeSlashes();
                var maxDepth = (int)depthUpDown.Value;
                var strategy = GetFMSType();
                var saveInHistory = historyCheckBox.Checked;
                var filterDtos = new List<FilterDto>();

                foreach (var checkedItem in filterListBox.CheckedItems)
                {
                    var filterName = checkedItem.ToString();

                    if (_filterParameters.TryGetValue(filterName, out var json))
                    {
                        try
                        {
                            var jsonDict = JsonSerializer.Deserialize<Dictionary<string, object>>(json);

                            if (!jsonDict.TryGetValue("type", out var typeValue))
                            {
                                MessageBox.Show($"В JSON фильтра {filterName} нет поля 'type'!");
                                return;
                            }

                            var filterDto = new FilterDto(
                                Type: filterName,
                                FilterParams: jsonDict
                                    .Where(kvp => kvp.Key != "type")
                                    .ToDictionary(
                                        kvp => kvp.Key,
                                        kvp => kvp.Value?.ToString() ?? ""
                                    )
                            );

                            filterDtos.Add(filterDto);
                        }
                        catch (JsonException)
                        {
                            MessageBox.Show($"Ошибка в параметрах фильтра {filterName}!");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Для фильтра {filterName} не указаны параметры!");
                        return;
                    }
                }

                if (metricsListBox.CheckedItems.Count == 0)
                {
                    MessageBox.Show("Выберите хотя бы одну метрику!");
                    return;
                }

                var requestDto = new FilesMeasurementDto(
                    StrategyType: strategy,
                    Path: path,
                    MaxDepth: maxDepth,
                    Filters: filterDtos
                );

                var result = await apiClient.CreateMeasurementAsync(requestDto);

                var resultForm = new ResultForm();
                resultForm.SetResult(result);
                resultForm.Show();
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

