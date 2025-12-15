using DiskAnalyzer.Infrastructure;

namespace DiskAnalyzer.UI.Forms
{
    partial class ResultForm
    {
        private System.ComponentModel.IContainer components = null;
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
            typeLabel = new Label();
            ResultLabel = new Label();
            analyzeLabel = new Label();
            dataGrid = new DataGridView();
            pathLabel = new Label();
            historyButton = new Button();
            SuspendLayout();
            //
            // PathLabel
            //
            pathLabel.Text = "default";
            pathLabel.AutoSize = true;
            pathLabel.Location = new Point(10, 10);
            pathLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            pathLabel.Name = "pathLabel";
            pathLabel.TabIndex = 0;
            //
            // HistoryButton
            //
            historyButton.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            historyButton.Location = new Point(10, 50);
            historyButton.Margin = new Padding(4, 3, 4, 3);
            historyButton.Name = "historyButton";
            historyButton.AutoSize = true;
            historyButton.TabIndex = 2;
            historyButton.Text = "Добавить в историю";
            historyButton.UseVisualStyleBackColor = true;
            historyButton.Click += HistoryButton_Click;
            //
            // DataGrid
            //
            dataGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom |
                             AnchorStyles.Left | AnchorStyles.Right;
            dataGrid.Location = new Point(10, 120);
            dataGrid.Size = new Size(980, 270);

            dataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dataGrid.RowTemplate.Height = 40;
            dataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dataGrid.RowTemplate.Height = 40;
            // 
            // ResultForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 400);
            Controls.Add(pathLabel);
            Controls.Add(historyButton);
            Controls.Add(dataGrid);
            Name = "ResultForm";
            Text = "AnalyzeResult";
            ResumeLayout(false);
            PerformLayout();
        }

        private void HistoryButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SetPathLabel(string path)
        {
            pathLabel.Text = $"Путь: {path}";
        }

        private void ConfigureDuplicateResultColumns()
        {
            if (dataGrid.Columns.Count >= 4)
            {
                dataGrid.Columns[0].Width = 120;
                dataGrid.Columns[1].Width = 150;
                dataGrid.Columns[2].Width = 150;

                dataGrid.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                dataGrid.Columns[3].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            }
        }

        private Label typeLabel;
        private Label ResultLabel;
        private Label analyzeLabel;
        private Label pathLabel;
        private DataGridView dataGrid;
        private object rows;
        private Button historyButton;
    }
}