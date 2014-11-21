using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StefanoVuerich.TestFinale.Contracts;
using StefanoVuerich.TestFinale.Models;
using StefanoVuerich.TestFinale.Queues;
using StefanoVuerich.TestFinale.AzureRepositories;

namespace StefanoVuerich.TestFinale.Portal.Controllers
{
    [Authorize]
    public class ActivitiesController : ApiController
    {
        MyQueue _queue;
        MyAzureRepository _rep;
        public IEnumerable<Activity> Get()
        {
            _rep = new MyAzureRepository();
            return _rep.Get();
        }

        public Activity Get(string id)
        {
            _rep = new MyAzureRepository();
            return _rep.Get(id);
        }

        [HttpPost]
        public void Insert(ActivityInsertDTO activity)
        {
            Activity entity = new Activity(activity.Titolo, activity.Descrizione, activity.Duration, activity.CategoryID, activity.CategoryDescription);

            _queue = new MyQueue();
            _queue.SendToQueue(entity);
        }

        [HttpPut]
        public void Update(Activity acitivity)
        {
            _rep = new MyAzureRepository();
            _rep.Update(acitivity);
        }
        [HttpDelete]
        public void Delete(string id)
        {
            _rep = new MyAzureRepository();
            _rep.Delete(id);
        }
    }
}
