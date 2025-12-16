using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Technology_Project;

namespace technology2
{
    public partial class Form2 : Form
    {
        Class2 db = new Class2();
        public Form2()
        {
            InitializeComponent();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Form1 login = new Form1();
            login.Show();
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            db.AddPatient(
        txtName.Text,
        txtAge.Text,
        txtGender.Text,
        txtContact.Text,
        txtDiagnosis.Text,
        dtpConsultationDate.Value,
        txtAddress.Text
    );

            MessageBox.Show("Patient added successfully!");
            ClearFields();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Check if a row is selected
            if (dgvPatients.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a patient to update.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Exit the method if no row is selected
            }

            // Fetch selected row
            DataGridViewRow selectedRow = dgvPatients.SelectedRows[0];

            // Validate and convert age
            int age;
            if (!int.TryParse(txtAge.Text, out age))
            {
                MessageBox.Show("Please enter a valid age.");
                return; // Exit if age is invalid
            }

            // Validate and convert consultation date
            DateTime consultationDate;
            if (!DateTime.TryParse(dtpConsultationDate.Value.ToString(), out consultationDate))
            {
                MessageBox.Show("Please enter a valid consultation date.");
                return; // Exit if the date is invalid
            }

            // Get other values from textboxes
            string name = txtName.Text;
            string gender = txtGender.Text;
            string contact = txtContact.Text;
            string diagnosis = txtDiagnosis.Text;
            string address = txtAddress.Text;

            // SQL query to update the patient in the database
            string query = "UPDATE records SET name = @name, age = @age, gender = @gender, contact = @contact, " +
               "diagnosis = @diagnosis, address = @address, consultation_date = @consultationDate " +
               "WHERE patient_id = @patientId";


            using (MySqlConnection con = new DBConnection().Open())
            {
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@patientId", Convert.ToInt32(selectedRow.Cells["patient_id"].Value));
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@age", age);
                cmd.Parameters.AddWithValue("@gender", gender);
                cmd.Parameters.AddWithValue("@contact", contact);
                cmd.Parameters.AddWithValue("@diagnosis", diagnosis);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@consultationDate", consultationDate);

                cmd.ExecuteNonQuery(); // Execute the update query

                MessageBox.Show("Patient updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // Reload the patients to reflect the changes
            LoadPatients();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }
        private void ClearFields()
        {
            txtPatientID.Clear();
            txtName.Clear();
            txtAge.Clear();
            txtGender.Clear();
            txtContact.Clear();
            txtAddress.Clear();
            txtDiagnosis.Clear();
        }

        private void btnLogout_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            dgvPatients.Visible = false;
        }

        private void LoadPatients()
        {
            string query = "SELECT * FROM records";

            using (MySqlConnection con = new DBConnection().Open())
            {
                MySqlDataAdapter da = new MySqlDataAdapter(query, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvPatients.DataSource = dt;
            }
            
        }

        private void dgvPatients_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvPatients.Rows[e.RowIndex];

                txtPatientID.Text = row.Cells["patient_id"].Value.ToString();
                txtName.Text = row.Cells["name"].Value.ToString();
                txtAge.Text = row.Cells["age"].Value.ToString();
                txtGender.Text = row.Cells["gender"].Value.ToString();
                txtContact.Text = row.Cells["contact"].Value.ToString();
                txtDiagnosis.Text = row.Cells["diagnosis"].Value.ToString();
                dtpConsultationDate.Value = Convert.ToDateTime(row.Cells["consultation_date"].Value);
                txtAddress.Text = row.Cells["address"].Value.ToString();
            }
        }

        private void textBox3_Click(object sender, EventArgs e)
        {
            dgvPatients.Visible = true;
            LoadPatients();
        }


        private void btnPatient(object sender, EventArgs e)
        {
            dgvPatients.Visible = true;
            LoadPatients();
        }

        private void dgvPatients_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim();

            string query = @"SELECT * FROM records
                     WHERE name LIKE @keyword
                     OR patient_id LIKE @keyword";

            using (MySqlConnection con = new DBConnection().Open())
            {
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvPatients.Visible = true;
                dgvPatients.DataSource = dt;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
