using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using StefanoVuerich.TestFinale.AzureRepositories;
using StefanoVuerich.TestFinale.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StefanoVuerich.TestFinale.Queues
{
    public class MyQueue
    {
        //MyAzureRepository _rep;
        private readonly string _QUEUENAME = "productqueue";
        public CloudQueue azureQueue;
        public MyQueue()
            : this("azureCS")
        {
        }

        public MyQueue(string connectionStringName)
        {
            string storageConnectionString =
            ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            azureQueue = queueClient.GetQueueReference(_QUEUENAME);
            azureQueue.CreateIfNotExists();
        }

        public void SendToQueue(Activity activity)
        {
            var myJsonProduct = JsonConvert.SerializeObject(activity);
            var QueueObj = new CloudQueueMessage(myJsonProduct);
            azureQueue.AddMessage(QueueObj);
            //CheckIfSend();
        }

        //private void CheckIfSend()
        //{
        //    int? count = GetQueueCount();
        //    if (count != null && count >= 5)
        //    {
        //        _rep = new AzureRepositoryCls("azureStorage");
        //        for (int i = 0; i <= count - 1; i++)
        //        {
        //            var obj = _queue.GetMessage(TimeSpan.FromSeconds(10));
        //            var content = JsonConvert.DeserializeObject<Product>(obj.AsString);
        //            _rep.Post(content);
        //            _queue.DeleteMessage(obj);
        //        }
        //    }
        //}

        //private int? GetQueueCount()
        //{
        //    // Fetch the queue attributes.
        //    _queue.FetchAttributes();

        //    // Retrieve the cached approximate message count.
        //    return _queue.ApproximateMessageCount;
        //}
    }
}
