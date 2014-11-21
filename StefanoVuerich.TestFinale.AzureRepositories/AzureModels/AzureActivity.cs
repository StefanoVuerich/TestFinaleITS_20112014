using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StefanoVuerich.TestFinale.AzureRepositories.AzureModels
{
    class AzureActivity : TableEntity
    {
        public string ID { get; set; }
        public string Titolo { get; set; }
        public string Descrizione { get; set; }
        public DateTime CreationDate { get; set; }
        public int Duration { get; set; }
        public int CategoryID { get; set; }
        public string CategoryDescription { get; set; }
        public AzureActivity() { }
        public AzureActivity(string titolo, string descrizione, int duration, int categoryId, string categoryDescription)
        {
            this.PartitionKey = "1";
            this.RowKey = Guid.NewGuid().ToString();
            this.ID = Guid.NewGuid().ToString();
            this.Titolo = titolo;
            this.Descrizione = descrizione;
            this.CreationDate = DateTime.Now.Date;
            this.Duration = duration;
            this.CategoryID = categoryId;
            this.CategoryDescription = categoryDescription;
        }
    }
}
