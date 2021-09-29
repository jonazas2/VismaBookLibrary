using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable enable

namespace VismaBookLibrary
{
    
    public class Book
    {

        [JsonConstructor]
        public Book(
           [JsonProperty("name")] string name,
           [JsonProperty("author")] string author,
           [JsonProperty("categories")] List<string> categories,
           [JsonProperty("language")] string language,
           [JsonProperty("year")] int year,
           [JsonProperty("isbn")] string isbn,
           [JsonProperty("person")] Person person,
           [JsonProperty("isTaken")] bool isTaken = false
        )
        {
            this.Name = name;
            this.Author = author;
            this.Categories = categories;
            this.Language = language;
            this.Year = year;
            this.Isbn = isbn;            
            this.Person = person;
            this.IsTaken = isTaken;
        }

        [JsonProperty("name")]
        public string Name {get; }

        [JsonProperty("author")]
        public string Author { get; }

        [JsonProperty("categories")]
        public List<string> Categories { get; }

        [JsonProperty("language")]
        public string Language { get; }

        [JsonProperty("year")]
        public int Year { get; }

        [JsonProperty("isbn")]
        public string Isbn { get; }

        [JsonProperty("isTaken")]
        public bool IsTaken { get; set; }

        [JsonProperty("person")]
        public Person Person;
    }
}
