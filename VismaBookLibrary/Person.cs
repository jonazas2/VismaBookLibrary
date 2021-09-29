using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VismaBookLibrary
{
    public class Person
    {
        [JsonConstructor]
        public Person(
            [JsonProperty("name")] string name = null,
            [JsonProperty("takeDate")] DateTime takeDate = new DateTime(),
            [JsonProperty("returDate")] DateTime returnDate = new DateTime()
        )
        {
            this.Name = name;
            this.TakeDate = takeDate;
            this.ReturnDate = returnDate;
        }

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("takeDate")]
        public DateTime TakeDate;

        [JsonProperty("returnDate")]
        public DateTime ReturnDate;
    }
}
