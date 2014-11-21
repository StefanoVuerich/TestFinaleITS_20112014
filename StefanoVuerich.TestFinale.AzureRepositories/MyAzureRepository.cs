using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using StefanoVuerich.TestFinale.AzureRepositories.AzureModels;
using StefanoVuerich.TestFinale.Contracts;
using StefanoVuerich.TestFinale.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StefanoVuerich.TestFinale.AzureRepositories
{
    public class MyAzureRepository : IQueries <Activity>
    {
        private readonly string _TABLENAME = "product";
        CloudTable _table;

        public MyAzureRepository()
            : this("azureCS")
        {
        }

        public MyAzureRepository(string connectionStringName)
        {
            string storageConnectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            _table = tableClient.GetTableReference(_TABLENAME);
            _table.CreateIfNotExists();
        }
        public IEnumerable<Activity> Get()
        {
            List<Activity> listaProdotti = new List<Activity>();
            TableQuery<AzureActivity> query = new TableQuery<AzureActivity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "1"));
            foreach (AzureActivity entity in _table.ExecuteQuery(query))
            {
                var activity = new Activity
                {
                    ID = entity.ID,
                    Titolo = entity.Titolo,
                    Descrizione = entity.Descrizione,
                    CreationDate = entity.CreationDate,
                    Duration = entity.Duration,
                    CategoryID = entity.CategoryID,
                    CategoryDescription = entity.CategoryDescription
                };
                listaProdotti.Add(activity);
            }
            return listaProdotti;
        }

        public Activity Get(string titolo)
        {
            TableQuery<AzureActivity> query = new TableQuery<AzureActivity>()
                .Where(TableQuery.GenerateFilterCondition("Titolo", QueryComparisons.Equal, titolo));
            var entity = _table.ExecuteQuery(query).LastOrDefault();
            if (entity != null)
            {
                return new Activity
                {
                    ID = entity.ID,
                    Titolo = entity.Titolo,
                    Descrizione = entity.Descrizione,
                    CreationDate = entity.CreationDate,
                    Duration = entity.Duration,
                    CategoryID = entity.CategoryID,
                    CategoryDescription = entity.CategoryDescription
                };
            }
            return null;
        }

        public void Insert(Activity activity)
        {
            AzureActivity productTE = new AzureActivity(activity.Titolo, activity.Descrizione, activity.Duration, activity.CategoryID, activity.CategoryDescription);
            productTE.RowKey = DateTime.Now.Ticks.ToString();
            productTE.PartitionKey = "1";
            TableOperation insertOperation = TableOperation.Insert(productTE);
            _table.Execute(insertOperation);
        }

        public void Update(Activity activity)
        {
            TableQuery<AzureActivity> query = new TableQuery<AzureActivity>()
                .Where(TableQuery.GenerateFilterCondition("ID", QueryComparisons.Equal, activity.ID));
            AzureActivity entity = _table.ExecuteQuery(query).FirstOrDefault();

            if (entity != null)
            {
                entity.ID = activity.ID;
                entity.Titolo = activity.Titolo;
                entity.Descrizione = activity.Descrizione;
                entity.CreationDate = activity.CreationDate;
                entity.Duration = activity.Duration;
                entity.CategoryID = activity.CategoryID;
                entity.CategoryDescription = activity.CategoryDescription;

                TableOperation updateOperation = TableOperation.Replace(entity);

                _table.Execute(updateOperation);

                Console.WriteLine("Entity updated.");
            }

            else
                Console.WriteLine("Entity could not be found");
        }

        public void Delete(string id)
        {
            TableQuery<AzureActivity> query = new TableQuery<AzureActivity>()
                .Where(TableQuery.GenerateFilterCondition("Titolo", QueryComparisons.Equal, id));
            AzureActivity entity = _table.ExecuteQuery(query).FirstOrDefault();

            if (entity != null)
            {
                TableOperation deleteOperation = TableOperation.Delete(entity);
                _table.Execute(deleteOperation);
                Console.WriteLine("Entity deleted.");
            }

            else
                Console.WriteLine("Could not retrieve the entity.");
        }
    }
}
