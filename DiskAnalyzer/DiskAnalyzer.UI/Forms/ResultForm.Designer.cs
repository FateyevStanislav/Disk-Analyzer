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
            SuspendLayout();
            // 
            // typeLabel
            // 
            typeLabel.AutoSize = true;
            typeLabel.Location = new Point(12, 66);
            typeLabel.Name = "typeLabel";
            typeLabel.Size = new Size(86, 15);
            typeLabel.TabIndex = 1;
            // 
            // ResultLabel
            // 
            ResultLabel.AutoSize = true;
            ResultLabel.Location = new Point(255, 66);
            ResultLabel.Name = "ResultLabel";
            ResultLabel.Size = new Size(86, 15);
            ResultLabel.TabIndex = 3;
            ResultLabel.Text = "fill";
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
            ClientSize = new Size(600, 131);
            Controls.Add(analyzeLabel);
            Controls.Add(ResultLabel);
            Controls.Add(typeLabel);
            Name = "ResultForm";
            Text = "AnalyzeResult";
            ResumeLayout(false);
            PerformLayout();
        }

        private Label typeLabel;
        private Label label2;
        private Label ResultLabel;
        private Label label4;
        private Label analyzeLabel;
    }
}