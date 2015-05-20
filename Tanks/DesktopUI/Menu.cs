using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TanksGame.DesktopUI
{
    public partial class frmMenu : Form
    {
        public bool StartClicked { get; set; }

        public frmMenu()
        {
            StartClicked = false;
            InitializeComponent();
        }

        private void StartGame(object sender, EventArgs e)
        {
            Program.Play = true;
            Close();
        }

        private void GameControls(object sender, EventArgs e)
        {
            btnStart.Visible = false;
            btnStart.Enabled = false;
            btnControls.Visible = false;
            btnControls.Enabled = false;
            btnAbout.Visible = false;
            btnAbout.Enabled = false;
            btnCtrlBack.Visible = true;
            btnCtrlBack.Enabled = true;
            pbxControls.Visible = true;
        }

        private void CtrlBack(object sender, EventArgs e)
        {
            btnCtrlBack.Visible = false;
            btnCtrlBack.Enabled = false;
            pbxControls.Visible = false;
            btnStart.Visible = true;
            btnStart.Enabled = true;
            btnControls.Visible = true;
            btnControls.Enabled = true;
            btnAbout.Visible = true;
            btnAbout.Enabled = true;
        }

        private void tbxAbout_TextChanged(object sender, EventArgs e)
        {

        }

        private void About(object sender, EventArgs e)
        {
            btnStart.Visible = false;
            btnStart.Enabled = false;
            btnControls.Visible = false;
            btnControls.Enabled = false;
            btnAbout.Visible = false;
            btnAbout.Enabled = false;
            btnAbtBack.Visible = true;
            btnAbtBack.Visible = true;
            lblAbout.Visible = true;
            tbxAbout.Visible = true;
            pbxAbout.Visible = true;
        }

        private void AbtBack(object sender, EventArgs e)
        {
            btnAbtBack.Visible = false;
            btnAbtBack.Visible = false;
            lblAbout.Visible = false;
            tbxAbout.Visible = false;
            pbxAbout.Visible = false;
            btnStart.Visible = true;
            btnStart.Enabled = true;
            btnControls.Visible = true;
            btnControls.Enabled = true;
            btnAbout.Visible = true;
            btnAbout.Enabled = true;
        }
    }
}
