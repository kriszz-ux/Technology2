using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace technology2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            // Example: hard-coded login
            if (username == "admin" && password == "12345")
            {
                // Create and show Form2
                Form2 dashboard = new Form2();
                dashboard.Show();   // Show Form2

                this.Hide();        // Hide Form1 (login form)
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
