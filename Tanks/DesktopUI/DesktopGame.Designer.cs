using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms.VisualStyles;

namespace TanksGame.DesktopUI
{
    partial class frmDesktopGame
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
            this.pbCanvas = new System.Windows.Forms.FlowLayoutPanel();
            this.lblEnemyHealth = new System.Windows.Forms.Label();
            this.lblEnemyHealthValue = new System.Windows.Forms.Label();
            this.lblPlayerHealth = new System.Windows.Forms.Label();
            this.lblPlayerHealthValue = new System.Windows.Forms.Label();
            this.pbCanvas.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbCanvas
            // 
            this.pbCanvas.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pbCanvas.Controls.Add(this.lblEnemyHealth);
            this.pbCanvas.Controls.Add(this.lblEnemyHealthValue);
            this.pbCanvas.Controls.Add(this.lblPlayerHealth);
            this.pbCanvas.Controls.Add(this.lblPlayerHealthValue);
            this.pbCanvas.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pbCanvas.Location = new System.Drawing.Point(1, 1);
            this.pbCanvas.Name = "pbCanvas";
            this.pbCanvas.Size = new System.Drawing.Size(1100, 498);
            this.pbCanvas.TabIndex = 0;
            // 
            // lblEnemyHealth
            // 
            this.lblEnemyHealth.AutoSize = true;
            this.lblEnemyHealth.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblEnemyHealth.Location = new System.Drawing.Point(3, 0);
            this.lblEnemyHealth.Name = "lblEnemyHealth";
            this.lblEnemyHealth.Size = new System.Drawing.Size(258, 20);
            this.lblEnemyHealth.TabIndex = 1;
            this.lblEnemyHealth.Text = "                                                  Enemy";
            // 
            // lblEnemyHealthValue
            // 
            this.lblEnemyHealthValue.AutoSize = true;
            this.lblEnemyHealthValue.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblEnemyHealthValue.Location = new System.Drawing.Point(267, 0);
            this.lblEnemyHealthValue.Name = "lblEnemyHealthValue";
            this.lblEnemyHealthValue.Size = new System.Drawing.Size(0, 20);
            this.lblEnemyHealthValue.TabIndex = 2;
            // 
            // lblPlayerHealth
            // 
            this.lblPlayerHealth.AutoSize = true;
            this.lblPlayerHealth.ForeColor = System.Drawing.Color.GreenYellow;
            this.lblPlayerHealth.Location = new System.Drawing.Point(273, 0);
            this.lblPlayerHealth.Name = "lblPlayerHealth";
            this.lblPlayerHealth.Size = new System.Drawing.Size(248, 20);
            this.lblPlayerHealth.TabIndex = 3;
            this.lblPlayerHealth.Text = "                                                 Player";
            // 
            // lblPlayerHealthValue
            // 
            this.lblPlayerHealthValue.AutoSize = true;
            this.lblPlayerHealthValue.ForeColor = System.Drawing.Color.GreenYellow;
            this.lblPlayerHealthValue.Location = new System.Drawing.Point(527, 0);
            this.lblPlayerHealthValue.Name = "lblPlayerHealthValue";
            this.lblPlayerHealthValue.Size = new System.Drawing.Size(0, 20);
            this.lblPlayerHealthValue.TabIndex = 4;
            // 
            // frmDesktopGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1084, 501);
            this.Controls.Add(this.pbCanvas);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDesktopGame";
            this.Text = "Game";
            this.Load += new System.EventHandler(this.GameLoad);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyPressed);
            this.pbCanvas.ResumeLayout(false);
            this.pbCanvas.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.FlowLayoutPanel pbCanvas;
        private System.Windows.Forms.Label lblEnemyHealth;
        private System.Windows.Forms.Label lblEnemyHealthValue;
        private System.Windows.Forms.Label lblPlayerHealth;
        private System.Windows.Forms.Label lblPlayerHealthValue;
    }
}

