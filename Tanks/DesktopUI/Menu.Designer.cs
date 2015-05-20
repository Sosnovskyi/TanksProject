using System;

namespace TanksGame.DesktopUI
{
    partial class frmMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMenu));
            this.btnStart = new System.Windows.Forms.Button();
            this.pbxControls = new System.Windows.Forms.PictureBox();
            this.btnCtrlBack = new System.Windows.Forms.Button();
            this.pbxAbout = new System.Windows.Forms.PictureBox();
            this.btnAbtBack = new System.Windows.Forms.Button();
            this.tbxAbout = new System.Windows.Forms.RichTextBox();
            this.lblAbout = new System.Windows.Forms.Label();
            this.btnControls = new System.Windows.Forms.Button();
            this.btnAbout = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbxControls)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxAbout)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnStart.BackColor = System.Drawing.Color.DarkOrange;
            this.btnStart.ForeColor = System.Drawing.Color.Black;
            this.btnStart.Location = new System.Drawing.Point(58, 33);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(173, 28);
            this.btnStart.TabIndex = 6;
            this.btnStart.Text = "Start Game";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.StartGame);
            // 
            // pbxControls
            // 
            this.pbxControls.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pbxControls.Image = ((System.Drawing.Image)(resources.GetObject("pbxControls.Image")));
            this.pbxControls.Location = new System.Drawing.Point(-1, -1);
            this.pbxControls.Name = "pbxControls";
            this.pbxControls.Size = new System.Drawing.Size(291, 175);
            this.pbxControls.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbxControls.TabIndex = 9;
            this.pbxControls.TabStop = false;
            this.pbxControls.Visible = false;
            // 
            // btnCtrlBack
            // 
            this.btnCtrlBack.Enabled = false;
            this.btnCtrlBack.Location = new System.Drawing.Point(-2, -1);
            this.btnCtrlBack.Name = "btnCtrlBack";
            this.btnCtrlBack.Size = new System.Drawing.Size(75, 23);
            this.btnCtrlBack.TabIndex = 10;
            this.btnCtrlBack.Text = "Back";
            this.btnCtrlBack.UseVisualStyleBackColor = true;
            this.btnCtrlBack.Visible = false;
            this.btnCtrlBack.Click += new System.EventHandler(this.CtrlBack);
            // 
            // pbxAbout
            // 
            this.pbxAbout.BackColor = System.Drawing.Color.LightYellow;
            this.pbxAbout.Location = new System.Drawing.Point(-1, -1);
            this.pbxAbout.Name = "pbxAbout";
            this.pbxAbout.Size = new System.Drawing.Size(291, 175);
            this.pbxAbout.TabIndex = 11;
            this.pbxAbout.TabStop = false;
            this.pbxAbout.Visible = false;
            // 
            // btnAbtBack
            // 
            this.btnAbtBack.Location = new System.Drawing.Point(-1, 2);
            this.btnAbtBack.Name = "btnAbtBack";
            this.btnAbtBack.Size = new System.Drawing.Size(75, 23);
            this.btnAbtBack.TabIndex = 12;
            this.btnAbtBack.Text = "Back";
            this.btnAbtBack.UseVisualStyleBackColor = true;
            this.btnAbtBack.Visible = false;
            this.btnAbtBack.Click += new System.EventHandler(this.AbtBack);
            // 
            // tbxAbout
            // 
            this.tbxAbout.BackColor = System.Drawing.Color.LightYellow;
            this.tbxAbout.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbxAbout.Location = new System.Drawing.Point(-1, 28);
            this.tbxAbout.Name = "tbxAbout";
            this.tbxAbout.Size = new System.Drawing.Size(290, 145);
            this.tbxAbout.TabIndex = 13;
            this.tbxAbout.Text = resources.GetString("tbxAbout.Text");
            this.tbxAbout.Visible = false;
            this.tbxAbout.TextChanged += new System.EventHandler(this.tbxAbout_TextChanged);
            // 
            // lblAbout
            // 
            this.lblAbout.AutoSize = true;
            this.lblAbout.BackColor = System.Drawing.Color.LightYellow;
            this.lblAbout.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblAbout.Location = new System.Drawing.Point(122, 5);
            this.lblAbout.Name = "lblAbout";
            this.lblAbout.Size = new System.Drawing.Size(52, 20);
            this.lblAbout.TabIndex = 15;
            this.lblAbout.Text = "About";
            this.lblAbout.Visible = false;
            // 
            // btnControls
            // 
            this.btnControls.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnControls.BackColor = System.Drawing.Color.DarkOrange;
            this.btnControls.ForeColor = System.Drawing.Color.Black;
            this.btnControls.Location = new System.Drawing.Point(58, 75);
            this.btnControls.Name = "btnControls";
            this.btnControls.Size = new System.Drawing.Size(173, 28);
            this.btnControls.TabIndex = 7;
            this.btnControls.Text = "Controls";
            this.btnControls.UseVisualStyleBackColor = false;
            this.btnControls.Click += new System.EventHandler(this.GameControls);
            // 
            // btnAbout
            // 
            this.btnAbout.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAbout.BackColor = System.Drawing.Color.DarkOrange;
            this.btnAbout.ForeColor = System.Drawing.Color.Black;
            this.btnAbout.Location = new System.Drawing.Point(58, 118);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(173, 28);
            this.btnAbout.TabIndex = 16;
            this.btnAbout.Text = "About";
            this.btnAbout.UseVisualStyleBackColor = false;
            this.btnAbout.Click += new System.EventHandler(this.About);
            // 
            // frmMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(289, 174);
            this.Controls.Add(this.btnAbout);
            this.Controls.Add(this.lblAbout);
            this.Controls.Add(this.tbxAbout);
            this.Controls.Add(this.btnAbtBack);
            this.Controls.Add(this.pbxAbout);
            this.Controls.Add(this.btnCtrlBack);
            this.Controls.Add(this.pbxControls);
            this.Controls.Add(this.btnControls);
            this.Controls.Add(this.btnStart);
            this.Name = "frmMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tanks";
            ((System.ComponentModel.ISupportInitialize)(this.pbxControls)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxAbout)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.PictureBox pbxControls;
        private System.Windows.Forms.Button btnCtrlBack;
        private System.Windows.Forms.PictureBox pbxAbout;
        private System.Windows.Forms.Button btnAbtBack;
        private System.Windows.Forms.RichTextBox tbxAbout;
        private System.Windows.Forms.Label lblAbout;
        private System.Windows.Forms.Button btnControls;
        private System.Windows.Forms.Button btnAbout;

    }
}