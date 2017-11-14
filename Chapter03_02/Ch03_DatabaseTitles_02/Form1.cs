using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
namespace Ch03_DatabaseTitles_02
{
    public partial class frmTitles : Form
    {
        SqlConnection booksConnection;
        SqlCommand titlesCommand;
        SqlDataAdapter titlesAdapter;
        DataTable titlesTable;
        CurrencyManager titlesManager;
        public frmTitles()
        {
            InitializeComponent();
        }

        private void frmTitles_Load(object sender, EventArgs e)
        {
            // connect to books database
            booksConnection = new SqlConnection("Data Source=localhost\\SQLEXPRESS; AttachDbFilename=c:\\Users\\dms\\Documents\\Visual Studio 2017\\Projects\\LearnDatabase\\Chapter03\\SQLBooksDB.mdf; Integrated Security=True; Connect Timeout=25; User Instance=True");

            // open the connection
            booksConnection.Open();

            // establish command object
            titlesCommand = new SqlCommand("Select * from Titles", booksConnection);

            // establish data adapter/data table
            titlesAdapter = new SqlDataAdapter();
            titlesAdapter.SelectCommand = titlesCommand;
            titlesTable = new DataTable();
            titlesAdapter.Fill(titlesTable);

            // bind controls to data table
            txtTitle.DataBindings.Add(new Binding("Text", titlesTable, "Title"));
            txtYear.DataBindings.Add(new Binding("Text", titlesTable, "Year_Published"));
            txtISBN.DataBindings.Add(new Binding("Text", titlesTable, "ISBN"));
            txtPublisher.DataBindings.Add(new Binding("Text", titlesTable, "PubID"));

            // establish currency manager
            titlesManager = (CurrencyManager)
                BindingContext[titlesTable];
             
            // close the connection
            booksConnection.Close();

            // dispose of the connection object
            booksConnection.Dispose();
            titlesCommand.Dispose();
            titlesAdapter.Dispose();
            titlesTable.Dispose();
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            titlesManager.Position = 0;
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            titlesManager.Position--;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            titlesManager.Position++;
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            titlesManager.Position = titlesManager.Count - 1;
        }
    }
}
