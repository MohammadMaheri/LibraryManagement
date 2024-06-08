using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;



namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private List<Book> books = new List<Book>();
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {

                Book newBook = new Book(txtTitle.Text, txtAuthor.Text, int.Parse(txtYear.Text));

                // افزودن کتاب به لیست داده‌ها
                books.Add(newBook);

                //تابع ها را فرا خوانی کردم
                UpdateDataGridView();
                ClearTextFields();
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter a valid year.");
            }
        }

        private void UpdateDataGridView()
        {
            dataGridViewBooks.DataSource = null;
            dataGridViewBooks.DataSource = books;
            dataGridViewBooks.Columns["IsBorrowed"].HeaderText = "Borrowed";
            dataGridViewBooks.Columns["Borrower"].HeaderText = "Borrower";
        }

        private void ClearTextFields()
        {
            txtTitle.Clear();
            txtAuthor.Clear();
            txtYear.Clear();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewBooks.CurrentRow != null)
                {
                    Book bookToRemove = (Book)dataGridViewBooks.CurrentRow.DataBoundItem;
                    books.Remove(bookToRemove);
                    UpdateDataGridView();
                    MessageBox.Show($"Book '{bookToRemove.Title}' has been removed.");
                }
                else
                {
                    MessageBox.Show("No book is selected for removal.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while removing the book: {ex.Message}");
            }
        }
        private void SaveDataToCSV()
        {
            try
            {
                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "books.csv");
                using (StreamWriter writer = new StreamWriter(filePath, false))
                {
                    writer.WriteLine("Year,Title,Author,IsBorrowed,Borrower");
                    foreach (var book in books)
                    {
                        writer.WriteLine($"{book.Year},{book.Title},{book.Author},{book.IsBorrowed},{book.Borrower}");
                    }
                }
                MessageBox.Show("Data saved successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving the books: {ex.Message}");
            }
        }

        private void LoadDataFromCSV()
        {
            try
            {
                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "books.csv");
                if (File.Exists(filePath))
                {
                    books.Clear();
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        reader.ReadLine();
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            var fields = line.Split(',');
                            string title = fields[1];
                            string author = fields[2];
                            int year = int.Parse(fields[0]);
                            bool isBorrowed = bool.Parse(fields[3]);
                            string borrower = fields[4];
                            books.Add(new Book(title, author, year, isBorrowed, borrower));
                        }
                    }
                    UpdateDataGridView();
                    MessageBox.Show("Books loaded successfully.");
                }
                else
                {
                    MessageBox.Show("No saved books found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading the books: {ex.Message}");
            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveDataToCSV();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadDataFromCSV();
        }

        private void btnBorrow_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewBooks.CurrentRow != null)
                {
                    Book selectedBook = (Book)dataGridViewBooks.CurrentRow.DataBoundItem;
                    if (!selectedBook.IsBorrowed)
                    {
                        selectedBook.IsBorrowed = true;
                        selectedBook.Borrower = txtBorrower.Text;
                        UpdateDataGridView();
                        MessageBox.Show($"Book '{selectedBook.Title}' has been borrowed by {selectedBook.Borrower}.");
                    }
                    else
                    {
                        MessageBox.Show($"Book '{selectedBook.Title}' is already borrowed by {selectedBook.Borrower}.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while borrowing the book: {ex.Message}");
            }
        }

        private void txtTitle_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtAuthor_TextChanged(object sender, EventArgs e)
        {

        }
    }
}


