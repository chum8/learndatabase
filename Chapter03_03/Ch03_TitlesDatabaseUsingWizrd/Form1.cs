using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ch03_TitlesDatabaseUsingWizrd
{
    public partial class frmTitles : Form
    {
        public frmTitles()
        {
            InitializeComponent();
        }

        private void titlesBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.titlesBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.sQLBooksDBDataSet);

        }

        private void frmTitles_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'sQLBooksDBDataSet.Titles' table. You can move, or remove it, as needed.
            this.titlesTableAdapter.Fill(this.sQLBooksDBDataSet.Titles);

        }
    }
}
