﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockManagementSystemApp.UI
{
    public partial class ProgressForm : Form
    {
        public ProgressForm()
        {
            InitializeComponent();
        }

        private void ProgressForm_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            LoginForm frm = new LoginForm();
            progressBar1.Visible = true;

            this.progressBar1.Value = this.progressBar1.Value + 2;
            if (this.progressBar1.Value == 10)
            {
                label3.Text = "Reading modules..";
            }
            else if (this.progressBar1.Value == 20)
            {
                label3.Text = "Turning on modules.";
            }
            else if (this.progressBar1.Value == 40)
            {
                label3.Text = "Starting modules..";
            }
            else if (this.progressBar1.Value == 60)
            {
                label3.Text = "Loading modules..";
            }
            else if (this.progressBar1.Value == 80)
            {
                label3.Text = "Done Loading modules..";
            }
            else if (this.progressBar1.Value == 100)
            {
                frm.Show();
                timer1.Enabled = false;
                this.Hide();
                timer1.Stop();
            }
        }
    }
}
