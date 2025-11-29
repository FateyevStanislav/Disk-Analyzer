using DiskAnalyzer.Domain.Metrics;
using DiskAnalyzer.Infrastructure;
using DiskAnalyzer.Library.Domain.Attributes;

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
            metricLabel = new Label();
            metricResultLabel = new Label();
            analyzeLabel = new Label();
            SuspendLayout();
            // 
            // metricLabel
            // 
            metricLabel.AutoSize = true;
            metricLabel.Location = new Point(12, 66);
            metricLabel.Name = "metricLabel";
            metricLabel.Size = new Size(86, 15);
            metricLabel.TabIndex = 1;
            metricLabel.Text = "Успфвыфывех";
            // 
            // metricResultLabel
            // 
            metricResultLabel.AutoSize = true;
            metricResultLabel.Location = new Point(255, 66);
            metricResultLabel.Name = "metricResultLabel";
            metricResultLabel.Size = new Size(86, 15);
            metricResultLabel.TabIndex = 3;
            metricResultLabel.Text = "fill";
            // 
            // analyzeLabel
            // 
            analyzeLabel.AutoSize = true;
            analyzeLabel.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Point, 204);
            analyzeLabel.Location = new Point(136, 9);
            analyzeLabel.Margin = new Padding(4, 0, 4, 0);
            analyzeLabel.Name = "analyzeLabel";
            analyzeLabel.Size = new Size(69, 24);
            analyzeLabel.TabIndex = 4;
            analyzeLabel.Text = "УСПЕХ";
            // 
            // ResultForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(353, 131);
            Controls.Add(analyzeLabel);
            Controls.Add(metricResultLabel);
            Controls.Add(metricLabel);
            Name = "ResultForm";
            Text = "AnalyzeResult";
            ResumeLayout(false);
            PerformLayout();
        }

        public void SetMetric(IMetric metric)
        {
            metricLabel.Text = metric.GetType().GetDisplayName();
            metricResultLabel.Text = metric.Value.ToString();
        }
        private Label metricLabel;
        private Label label2;
        private Label metricResultLabel;
        private Label label4;
        private Label analyzeLabel;
    }
}