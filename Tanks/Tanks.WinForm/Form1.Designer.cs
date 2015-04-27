using System;
using System.Configuration;

namespace Tanks.WinForm
{
    partial class TankGameForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private int sizeX = Int32.Parse(ConfigurationManager.AppSettings["MaxX"]) * 16;
        private int sizeY = Int32.Parse(ConfigurationManager.AppSettings["MaxY"]) * 16;
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
            this.SuspendLayout();
            // 
            // TankGameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(sizeX, sizeY);
            this.Name = "TankGameForm";
            this.Text = "Tanks Game";
            this.ResumeLayout(false);

        }

        #endregion
    }
}

