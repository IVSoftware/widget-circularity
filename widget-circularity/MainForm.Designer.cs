﻿namespace widget_circularity
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            buttonSetProgrammatically = new Button();
            SuspendLayout();
            // 
            // buttonSetProgrammatically
            // 
            buttonSetProgrammatically.Location = new Point(66, 103);
            buttonSetProgrammatically.Name = "buttonSetProgrammatically";
            buttonSetProgrammatically.Size = new Size(324, 34);
            buttonSetProgrammatically.TabIndex = 0;
            buttonSetProgrammatically.Text = "Set Programmatically";
            buttonSetProgrammatically.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(478, 244);
            Controls.Add(buttonSetProgrammatically);
            Name = "MainForm";
            Text = "Main Form";
            ResumeLayout(false);
        }

        #endregion

        private Button buttonSetProgrammatically;
    }
}
