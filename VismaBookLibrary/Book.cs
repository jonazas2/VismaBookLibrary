using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
           [JsonProperty("isbn")] string isbn
       )
        {
            this.Name = name;
            this.Author = author;
            this.Categories = categories;
            this.Language = language;
            this.Year = year;
            this.Isbn = isbn;
        }

        [JsonProperty("name")]
        private string Name { get; }

        [JsonProperty("author")]
        private string Author { get; }

        [JsonProperty("category")]
        private List<string> Categories { get; }

        [JsonProperty("language")]
        private string Language { get; }

        [JsonProperty("year")]
        private int Year { get; }

        [JsonProperty("isbn")]
        private string Isbn { get; }

    }
}
