using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;


namespace WindowsFormsApplication1
{

    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
        public bool IsBorrowed { get; set; }
        public string Borrower { get; set; }

        public Book(string title, string author, int year, bool isBorrowed = false, string borrower = "")
        {
            Title = title;
            Author = author;
            Year = year;
            IsBorrowed = isBorrowed;
            Borrower = borrower;
        }
    }
}
