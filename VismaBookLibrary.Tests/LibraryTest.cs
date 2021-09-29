using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;

namespace VismaBookLibrary.Tests
{
    [TestClass]
    public class LibraryTest
    {
        [TestMethod]
        public void AddingBookWhenBookAlreadyExists()
        {
            File.Create("temp.json").Close();
           
            Library library = new Library(@"temp.json");

            library.AddBook("name", "author", new List<string> { "categories" }, "language", 2021, "isbn");

            var expected = library.GetBooks().Count;

            library.AddBook("name", "author", new List<string> { "categories" }, "language", 2021, "isbn");

            var actual = library.GetBooks().Count;

            Assert.AreEqual(actual, expected);

            File.Delete(@"temp.json");
        }

        [TestMethod]
        public void RemovingBookSomeoneOwns()
        {
            File.Create("temp.json").Close();

            Library library = new Library(@"temp.json");

            library.AddBook("name", "author", new List<string> { "categories" }, "language", 2021, "isbn");

            library.TakeBook("isbn", "person", 5);

            var expected = library.GetBooks().Count;

            library.DeleteBook("isbn");

            var actual = library.GetBooks().Count;

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void TakingBookWhileHaving3Books()
        {
            File.Create("temp.json").Close();

            Library library = new Library(@"temp.json");

            library.AddBook("name", "author", new List<string> { "categories" }, "language", 2021, "isbn0");
            library.AddBook("name", "author", new List<string> { "categories" }, "language", 2021, "isbn1");
            library.AddBook("name", "author", new List<string> { "categories" }, "language", 2021, "isbn2");
            library.AddBook("name", "author", new List<string> { "categories" }, "language", 2021, "isbn3");

            library.TakeBook("isbn0", "person", 5);
            library.TakeBook("isbn1", "person", 5);
            library.TakeBook("isbn2", "person", 5);

            var expected = library.BooksTaken("person");

            library.TakeBook("isbn3", "person", 5);

            var actual = library.BooksTaken("person");

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void TakingBookWhichIsTaken()
        {
            File.Create("temp.json").Close();

            Library library = new Library(@"temp.json");

            library.AddBook("name", "author", new List<string> { "categories" }, "language", 2021, "isbn");
            library.TakeBook("isbn", "person", 5);

            var expected = library.BooksTaken("person2");

            library.TakeBook("isbn", "person2", 5);

            var actual = library.BooksTaken("person2");

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void TakingBookWhichIsNotInLibrary()
        {
            File.Create("temp.json").Close();

            Library library = new Library(@"temp.json");

            library.AddBook("name", "author", new List<string> { "categories" }, "language", 2021, "isbn");            

            var expected = library.BooksTaken("person");

            library.TakeBook("isbn2", "person", 5);

            var actual = library.BooksTaken("person");

            Assert.AreEqual(actual, expected);
        }
    }
}

