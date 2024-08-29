namespace widget_circularity
{
    partial class WidgetA
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textBox = new TextBox();
            SuspendLayout();
            // 
            // textBox
            // 
            textBox.Location = new Point(97, 85);
            textBox.Name = "textBox";
            textBox.PlaceholderText = "Test Circularity";
            textBox.Size = new Size(265, 31);
            textBox.TabIndex = 0;
            // 
            // WidgetA
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(478, 244);
            Controls.Add(textBox);
            Name = "WidgetA";
            Text = "Widget A";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox;
    }
}