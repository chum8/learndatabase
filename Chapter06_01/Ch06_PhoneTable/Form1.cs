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

namespace Ch06_PhoneTable
{
    public partial class frmPhoneDB : Form
    {
        SqlConnection phoneConnection;
        SqlCommand phoneCommand;
        SqlDataAdapter phoneAdapter;
        DataTable phoneTable;
        CurrencyManager phoneManager;
        string myState;
        int myBookmark;
        public frmPhoneDB()
        {
            InitializeComponent();
        }

        private void frmPhoneDB_Load(object sender, EventArgs e)
        {
            // connect to the phone database
            phoneConnection = new SqlConnection("Data Source=.\\SQLEXPRESS; User Instance=True; Integrated Security=True; Connect Timeout=18; AttachDbFilename=C:\\Users\\dms\\Documents\\Visual Studio 2017\\Projects\\LearnDatabase\\Working\\SQLPhoneDB.mdf");
            phoneConnection.Open();

            // establish command object
            phoneCommand = new SqlCommand("SELECT * from phoneTable ORDER BY ContactName", phoneConnection);

            // establish data adapter/table
            phoneAdapter = new SqlDataAdapter();
            phoneAdapter.SelectCommand = phoneCommand;
            phoneTable = new DataTable();
            phoneAdapter.Fill(phoneTable);

            // bind controls to data table
            txtID.DataBindings.Add("Text", phoneTable, "ContactID");
            txtName.DataBindings.Add("Text", phoneTable, "ContactName");
            txtNumber.DataBindings.Add("Text", phoneTable, "ContactNumber");

            // establish currency manager
            phoneManager = (CurrencyManager)this.BindingContext[phoneTable];

            // set state
            SetState("View");
        }

        private void frmPhoneDB_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // save the updated phone table
                SqlCommandBuilder phoneAdapterCommands = new SqlCommandBuilder(phoneAdapter);
                phoneAdapter.Update(phoneTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving database to file:\r\n" + ex.Message, "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // close the connection
            phoneConnection.Close();
            
            // dispose of the objects
            phoneConnection.Dispose();
            phoneCommand.Dispose();
            phoneAdapter.Dispose();
            phoneTable.Dispose();
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            phoneManager.Position = phoneManager.Count - 1;
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            phoneManager.Position = 0;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            phoneManager.Position++;
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            phoneManager.Position--;
        }

        private void SetState(string appState)
        {
            myState = appState;
            switch(appState)
            {
                case "View":
                    btnFirst.Enabled = true;
                    btnPrevious.Enabled = true;
                    btnNext.Enabled = true;
                    btnLast.Enabled = true;
                    btnEdit.Enabled = true;
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                    btnDelete.Enabled = true;
                    btnAdd.Enabled = true;
                    txtID.BackColor = Color.White;
                    txtID.ForeColor = Color.Black;
                    txtID.Enabled = false;
                    txtName.ReadOnly = true;
                    txtName.Enabled = false;
                    txtNumber.ReadOnly = true;
                    txtNumber.Enabled = false;
                    break;

                default: // "Edit" mode, "Add" mode
                    btnFirst.Enabled = false;
                    btnPrevious.Enabled = false;
                    btnNext.Enabled = false;
                    btnLast.Enabled = false;
                    btnEdit.Enabled = false;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    btnDelete.Enabled = false;
                    btnAdd.Enabled = false;
                    txtID.BackColor = Color.Pink;
                    txtID.ForeColor = Color.Black;
                    txtID.Enabled = false;
                    txtName.ReadOnly = false;
                    txtName.Enabled = true;
                    txtNumber.ReadOnly = false;
                    txtNumber.Enabled = true;
                    break;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            SetState("Edit");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string savedName = txtName.Text;
            int savedRow;
            phoneManager.EndCurrentEdit();
            phoneTable.DefaultView.Sort = "ContactName";
            savedRow = phoneTable.DefaultView.Find(savedName);
            phoneManager.Position = savedRow;
            SetState("View");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            phoneManager.CancelCurrentEdit();
            if (myState.Equals("Add"))
            {
                phoneManager.Position = myBookmark;
            }
            SetState("View");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            myBookmark = phoneManager.Position;
            SetState("Add");
            phoneManager.AddNew();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this record?  You will not be able to recover it!", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                phoneManager.RemoveAt(phoneManager.Position);
            }
            SetState("View");
        }
    }
}