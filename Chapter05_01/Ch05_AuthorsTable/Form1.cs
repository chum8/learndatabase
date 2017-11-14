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
using System.Text.RegularExpressions;

namespace Ch05_AuthorsTable
{
    public partial class frmPublishers : Form
    {
        // regular expressions used in form validation
        Regex regexAuthorYearString = new Regex(@"^[0-9\b\d]$");
        Regex regexTxtCurrentPosition = new Regex(@"^\d+$");

        // year variables used in several functions
        int currentYear = DateTime.Now.Year;
        int minimumYear = DateTime.Now.Year - (DateTime.Now.Year / 7);

        // sql connection variables
        SqlConnection booksConnection;
        SqlCommand publishersCommand;
        SqlDataAdapter publishersAdapter;
        DataTable publishersTable;
        CurrencyManager publishersManager;
        public frmPublishers()
        {
            InitializeComponent();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (publishersManager.Position == 0)
            {
                Console.Beep();
            }
            publishersManager.Position--;
            updateTxtCurrentPosition();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (publishersManager.Position == publishersManager.Count - 1)
            {
                Console.Beep();
            }
            publishersManager.Position++;
            updateTxtCurrentPosition();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            SetState("Edit");
            txtPubName.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            SetState("View");
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                SetState("Add");
            }
            catch (Exception ex)
            {
                showErrorMessage(ex.Message, "Error adding record.");
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult response;
            response = MessageBox.Show("Are you sure you want to save this record?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (response == DialogResult.No)
            {
                return;
            }
            else if (!ValidateData())
            {
                return;
            }
            else
            {
                try
                {
                    MessageBox.Show("Record saved.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    SetState("View");
                }
                catch (Exception ex)
                {
                    showErrorMessage(ex.Message, "Error adding record.");
                }                
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult response;
            response = MessageBox.Show("Are you sure you want to delete this record?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (response == DialogResult.No)
            {
                return;
            }
            try
            {

            }
            catch (Exception ex)
            {
                showErrorMessage(ex.Message, "Error deleting record!"); 
            }
        }

        private void btnDone_Click(object sender, EventArgs e)
        {

        }
        private void SetState(string appState)
        {
            switch (appState)
            {
                case "View":

                    // set text field states
                    txtPubID.BackColor = Color.White;
                    txtPubID.ForeColor = Color.Black;
                    txtPubName.Enabled = false;
                    txtPubName.ReadOnly = true;
                    txtCompanyName.Enabled = false;
                    txtCompanyName.ReadOnly = true;
                    txtPubAddress.Enabled = false;
                    txtPubAddress.ReadOnly = true;
                    txtPubCity.Enabled = false;
                    txtPubCity.ReadOnly = true;
                    txtPubState.Enabled = false;
                    txtPubState.ReadOnly = true;
                    txtPubZip.Enabled = false;
                    txtPubZip.ReadOnly = true;
                    txtPubTelephone.Enabled = false;
                    txtPubTelephone.ReadOnly = true;
                    txtPubFAX.Enabled = false;
                    txtPubFAX.ReadOnly = true;
                    txtPubComments.Enabled = false;
                    txtPubComments.ReadOnly = true;
                    
                    // set button states 
                    btnPrevious.Enabled = true;
                    btnNext.Enabled = true;
                    btnAddNew.Enabled = true;
                    btnSave.Enabled = false;
                    btnCancel.Enabled = false;
                    btnEdit.Enabled = true;
                    btnDelete.Enabled = true;

                    // set focus
                    txtPubName.Focus();
                    break;

                default: // Add or Edit if not View
 
                    // set text field states
                    txtPubID.BackColor = Color.LightPink;
                    txtPubID.ForeColor = Color.White;
                    txtPubName.Enabled = true;
                    txtPubName.ReadOnly = false;
                    txtCompanyName.Enabled = true;
                    txtCompanyName.ReadOnly = false;
                    txtPubAddress.Enabled = true;
                    txtPubAddress.ReadOnly = false;
                    txtPubCity.Enabled = true;
                    txtPubCity.ReadOnly = false;
                    txtPubState.Enabled = true;
                    txtPubState.ReadOnly = false;
                    txtPubZip.Enabled = true;
                    txtPubZip.ReadOnly = false;
                    txtPubTelephone.Enabled = true;
                    txtPubTelephone.ReadOnly = false;
                    txtPubFAX.Enabled = true;
                    txtPubFAX.ReadOnly = false;
                    txtPubComments.Enabled = true;
                    txtPubComments.ReadOnly = false;
                    
                    // set button states 
                    btnPrevious.Enabled = false;
                    btnNext.Enabled = false;
                    btnAddNew.Enabled = false;
                    btnSave.Enabled = true;
                    btnCancel.Enabled = true;
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;

                    // set focus
                    txtPubName.Focus();
                    break;
            }
        }

        private void updateTotalCountLabel()
        {
            lblTotalCount.Text = "\\ " + Convert.ToString(publishersManager.Count - 1);
        }

        private void updateTxtCurrentPosition()
        {
            txtCurrentPosition.Text = Convert.ToString(publishersManager.Position);
        }

        private void txtCurrentPosition_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                string t = txtCurrentPosition.Text;
                if (regexTxtCurrentPosition.IsMatch(t))
                {
                    // if user input a valid number
                    // first check and make sure it's in range
                    int p = Convert.ToInt32(t) >= publishersManager.Count ? publishersManager.Count : Convert.ToInt32(t);

                    // then set counter to that position
                    publishersManager.Position = p;
                    updateTxtCurrentPosition();
                }
                else
                {
                    // restore text counter to last current position
                    int p = publishersManager.Position;
                    updateTxtCurrentPosition();
                }
            }
        }

        private void txtInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!regexAuthorYearString.IsMatch(Convert.ToString(e.KeyChar)))
            {
                e.Handled = true;
                Console.Beep();
            }
            TextBox whichBox = (TextBox)sender;
            if ((int)e.KeyChar == 13)
            {
                switch (whichBox.Name)
                {
                    case "txtPubName":
                        txtCompanyName.Focus();
                        break;
                    case "txtCompanyName":
                        txtPubAddress.Focus();
                        break;
                    case "txtPubAddress":
                        txtPubCity.Focus();
                        break;
                    case "txtPubCity":
                        txtPubState.Focus();
                        break;
                    case "txtPubState":
                        txtPubZip.Focus();
                        break;
                    case "txtPubZip":
                        txtPubTelephone.Focus();
                        break;
                    case "txtPubTelephone":
                        txtPubFAX.Focus();
                        break;
                    case "txtPubFAX":
                        txtPubComments.Focus();
                        break;
                    case "txtPubComments":
                        btnSave.Focus();
                        break;
                }
            }
        }
        private bool ValidateData()
        {
            string message = "";
            // int inputYear; // vestige
            bool allOK = true;

            // check for name
            if (txtPubName.Text.Trim().Equals(""))
            {
                message = "You must enter a Publisher Name." + "\r\n";
                txtPubName.Focus();
                allOK = false;
            }

            // check length and range on year born
            /* old code, vestige of when there was a year input box, left in case such a box is implemented again
            
            if (!txtCompanyName.Text.Trim().Equals(""))
            {
                inputYear = Convert.ToInt32(txtCompanyName.Text);
                if (inputYear > currentYear || inputYear < minimumYear)
                {
                    message += "Year born must be between " + minimumYear.ToString() + " and " + currentYear.ToString();
                    txtCompanyName.Focus();
                    allOK = false;
                }
            }
            */
            if (!allOK)
            {
                showErrorMessage(message, "Validation Error");
            }
            return allOK;
        }

        private void showErrorMessage(string message = "There was an error", string winMessage = "Error Detected!")
        {
            MessageBox.Show(message, winMessage, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void frmPublishers_Load(object sender, EventArgs e)
        {
            try
            {
                // connect to books database
                booksConnection = new SqlConnection("Data Source=.\\SQLExpress; AttachDbFilename=C:\\Users\\dms\\Documents\\Visual Studio 2017\\Projects\\LearnDatabase\\Working\\SQLBooksDB.mdf; User Instance=True; Connect Timeout=18; Integrated Security=True;");
                booksConnection.Open();

                // establish command object
                publishersCommand = new SqlCommand("SELECT * FROM Publishers ORDER BY Name", booksConnection);

                // establish data adapter/data table
                publishersAdapter = new SqlDataAdapter();
                publishersAdapter.SelectCommand = publishersCommand;
                publishersTable = new DataTable();
                publishersAdapter.Fill(publishersTable);

                // bind controls to data table
                txtPubID.DataBindings.Add("Text", publishersTable, "PubID");
                txtPubName.DataBindings.Add("Text", publishersTable, "Name");
                txtCompanyName.DataBindings.Add("Text", publishersTable, "Company_Name");
                txtPubAddress.DataBindings.Add("Text", publishersTable, "Address");
                txtPubCity.DataBindings.Add("Text", publishersTable, "City");
                txtPubState.DataBindings.Add("Text", publishersTable, "State");
                txtPubZip.DataBindings.Add("Text", publishersTable, "Zip");
                txtPubTelephone.DataBindings.Add("Text", publishersTable, "Telephone");
                txtPubFAX.DataBindings.Add("Text", publishersTable, "FAX");
                txtPubComments.DataBindings.Add("Text", publishersTable, "Comments");

                // establish currency manager
                publishersManager = (CurrencyManager)this.BindingContext[publishersTable];

                // update total count label
                updateTotalCountLabel();
                updateTxtCurrentPosition();

                // set state
                SetState("View");
            }
            catch (Exception ex)
            {
                showErrorMessage(ex.Message, "Error establishing Publishers table!");
                return; 
            }

        }

        private void frmPublishers_FormClosing(object sender, FormClosingEventArgs e)
        {
            // close the connection
            booksConnection.Close();

            // dispose of the objects
            booksConnection.Dispose();
            publishersCommand.Dispose();
            publishersAdapter.Dispose();
            publishersTable.Dispose();
        }
    }
}
