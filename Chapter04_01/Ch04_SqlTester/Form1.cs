using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Ch04_SqlTester
{
    public partial class frmSqlTester : Form
    {
        SqlConnection booksConnection;        
        public frmSqlTester()
        {
            InitializeComponent();
        }

        private void frmSqlTester_Load(object sender, EventArgs e)
        {
            // connect to books database
            booksConnection = new SqlConnection("Data Source=.\\SQLEXPRESS; AttachDbFilename=c:\\Users\\dms\\Documents\\Visual Studio 2017\\Projects\\LearnDatabase\\Working\\SQLBooksDB.mdf; Integrated Security=True; Connect Timeout=18; User Instance=True");
            booksConnection.Open(); 
        }

        private void frmSqlTester_FormClosing(object sender, FormClosingEventArgs e)
        {
            booksConnection.Close();
            booksConnection.Dispose();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            SqlCommand resultsCommand = null;
            SqlDataAdapter resultsAdapter = new SqlDataAdapter();
            DataTable resultsTable = new DataTable();

            try
            {
                // establish command object and data adapter
                resultsCommand = new SqlCommand(txtSqlTester.Text, booksConnection);
                resultsAdapter.SelectCommand = resultsCommand;
                resultsAdapter.Fill(resultsTable);

                // bind grid view to data table
                grdSqlTester.DataSource = resultsTable;
                lblRecords.Text = resultsTable.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error in Processing SQL", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
            resultsCommand.Dispose();
            resultsAdapter.Dispose();
            resultsTable.Dispose();
        }
    }
}
