using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Windows.Forms;

namespace Workflow.Diagnosis
{
    public partial class frmMain : Form {

        protected ServiceController service = new ServiceController("Ticket Email Service");

        public frmMain() {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e) {
            if (service.Status == ServiceControllerStatus.Running) {
                btnStart.Text = "Stop";
                btnPull.Enabled = true;
            } else {
                btnStart.Text = "Start";
                btnPull.Enabled = false;
            }
        }

        private void btnStart_Click(object sender, EventArgs e) {

            if (MessageBox.Show(string.Format("Would you like to {0} service?", btnStart.Text.ToLower()), "Confirm!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            if(btnStart.Text == "Stop") {
                service.Stop();
                btnStart.Text = "Start";
                btnPull.Enabled = false;
            } else {
                service.Start();
                btnStart.Text = "Stop";
                btnPull.Enabled = true;
            }
        }

        private void btnPull_Click(object sender, EventArgs e) {
            if (MessageBox.Show("Would you like to pull new email?", "Confirm!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            service.ExecuteCommand(128);
        }

        private void btnRefresh_Click(object sender, EventArgs e) {
            LoadLogs();
        }

        private void LoadLogs() {
            lstLog.Items.Clear();
            string content = File.ReadAllText(Application.StartupPath + "\\logs\\logs.txt");
            List<string> logs = content.Split('\n').Reverse().ToList();
            if (logs != null && logs.Count > 0) {
                logs.ForEach(x => lstLog.Items.Add(x));
            }
        }
    }
}
