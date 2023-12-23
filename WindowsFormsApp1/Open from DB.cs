using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Open_from_DB : Form
    {
        private Form1 _form1;

        public Open_from_DB(Form1 form1)
        {
            _form1 = form1;
            InitializeComponent();
        }


        private void button1_Click(object sender, System.EventArgs e)
        {
            if (fileName.Text != "")
            {
                using (SqlConnection connection = new SqlConnection(Form1.connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(Form1.sqlCommandforGettingTextfromDB, connection))
                    {
                        command.Parameters.AddWithValue("@filename", fileName.Text);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string retrievedText = reader.GetString(0);
                                _form1.TextRetrieved(retrievedText);
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("No text found for the specified filename", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("File Name or Database connection string must not be an empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
