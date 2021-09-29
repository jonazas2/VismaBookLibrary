using System;
using System.Collections.Generic;
using System.Globalization;

namespace VismaBookLibrary
{

    class Program
    {
        
        static void Main(string[] args)
        {
            DisplayCommands();

            int number = -1;
            while (true)
            {
                while (!int.TryParse(Console.ReadLine(), out number))
                {
                    Console.Write("This is not valid input! Try again:");
                }

                Navigation(number);
            }
        }

        private static void DisplayCommands()
        {
            Console.WriteLine("Commands");
            Console.WriteLine("---------------------------------------------------------------------------");
            Console.WriteLine("1. Add a new book");
            Console.WriteLine("2. Take a book");
            Console.WriteLine("3. List/filter all the books");
            Console.WriteLine("4. Show main menu");
            Console.WriteLine("5. Return the book");
            Console.WriteLine("6. Delete a book");
            Console.WriteLine("0. Close the program");
            Console.WriteLine("---------------------------------------------------------------------------");
        }

        private static void DisplayFilterMenu()
        {
            Console.WriteLine("Choose the book filter:");
            Console.WriteLine("---------------------------------------------------------------------------");
            Console.WriteLine("1. List all books without filtering:");
            Console.WriteLine("2. Filter by Author name:");
            Console.WriteLine("3. Filter by Book name:");
            Console.WriteLine("4. Filter by Category:");
            Console.WriteLine("5. Filter by Language:");
            Console.WriteLine("6. Filter by International Standard Book Number (ISBN):");
            Console.WriteLine("7. Filter by Availability:");
            Console.WriteLine("0. Return to menu:");
            Console.WriteLine("---------------------------------------------------------------------------");
        }


        private static void Navigation(int key)
        {
            Library library = new Library(@"..\..\..\Books.json");

            string bookname;          
            string author;
            List<string> categories = new List<string>();
            string language;
            int year;
            string isbn = "";
            bool isavailable;

            string personname;
            int daystaken = 0;

            switch (key)
            {
                case 1:

                    Console.Write("Enter the book name:");
                    bookname = Console.ReadLine();

                    Console.Write("Enter the author name:");
                    author = Console.ReadLine();

                    Console.Write("Enter the categories (divided by comma and space):");
                    foreach (var item in Console.ReadLine().Split(", "))
                    {
                        categories.Add(item);
                    }

                    Console.Write("Enter the language:");
                    language = Console.ReadLine();

                    Console.Write("Enter the year book was released in: ");
                    while (!int.TryParse(Console.ReadLine(), out year))
                    {
                        Console.Write("This is not valid input. Please enter a number: ");
                    }
                    Console.Write("Enter books International Standard Book Number (ISBN):");
                    isbn = Console.ReadLine();                  

                    library.AddBook(bookname, author, categories, language, year, isbn);
                    
                    categories.Clear();

                    break;

                case 2:
                    
                    Console.Write("Enter books International Standard Book Number (ISBN):");
                    isbn = Console.ReadLine();

                    Console.Write("Enter your name:");
                    personname = Console.ReadLine();

                    //Doesn't allow negative numbers and numbers bigger then 60
                    Console.Write("Enter the amount of days you are taking the book for:");
                    do                    
                    {
                        int.TryParse(Console.ReadLine(), out daystaken);
                        if (daystaken < 0)
                            Console.Write("This is not valid input. Please enter a non negative number:");
                        if (daystaken > 60)
                            Console.Write("This is not valid input. You can't take books for more then 2 months:");
                    } 
                    while (daystaken < 0 || daystaken > 60);

                    library.TakeBook(isbn, personname, daystaken);

                    break;

                case 3:
                    int selection;
                    DisplayFilterMenu();

                    do
                    {
                        int.TryParse(Console.ReadLine(), out selection);
                        if (selection < 0 || selection > 6)
                            Console.WriteLine("This is not valid input:");
                    }
                    while (selection < 0 || selection > 6);

                    switch (selection) 
                    {
                        case 1:
                            Console.WriteLine("------------------------------------------");

                            library.ListBooks(library.GetBooks());

                            break;

                        case 2:
                            Console.Write("Enter the author name:");
                            author = Console.ReadLine();

                            Console.WriteLine("------------------------------------------");

                            library.ListBooks(library.GetBooksByAuthor(author));

                            break;

                        case 3:
                            Console.Write("Enter the book's name:");
                            bookname = Console.ReadLine();

                            Console.WriteLine("------------------------------------------");

                            library.ListBooks(library.GetBooksByName(bookname));

                            break;

                        case 4:
                            Console.Write("Enter the category:");
                            
                            categories.Add(Console.ReadLine());

                            Console.WriteLine("------------------------------------------");

                            library.ListBooks(library.GetBooksByCategory(categories[0]));

                            break;

                        case 5:
                            Console.Write("Enter the language:");
                            language = Console.ReadLine();

                            Console.WriteLine("------------------------------------------");

                            library.ListBooks(library.GetBooksByLanguage(language));

                            break;

                        case 6:
                            Console.Write("Enter the International Standard Book Number (ISBN):");
                            isbn = Console.ReadLine();

                            Console.WriteLine("------------------------------------------");

                            library.ListBooks(library.GetBooksByName(isbn));

                            break;

                        case 7:
                            Console.Write("Enter book's availability true if book is taken false if book is available: ");

                            while (!bool.TryParse(Console.ReadLine(), out isavailable))
                            {
                                Console.Write("This is not valid input. Please enter either true or false: ");
                            }
                            Console.WriteLine("------------------------------------------");

                            library.ListBooks(library.GetBooksByAvailability(isavailable));
                            break;

                        case 0:
                            DisplayCommands();
                            break;
                    }
                    break;

                case 4:
                    DisplayCommands();
                    break;

                case 5:
                    Console.Write("Enter books International Standard Book Number (ISBN):");
                    isbn = Console.ReadLine();

                    library.ReturnBook(isbn);
                    break;

                case 6:
                    Console.Write("Enter books International Standard Book Number (ISBN):");
                    isbn = Console.ReadLine();

                    library.DeleteBook(isbn);
                    break;
                case 0:
                    Environment.Exit(0);

                    break;

                default:
                    Console.WriteLine("Wrong value entered");

                    break;
            }
        }
    }
}