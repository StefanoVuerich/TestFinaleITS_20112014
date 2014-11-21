using Newtonsoft.Json;
using StefanoVuerich.TestFinale.AzureRepositories;
using StefanoVuerich.TestFinale.Models;
using StefanoVuerich.TestFinale.Queues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StefanoVuerich.TestFinale.QueueWorker
{
    class Program
    {
        
        static void Main(string[] args)
        {
            MyAzureRepository _rep = new MyAzureRepository();
            MyQueue _queue = new MyQueue();
            _queue.azureQueue.CreateIfNotExists();

            while (true)
            {
                var obj = _queue.azureQueue.GetMessage(TimeSpan.FromSeconds(10));

                if (obj != null)
                {
                    Console.WriteLine("Trovato oggetto");
                    var content = JsonConvert.DeserializeObject<Activity>(obj.AsString);
                    _rep.Insert(content);
                    _queue.azureQueue.DeleteMessage(obj);
                }
                else
                {
                    Thread.Sleep(10000);
                }
            }
        }
    }
}
