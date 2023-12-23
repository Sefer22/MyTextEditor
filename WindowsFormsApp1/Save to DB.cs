using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Save_to_DB : Form
    {
        public Save_to_DB()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (fileName.Text != "")
            {
                using (SqlConnection connection = new SqlConnection(Form1.connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(Form1.sqlCommandforWritingTexttoDB, connection))
                    {
                        command.Parameters.AddWithValue("@text", Form1.textBoxText);
                        command.Parameters.AddWithValue("@filename", fileName.Text);

                        try
                        {
                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Text saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Error saving text.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (Exception ex)
                        {
                            // Handle database errors
                            MessageBox.Show("Error saving text: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
