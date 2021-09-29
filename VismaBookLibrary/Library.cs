using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VismaBookLibrary
{
    public class Library
    {
		public List<Book> books = new List<Book>();

		private string filepath;

		public Library(string filepath){

			this.filepath = filepath;

			try
			{
				books = JsonConvert.DeserializeObject<List<Book>>(File.ReadAllText(filepath)) 
					?? new List<Book>();
			}
			catch (Exception ex)
			{
				//Exit the program if there was an error
				Console.WriteLine("Failed to read books from file! Error: {0}", ex.Message);
				Environment.Exit(0);
			}
		}

		public void AddBook(string name, string author, List<string> categories, string language, int year, string isbn)
        {
			Book book = new Book(
			name: name,
			author: author,
			categories: categories,
			language: language,
			year: year,
			isbn: isbn,
			person: new Person()
			);


            // Reads all the json data
            var jsonData = File.ReadAllText(filepath);

			// De-serialize to object or create a new list if empty
			var booksList = JsonConvert.DeserializeObject<List<Book>>(jsonData)
				?? new List<Book>();

			//Checks if book isn't already added
			if (!BookExists(isbn))
			{
				booksList.Add(book);

				// Updates json data to string
				jsonData = JsonConvert.SerializeObject(booksList);

				// Writes data to json file
				File.WriteAllText(filepath, jsonData);

				Console.WriteLine("Well done book was sucesfully added to the library");
			}
			else
			{
				Console.WriteLine("I am sorry, but this book is already in the library");
			}
		}

		public void TakeBook(string isbn, string personname, int days)
        {
			// Reads all the json data
			var jsonData = File.ReadAllText(filepath);

			var booksList = JsonConvert.DeserializeObject<List<Book>>(jsonData)
				?? new List<Book>();

			//Checks if book exists, book is not taken by anyone else and person taking it has less then 3 books
			if (BookExists(isbn) 
				&& !IsBookTaken(isbn) 
				&& BooksTaken(personname) < 3) { 
				booksList.Where(book => book.Isbn == isbn).ToList().ForEach((p) => {
					p.Person.Name = personname;
					p.Person.TakeDate = DateTime.Now;
					p.Person.ReturnDate = DateTime.Now.AddDays(days);
					p.IsTaken = true;
				});

				string output = JsonConvert.SerializeObject(booksList);
				File.WriteAllText(filepath, output);

				Console.WriteLine("Your book order is succesful");
			}

			else if (BooksTaken(personname) >= 3)
			{
				Console.WriteLine("I am sorry but you already have 3 books, you can't take more");
				return;
			}

			else if (!BookExists(isbn))
			{
				Console.WriteLine("I am sorry but there are no books with given isbn");
				return;
			}

			else if (IsBookTaken(isbn))
			{
				Console.WriteLine("I am sorry but this book is already taken");
				return;
			}
		}

		public void ReturnBook(string isbn)
		{
			var jsonData = File.ReadAllText(filepath);

			var booksList = JsonConvert.DeserializeObject<List<Book>>(jsonData)
				?? new List<Book>();

			//Checks if the book exists in the library
			if (BookExists(isbn))
			{
				//Checks if the book is taken
				if (IsBookTaken(isbn))
				{
					booksList.Where(book => book.Isbn == isbn).ToList().ForEach((p) =>
					{
						if (p.Person.ReturnDate < DateTime.Now)
						{
							Console.WriteLine("And I was thinking this book is gone forever...");
						}

						p.Person.Name = null;
						p.Person.TakeDate = new DateTime();
						p.Person.ReturnDate = new DateTime();
						p.IsTaken = false;
					});

					string output = JsonConvert.SerializeObject(booksList);
					File.WriteAllText(filepath, output);

					Console.WriteLine("Thank you for returning the book");
				}
				else
					Console.WriteLine("I am sorry but you can't return the book that has not been taken");
			}
			else
				Console.WriteLine("I am sorry but this book doesn't exist in the library");
		}

		public void DeleteBook(string isbn)
		{
			var jsonData = File.ReadAllText(filepath);

			var booksList = JsonConvert.DeserializeObject<List<Book>>(jsonData)
				?? new List<Book>();

			//Checks if book exists in the library
			if (BookExists(isbn))
			{
				//Checks if the book is already taken
				if (!IsBookTaken(isbn))
				{
					Book book = booksList.Where(book => book.Isbn == isbn).ToList()[0];

					booksList.Remove(book);

					// Updates json data to string
					jsonData = JsonConvert.SerializeObject(booksList);

					// Writes data to json file
					File.WriteAllText(filepath, jsonData);
					Console.WriteLine("Succesfully removed book from the library");
				}
				else
					Console.WriteLine("I am sorry, but you can't remove the book that is taken");
			}
			else
				Console.WriteLine("I am sorry, but you can't remove what is already gone");

		}


		public void ListBooks(List<Book> books)
        {
			foreach (Book book in books)
            {
				Console.WriteLine("Name: " + book.Name);
				Console.WriteLine("Author: " + book.Author);
				Console.WriteLine("Categories: " + string.Join(", ", book.Categories));
				Console.WriteLine("Language: " + book.Language);
				Console.WriteLine("Publication date: " + book.Year);
				Console.WriteLine("ISBN: " + book.Isbn);
				Console.WriteLine();
				Console.WriteLine("------------------------------------------");
				Console.WriteLine();
			}
		}

		public List<Book> GetBooks()
		{
			var jsonData = File.ReadAllText(filepath);

			var booksList = JsonConvert.DeserializeObject<List<Book>>(jsonData)
				?? new List<Book>();

			return booksList.ToList();
		}

		public List<Book> GetBooksByAuthor(string author)
		{
			var jsonData = File.ReadAllText(filepath);

			var booksList = JsonConvert.DeserializeObject<List<Book>>(jsonData)
				?? new List<Book>();

			return booksList.Where(book => book.Author == author).ToList();
		}

		public List<Book> GetBooksByCategory(string category)
		{
			var jsonData = File.ReadAllText(filepath);

			var booksList = JsonConvert.DeserializeObject<List<Book>>(jsonData)
				?? new List<Book>();

			return booksList.Where(book => book.Categories.Contains(category)).ToList();
		}

		public List<Book> GetBooksByLanguage(string language)
		{
			var jsonData = File.ReadAllText(filepath);

			var booksList = JsonConvert.DeserializeObject<List<Book>>(jsonData)
				?? new List<Book>();

			return booksList.Where(book => book.Language == language).ToList();
		}

		public List<Book> GetBooksByISBN(string isbn)
		{
			var jsonData = File.ReadAllText(filepath);

			var booksList = JsonConvert.DeserializeObject<List<Book>>(jsonData)
				?? new List<Book>();

			return booksList.Where(book => book.Isbn == isbn).ToList();
		}

		public List<Book> GetBooksByName(string name)
		{
			var jsonData = File.ReadAllText(filepath);

			var booksList = JsonConvert.DeserializeObject<List<Book>>(jsonData)
				?? new List<Book>();

			return booksList.Where(book => book.Name == name).ToList();
		}

		public List<Book> GetBooksByAvailability(bool istaken)
		{
			var jsonData = File.ReadAllText(filepath);

			var booksList = JsonConvert.DeserializeObject<List<Book>>(jsonData)
				?? new List<Book>();

			return booksList.Where(book => book.IsTaken == istaken).ToList();
		}


		public int BooksTaken(string personname)
		{
			var jsonData = File.ReadAllText(filepath);

			var booksList = JsonConvert.DeserializeObject<List<Book>>(jsonData)
				?? new List<Book>();

			int bookcount = booksList.Where(book => book.Person.Name == personname).ToList().Count();
			return bookcount;
		}

		public bool BookExists(string isbn)
		{
			var jsonData = File.ReadAllText(filepath);

			var booksList = JsonConvert.DeserializeObject<List<Book>>(jsonData)
				?? new List<Book>();

			if (booksList.Where(book => book.Isbn == isbn).ToList().Count() == 1)
				return true;
			else
				return false;
		}

		public bool IsBookTaken(string isbn)
		{
			var jsonData = File.ReadAllText(filepath);

			var booksList = JsonConvert.DeserializeObject<List<Book>>(jsonData)
				?? new List<Book>();

			if (booksList.Where(book => (book.Isbn == isbn) && (book.IsTaken == true)).ToList().Count() == 1)
				return true;
			else
				return false;
		}
	}
}