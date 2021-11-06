//Michael Leal
//Database program
//itse 2353
//program will get info from a database and display it in program

using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Database_Program
{
    public partial class frmMain : Form
    {
        private OleDbDataAdapter customerDataAdapter;
        private DataSet customerDataSet;
        private OleDbCommand dbCommand;
        private OleDbConnection dbConnection;      

        public frmMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.customerTableTableAdapter.Fill(this.customersDataSet.CustomerTable);
            infoHide();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        private void infoHide()
        {
            label1.Hide();
            label2.Hide();
            label3.Hide();
            dataGridView1.Hide();
            btnShow.Hide();
            txtNumber.Hide();
            txtName.Hide();
            txtLocation.Hide();
        }

        private void infoShow()
        {
            label1.Show();
            label2.Show();
            label3.Show();
            txtNumber.Show();
            txtName.Show();
            txtLocation.Show();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            //display dataGridView1 box
            dataGridView1.Show();
            //display bthShow and hide btnLoad from user
            btnLoad.Hide();
            btnShow.Show();

            //display records from access file
            try
            {
                dbConnection = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0;" + "Data Source = Customers.accdb");
                dbCommand = new OleDbCommand();
                dbCommand.CommandText = "SELECT * FROM CustomerTable ORDER BY customerLastName ASC, customerFirstName ASC;";

                dbCommand.Connection = dbConnection;

                //create the DataAdapter and set the select command
                customerDataAdapter = new OleDbDataAdapter();
                customerDataAdapter.SelectCommand = dbCommand;

                //create and fill the data set
                customerDataSet = new DataSet();
                customerDataAdapter.Fill(customerDataSet, "CustomerTable");

                dataGridView1.DataSource = customerDataSet;
                dataGridView1.DataMember = "CustomerTable";
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            //display all infromation from selected record. 
            infoShow();

            try
            {
                DataGridViewRow dataInfoGrid = dataGridView1.SelectedRows[0];
                txtNumber.Text = dataInfoGrid.Cells[0].Value.ToString();
                txtName.Text = dataInfoGrid.Cells[1].Value.ToString() + " " + dataInfoGrid.Cells[2].Value.ToString();

                switch (txtLocation.Text = dataInfoGrid.Cells[3].Value.ToString())
                {
                    case "N":
                        txtLocation.Text = "North";
                        break;
                    case "S":
                        txtLocation.Text = "South";
                        break;
                    case "E":
                        txtLocation.Text = "East";
                        break;
                    case "W":
                        txtLocation.Text = "West";
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
