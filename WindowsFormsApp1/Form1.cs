using System;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        // Database connection string

        public static string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MyTextEditor;Integrated Security=True;";
        public static string sqlCommandforGettingTextfromDB = "SELECT Text from Files WHERE File_Name=@filename";
        public static string sqlCommandforWritingTexttoDB = "INSERT INTO Files (File_Name,Text) VALUES(@filename,@text)";
        public static string textBoxText = "";

        public Form1()
        {
            InitializeComponent();
            saveFileDialog1.DefaultExt = ".txt";
            if (textBoxText.Length != 0)
                richTextBox1.Text = textBoxText;
        }

        public void TextRetrieved(string text)
        {
            richTextBox1.Text = text;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            fileName.Text = "No file choosen";
        }

        // Clearing the text box
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.ResetText();
        }

        // Closing the application
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void fromLocalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Text = File.ReadAllText(openFileDialog1.FileName);
                fileName.Text = openFileDialog1.FileName;
            }
        }

        private void saveToLocalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog1.FileName, richTextBox1.Text);
            }
        }

        private void fromDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Open_from_DB open_From_DB = new Open_from_DB(this);
            open_From_DB.Show();
        }

        private void saveToDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text != "")
            {
                Save_to_DB save_To_DB = new Save_to_DB();
                textBoxText = richTextBox1.Text;
                save_To_DB.Show();
            }
            else
            {
                MessageBox.Show("Enter the text", "Warning", MessageBoxButtons.OK);
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
