using System;
using System.Collections.Generic;

class Book
{
    public string Title { get; set; }
    public bool IsCheckedOut { get; set; } = false;
}

class Library
{
    private List<Book> books = new List<Book>();
    private int booksBorrowed = 0;
    private const int BorrowLimit = 3;

    public void AddBook(string title)
    {
        books.Add(new Book { Title = title });
    }

    public void SearchBook()
    {
        Console.Write("Enter book title to search: ");
        string searchTitle = Console.ReadLine();

        Book found = books.Find(b => b.Title.Equals(searchTitle, StringComparison.OrdinalIgnoreCase));

        if (found != null)
            Console.WriteLine(found.IsCheckedOut ? $"'{found.Title}' is currently checked out." : $"'{found.Title}' is available.");
        else
            Console.WriteLine($"Sorry, '{searchTitle}' is not in the collection.");
    }

    public void BorrowBook()
    {
        if (booksBorrowed >= BorrowLimit)
        {
            Console.WriteLine("You have reached the borrowing limit of 3 books.");
            return;
        }

        Console.Write("Enter book title to borrow: ");
        string borrowTitle = Console.ReadLine();

        Book found = books.Find(b => b.Title.Equals(borrowTitle, StringComparison.OrdinalIgnoreCase));

        if (found != null && !found.IsCheckedOut)
        {
            found.IsCheckedOut = true;
            booksBorrowed++;
            Console.WriteLine($"You have successfully borrowed '{found.Title}'.");
        }
        else if (found != null && found.IsCheckedOut)
            Console.WriteLine($"Sorry, '{found.Title}' is already checked out.");
        else
            Console.WriteLine($"Sorry, '{borrowTitle}' is not in the collection.");
    }

    public void ReturnBook()
    {
        Console.Write("Enter book title to return: ");
        string returnTitle = Console.ReadLine();

        Book found = books.Find(b => b.Title.Equals(returnTitle, StringComparison.OrdinalIgnoreCase));

        if (found != null && found.IsCheckedOut)
        {
            found.IsCheckedOut = false;
            booksBorrowed--;
            Console.WriteLine($"'{found.Title}' has been returned. Thank you!");
        }
        else if (found != null && !found.IsCheckedOut)
            Console.WriteLine($"'{found.Title}' was not checked out.");
        else
            Console.WriteLine($"'{returnTitle}' is not in the collection.");
    }

    public void DisplayBooks()
    {
        Console.WriteLine("\nLibrary Collection:");
        foreach (var book in books)
        {
            Console.WriteLine($"{book.Title} {(book.IsCheckedOut ? "(Checked Out)" : "(Available)")}");
        }
    }
}

class Program
{
    static void Main()
    {
        Library library = new Library();

        // Adding sample books
        library.AddBook("The Hobbit");
        library.AddBook("1984");
        library.AddBook("Harry Potter");
        library.AddBook("Pride and Prejudice");

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\nChoose an option: 1-Search 2-Borrow 3-Return 4-Show All 5-Exit");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": library.SearchBook(); break;
                case "2": library.BorrowBook(); break;
                case "3": library.ReturnBook(); break;
                case "4": library.DisplayBooks(); break;
                case "5": exit = true; break;
                default: Console.WriteLine("Invalid option"); break;
            }
        }
    }
}
