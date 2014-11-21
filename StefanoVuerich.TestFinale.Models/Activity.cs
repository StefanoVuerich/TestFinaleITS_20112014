using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StefanoVuerich.TestFinale.Models
{
    public class Activity
    {
        public string ID { get; set; }
        public string Titolo { get; set; }
        public string Descrizione { get; set; }
        public DateTime CreationDate { get; set; }
        public int Duration { get; set; }
        public int CategoryID { get; set; }
        public string CategoryDescription { get; set; }
        public Activity() { }
        public Activity(string titolo, string descrizione, int durata, int categoryID, string categoryDescription)
        {
            this.ID = Guid.NewGuid().ToString();
            this.Titolo = titolo;
            this.Descrizione = descrizione;
            this.CreationDate = DateTime.Now;
            this.Duration = durata;
            this.CategoryID = categoryID;
            this.CategoryDescription = categoryDescription;
        }
    }
}
