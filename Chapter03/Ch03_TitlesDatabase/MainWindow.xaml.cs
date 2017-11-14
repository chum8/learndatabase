using System.Windows;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace Ch03_TitlesDatabase
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection booksConnection;
        SqlCommand titlesCommand;
        SqlDataAdapter titlesAdapter;
        DataTable titlesTable;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void frmTitles_Loaded(object sender, RoutedEventArgs e)
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
            
           
            // close the connection
            booksConnection.Close();

            // dispose of the connection object
            booksConnection.Dispose();
            titlesCommand.Dispose();
            titlesAdapter.Dispose();
            titlesTable.Dispose();
        }

    }
}
