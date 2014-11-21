using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StefanoVuerich.TestFinale.Contracts
{
    public interface IQueries <T>
    {
        IEnumerable<T> Get();
        T Get(string id);
        void Insert(T t);
        void Update(T t);
        void Delete(string id);
    }
}
